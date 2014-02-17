/*
 * QuadTree class for spacially managing game content
 * 
 * Adapted from: http://www.underwatergorilladome.com/managing-2d-objects-with-a-quadtree/
 * Original author: Eric Blatz
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalProject
{
    public class QuadTree<T>
    {
        Dictionary<T, QuadTreeItem> AllItems;
        protected Func<T, Rectangle> PositionDelegate;
        protected QuadTreeNode Root;

        protected class QuadTreeItem
        {
            public QuadTreeNode Parent;
            public Rectangle Position;
            public T Value;
        }

        protected class QuadTreeNode
        {
            public QuadTreeNode Parent;
            public QuadTreeNode[] Children;
            public List<QuadTreeItem> Items;
            public Rectangle Position;
            public int ItemCount;

            public QuadTreeNode(Rectangle dimensions, QuadTreeNode parent)
            {
                Items = new List<QuadTreeItem>();
                Position = dimensions;
                Parent = parent;
            }
        }

        public QuadTree(Func<T, Rectangle> positionDelegate, Rectangle dimensions)
        {
            PositionDelegate = positionDelegate;
            AllItems = new Dictionary<T, QuadTreeItem>();
            Root = new QuadTreeNode(dimensions, null);
        }

        public void Add(T item)
        {
            QuadTreeItem i = new QuadTreeItem();
            i.Value = item;
            i.Position = PositionDelegate(item);
            AllItems.Add(item, i);
            Add(Root, i);
        }

        private bool Add(QuadTreeNode node, QuadTreeItem item)
        {
            Rectangle nodePosition = node.Position;
            //check if the item is entirely contained in this node
            if (nodePosition.Contains(item.Position))
            {
                if (node.Children == null)
                {
                    //Bucket has not been split yet
                    if (node.Items.Count < 5)
                    {
                        InnerAdd(node, item);
                        return true;
                    }

                    //Bucket needs to be split
                    CreateChildren(node);

                    //Move all nodes down
                    for (int i = 0; i < node.Items.Count; i++)
                    {
                        if (MoveDown(node.Items[i]))
                        {
                            i--;
                        }
                    }
                }
                //bucket has been split
                //try to add the item to each child
                foreach (QuadTreeNode child in node.Children)
                {
                    if (Add(child, item))
                    {
                        node.ItemCount++;
                        return true;
                    }
                }

                //couldn't add to any children, add to this node
                InnerAdd(node, item);
                return true;
            }

            return false;
        }

        private void CreateChildren(QuadTreeNode node)
        {
            Rectangle nodePosition = node.Position;
            int w = nodePosition.Width / 2, h = nodePosition.Height / 2;
            node.Children = new QuadTreeNode[4] {
                new QuadTreeNode(new Rectangle(nodePosition.X, nodePosition.Y, w, h), node),
                new QuadTreeNode(new Rectangle(nodePosition.X + w, nodePosition.Y, w,h), node),
                new QuadTreeNode(new Rectangle(nodePosition.X, nodePosition.Y + h, w, h), node),
                new QuadTreeNode(new Rectangle(nodePosition.X + w, nodePosition.Y + h, w, h), node)
            };
        }

        private bool MoveDown(QuadTreeItem item)
        {
            foreach (QuadTreeNode child in item.Parent.Children)
            {
                if (Add(child, item))
                {
                    return true;
                }
            }

            return false;
        }

        private void InnerAdd(QuadTreeNode node, QuadTreeItem item)
        {
            if (item.Parent != null)
            {
                item.Parent.Items.Remove(item);
            }
            node.Items.Add(item);
            node.ItemCount++;
            item.Parent = node;
        }

        public void Remove(T item)
        {
            Remove(AllItems[item].Parent, AllItems[item]);
            AllItems.Remove(item);
        }

        private void Remove(QuadTreeNode node, QuadTreeItem item)
        {
            node.Items.Remove(item);
            item.Parent = null;

            while (node != null)
            {
                node.ItemCount--;
                if (node.ItemCount < 6)
                {
                    CollapseChildren(node);
                }
                node = node.Parent;
            }
        }

        private void CollapseChildren(QuadTreeNode node)
        {
            if (node == null || node.Children == null)
                return;

            foreach (QuadTreeNode child in node.Children)
            {
                while (child.Items.Count > 0)
                {
                    MoveUp(child.Items[0]);
                }
            }

            node.Children = null;
        }

        private void MoveUp(QuadTreeItem item)
        {
            item.Parent.Items.Remove(item);
            item.Parent.ItemCount--;
            if (item.Parent.Children != null && item.Parent.ItemCount < 6)
            {
                CollapseChildren(item.Parent);
            }
            item.Parent = item.Parent.Parent;


            if (item.Parent == null)
            {
                AllItems.Remove(item.Value);
            }
            else
            {
                item.Parent.Items.Add(item);
            }
        }

        public void UpdatePosition(T item)
        {
            QuadTreeItem ri = AllItems[item];
            Rectangle newPosition = PositionDelegate(item);

            if (newPosition == ri.Position)
            {
                return;
            }

            ri.Position = newPosition;
            if (ri.Parent.Position.Contains(newPosition))
            { //Step Into
                if (ri.Parent.Children != null)
                    MoveDown(ri);
            }
            else
            {
                do
                { //Step Out Of
                    MoveUp(ri);
                } while (ri.Parent != null && !ri.Parent.Position.Contains(newPosition));

                if (ri.Parent != null)
                {
                    Add(ri.Parent, ri);
                }
            }
        }

        public T[] GetItems(Rectangle area)
        {
            return GetItems(Root, area);
        }

        private T[] GetItems(QuadTreeNode node, Rectangle area)
        {
            List<T> items = new List<T>();
            foreach (QuadTreeItem item in node.Items)
            {
                if (item.Position.Intersects(area) || item.Position.Contains(area) || area.Contains(item.Position))
                {
                    items.Add(item.Value);
                }
            }

            if (node.Children != null)
            {
                foreach (QuadTreeNode child in node.Children)
                {
                    if (area.Contains(child.Position))
                    {
                        items.AddRange(GetAllItems(node));
                    }
                    else if (child.Position.Contains(area))
                    {
                        items.AddRange(GetItems(child, area));
                        break;
                    }
                    else if (child.Position.Intersects(area))
                    {
                        items.AddRange(GetItems(child, area));
                    }
                }
            }

            return items.ToArray();
        }

        private T[] GetAllItems(QuadTreeNode node)
        {
            T[] items = new T[node.ItemCount];
            int i = 0;
            foreach (QuadTreeItem item in node.Items)
            {
                items[i++] = item.Value;
            }

            if (node.Children != null)
            {
                int q;
                foreach (QuadTreeNode child in node.Children)
                {
                    T[] tmp = GetAllItems(child);
                    for (q = i; q < tmp.Length + i; q++)
                    {
                        items[q] = tmp[q - i];
                    }
                    i = q;
                }
            }

            return items;
        }

        public T[] GetItems(Point point)
        {
            return GetItems(Root, point);
        }

        private T[] GetItems(QuadTreeNode node, Point point)
        {
            List<T> items = new List<T>();
            foreach (QuadTreeItem item in node.Items)
            {
                if (item.Position.Contains(point))
                {
                    items.Add(item.Value);
                }
            }

            if (node.Children != null)
            {
                foreach (QuadTreeNode child in node.Children)
                {
                    if (child.Position.Contains(point))
                    {
                        items.AddRange(GetItems(child, point));
                        break;
                    }
                }
            }
            return items.ToArray();
        }
    }
}
