using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    class Camera2D
    {
        private float rotation, zoom;
        private Vector2 position;

        public Camera2D() : this(new Vector2(), 0.0f, 1.0f)
        {

        }

        public Camera2D(Vector2 position, float rotation, float zoom)
        {
            this.position = position;
            this.rotation = rotation;
            this.zoom = zoom;
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
            }
        }

        public float Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                zoom = value;
            }
        }

        public void Move(Vector2 offset)
        {
            position += offset;
        }

        public Matrix GetCameraTransform(GraphicsDevice gd)
        {
            Matrix toReturn =
                Matrix.CreateTranslation(new Vector3(-position.X + gd.Viewport.Width / 2, -position.Y + gd.Viewport.Height / 2, 0)) *
                Matrix.CreateRotationZ(rotation) *
                Matrix.CreateScale(new Vector3(zoom, zoom, 1));

            return toReturn;
        }
    }
}
