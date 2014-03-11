using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject
{
    class GameMapForest : GameMap
    {
        private MapDataBuilder mapDataBuilder;

        private readonly static int CLEAR_AREA_LENGTH = 5;
        private readonly static int MAP_WIDTH_BORDER = GraphicsDeviceManager.DefaultBackBufferWidth / 2;
        private readonly static int MAP_HEIGHT_BORDER = GraphicsDeviceManager.DefaultBackBufferHeight / 2;

        public GameMapForest(int height, int width)
        {
            this.collidablesToAdd = new List<Collidable>();
            this.mapDataBuilder = new MapDataBuilder();
            this.Height = height;
            this.Width = width;

            this.contentLayers = new List<MapEntity[][]>(MAX_LAYERS);

            for (int i = 0; i < MAX_LAYERS; i++)
            {
                MapEntity[][] layer = new MapEntity[Height / MapEntity.MAP_ENTITY_BASE_SIZE][];

                for (int j = 0; j < layer.Length; j++)
                {
                    layer[j] = new MapEntity[Width / MapEntity.MAP_ENTITY_BASE_SIZE];
                }

                this.contentLayers.Add(layer);
            }

            this.mapDataBuilder.SetSpawn(GetRandomMapPoint());
            LoadContent();
            this.mapDataBuilder.SetMapContents(this.contentLayers);
            this.mapDataBuilder.SetEnd(GetRandomMapPoint());
            this.mapData = this.mapDataBuilder.GetResult();
            
            CreateStartAndEnd();
            PopulateCollisionTree();

            DebugText.GetInstance().WriteLinePerm("Spawn: " + this.mapData.Spawn + " End: " + this.mapData.End);
        }

        public override void LoadContent()
        {
            CreateMapBorder();
            RandomlyGenerateTerrain();
        }

        private Vector2 GetRandomMapPoint()
        {
            Random rand = new Random();

            return new Vector2((float)rand.Next(CLEAR_AREA_LENGTH * MapEntity.MAP_ENTITY_BASE_SIZE + MAP_WIDTH_BORDER, 
                                         Width - MAP_WIDTH_BORDER - CLEAR_AREA_LENGTH * MapEntity.MAP_ENTITY_BASE_SIZE),
                               (float)rand.Next(CLEAR_AREA_LENGTH * MapEntity.MAP_ENTITY_BASE_SIZE + MAP_HEIGHT_BORDER,
                                         Height - MAP_HEIGHT_BORDER - CLEAR_AREA_LENGTH * MapEntity.MAP_ENTITY_BASE_SIZE));
        }

        private void CreateStartAndEnd()
        {
            Random rand = new Random();
            Point start = new Point( (int)this.mapData.Spawn.X / MapEntity.MAP_ENTITY_BASE_SIZE, 
                                     (int)this.mapData.Spawn.Y / MapEntity.MAP_ENTITY_BASE_SIZE ),
                    end = new Point( (int)this.mapData.End.X / MapEntity.MAP_ENTITY_BASE_SIZE,
                                     (int)this.mapData.End.Y / MapEntity.MAP_ENTITY_BASE_SIZE );

            ClearAnOpenPath(start);
            ClearAnOpenPath(end);
            CreatePath(start, end, MapEntityFactory.MAP_ENTITY.DIRT);
        }

        private void ClearAnOpenPath(Point point)
        {

            for(int i = point.X - CLEAR_AREA_LENGTH/2; i < point.X + CLEAR_AREA_LENGTH; i++)
            {
                for(int j = point.Y - CLEAR_AREA_LENGTH/2; j < point.Y + CLEAR_AREA_LENGTH; j++)
                {
                    this.contentLayers[0][i][j] = CreateMapEntity(MapEntityFactory.MAP_ENTITY.DIRT, i, j);

                    for (int l = 1; l < this.contentLayers.Capacity; l++)
                    {
                        if(this.contentLayers[l][i][j] is CollidableMapEntity)
                            this.collidablesToAdd.Remove((CollidableMapEntity)this.contentLayers[l][i][j]);
                        this.contentLayers[l][i][j] = null;
                    }
                }
            }
        }

        private void CreatePath(Point start, Point end, MapEntityFactory.MAP_ENTITY path_type)
        {
            int mapWidthBorder = MAP_WIDTH_BORDER / MapEntity.MAP_ENTITY_BASE_SIZE,
                mapHeightBorder = MAP_HEIGHT_BORDER / MapEntity.MAP_ENTITY_BASE_SIZE;

            Random rand = new Random();
            double value = 0;

            while (Math.Abs(start.X - end.X) >= CLEAR_AREA_LENGTH/2 || Math.Abs(start.Y - end.Y) >= CLEAR_AREA_LENGTH/2)
            {
                this.contentLayers[(int)LAYERS.GROUND][start.X][start.Y] = this.CreateMapEntity(path_type, start.X, start.Y);

                value = rand.NextDouble();

                if(value < .25 && start.X+1 < this.contentLayers[0].Length - mapWidthBorder)
                {
                    start.X++;
                }
                else if (value < .5 && start.X-1 >= mapWidthBorder)
                {
                    start.X--;
                }
                else if (value < .75 && start.Y+1 < this.contentLayers[0][0].Length - mapHeightBorder)
                {
                    start.Y++;
                }
                else if (start.Y-1 >= mapHeightBorder)
                {
                    start.Y--;
                }
            }
        }

        private MapEntity CreateMapEntity(MapEntityFactory.MAP_ENTITY type, int x, int y)
        {
            return MapEntityFactory.GetInstance().CreateMapEntity(type, new Vector2(x * MapEntity.MAP_ENTITY_BASE_SIZE, y * MapEntity.MAP_ENTITY_BASE_SIZE));
        }

        private CollidableMapEntity CreateCollidableMapEntity(MapEntityFactory.MAP_ENTITY type, int x, int y)
        {
            return MapEntityFactory.GetInstance().CreateCollidableMapEntity(type, new Vector2(x * MapEntity.MAP_ENTITY_BASE_SIZE, y * MapEntity.MAP_ENTITY_BASE_SIZE));
        }

        private void CreateMapBorder()
        {
            FillEntireMapWithBorder();
            CreateCollidableBorder();
            CutOutMiddleMapWithoutBorder();
        }

        private void CreateCollidableBorder()
        {
            collidablesToAdd.Add(new CollidableNullBox(new Rectangle(0, 0, Height, MAP_HEIGHT_BORDER+32))); // top
            collidablesToAdd.Add(new CollidableNullBox(new Rectangle(0, Width - MAP_HEIGHT_BORDER, Height, MAP_HEIGHT_BORDER))); // bottom
            collidablesToAdd.Add(new CollidableNullBox(new Rectangle(0, 0, MAP_WIDTH_BORDER+32, Width))); // left
            collidablesToAdd.Add(new CollidableNullBox(new Rectangle(Height-MAP_WIDTH_BORDER, 0, MAP_WIDTH_BORDER, Width))); // right
        }

        private void FillEntireMapWithBorder()
        {
            Random rand = new Random();
            CollidableMapEntity toAdd = null;

            for (int i = 0; i < contentLayers[0].Length; i += rand.Next(0, 2))
            {
                for (int j = 0; j < contentLayers[0][0].Length; j += rand.Next(0, 4))
                {
                    contentLayers[(int)LAYERS.GROUND][i][j] = CreateMapEntity(MapEntityFactory.MAP_ENTITY.GRASS, i, j);

                    if (rand.Next(0, 100) < 10)
                        toAdd = CreateCollidableMapEntity(MapEntityFactory.MAP_ENTITY.BUSH, i, j);
                    else if (rand.Next(0, 10) <= 5)
                        toAdd = CreateCollidableMapEntity(MapEntityFactory.MAP_ENTITY.TREE_LARGE, i, j);
                    else
                        toAdd = CreateCollidableMapEntity(MapEntityFactory.MAP_ENTITY.TREE_SMALL, i, j);

                    contentLayers[(int)LAYERS.TREES][i][j] = toAdd;
                }
            }
        }

        private void CutOutMiddleMapWithoutBorder()
        {
            int mapWidthOffset = MAP_WIDTH_BORDER / MapEntity.MAP_ENTITY_BASE_SIZE,
                mapHeightOffset = MAP_HEIGHT_BORDER / MapEntity.MAP_ENTITY_BASE_SIZE;

            for (int i = mapWidthOffset; i < contentLayers[0].Length - mapWidthOffset; i++)
            {
                for (int j = mapHeightOffset; j < contentLayers[0][0].Length - mapHeightOffset; j++)
                {
                    for (int k = 0; k < MAX_LAYERS; k++)
                    {
                        contentLayers[k][i][j] = null;
                    }
                }
            }
        }

        private CollidableMapEntity CreateTreeOrBush(float value, int i, int j)
        {
            MapEntityFactory factory = MapEntityFactory.GetInstance();
            Vector2 position = new Vector2(i * MapEntity.MAP_ENTITY_BASE_SIZE, j * MapEntity.MAP_ENTITY_BASE_SIZE);
            CollidableMapEntity treeOrBush = null;

            if (value < .95f)
                treeOrBush = factory.CreateCollidableMapEntity(MapEntityFactory.MAP_ENTITY.TREE_LARGE, position);
            else if (value < .97f)
                treeOrBush = factory.CreateCollidableMapEntity(MapEntityFactory.MAP_ENTITY.TREE_SMALL, position);
            else
                treeOrBush = factory.CreateCollidableMapEntity(MapEntityFactory.MAP_ENTITY.BUSH, position);

            AddToCollideList(treeOrBush);

            return treeOrBush;
        }

        private void RandomlyGenerateTerrain()
        {
            IGenerator valueGenerator = new GeneratorSimplexNoise();
            float value = 0f;

            int mapWidthOffset = MAP_WIDTH_BORDER / MapEntity.MAP_ENTITY_BASE_SIZE,
                mapHeightOffset = MAP_HEIGHT_BORDER / MapEntity.MAP_ENTITY_BASE_SIZE;

            for (int i = 0; i < contentLayers[0].Length; i++)
            {
                for (int j = 0; j < contentLayers[0][0].Length; j++)
                {
                    if (contentLayers[0][i][j] != null) // empty spot, fill it with something more or less randomly
                        continue;


                    value = valueGenerator.GetValue(i * MapEntity.MAP_ENTITY_BASE_SIZE, j * MapEntity.MAP_ENTITY_BASE_SIZE, new Random().Next(100000));

                    if (value < 0.1f)
                    {
                        contentLayers[(int)LAYERS.GROUND][i][j] = CreateMapEntity(MapEntityFactory.MAP_ENTITY.DIRT, i, j);

                        if (value <= 0.07)
                        {
                            contentLayers[(int)LAYERS.ABOVE_GROUND][i][j] = CreateMapEntity(MapEntityFactory.MAP_ENTITY.GRASS_BLADES, i, j);
                        }
                    }
                    else if (value <= .9f)
                    {
                        contentLayers[(int)LAYERS.GROUND][i][j] = CreateMapEntity(MapEntityFactory.MAP_ENTITY.GRASS, i, j);
                    }
                    else if (value <= 1f)
                    {
                        // this makes sure something is spawned in the upper left corner of the tree or bush because otherwise the background will bleed through and become visible
                        contentLayers[(int)LAYERS.GROUND][i][j] = CreateMapEntity(MapEntityFactory.MAP_ENTITY.GRASS, i, j);
                        contentLayers[(int)LAYERS.TREES][i][j] = CreateTreeOrBush(value, i, j);
                    }
                }
            }
        }
    }
}
