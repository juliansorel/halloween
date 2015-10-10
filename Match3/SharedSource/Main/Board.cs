using System;
using System.Collections.Generic;
using System.Text;
using WaveEngine.Common.Math;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;

namespace Match3
{
    public class Board
    {

        private int _x;
        private int _y;
        private int _width;
        private int _height;
        private int _columns;
        private int _rows;


        public Board(int x, int y, int width, int height, int columns, int rows)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _columns = columns;
            _rows = rows;
        }

        public Vector2 GetTileScale(int tileWidth = 100, int tileHeight= 100)
        {
            // this wasn't tested for boards that don't have square tiles. Try to use boards with square tiles.
            float scaleX = ((float)_width / (float)_columns) / (float) tileWidth;
            float scaleY = ((float)_height / (float)_rows) / (float) tileHeight;
            return new Vector2(scaleX, scaleY);
        }

        public Point GetTilePosition(int column, int row)
        {
            int tileWidth = _width / _columns;
            int tileHeight = _height / _rows;

            return new Point(_x + (column * tileWidth), _y + (row * tileHeight));
        }

        public List<Entity> GenerateRandomBoard(string tilesSpriteSheet, string[] tiles)
        {
            Random random = new Random();
            List<Entity> entities = new List<Entity>();

            for (int i = 0; i < _columns; i++)
            {
                for(int j = 0; j < _rows; j++)
                {
                    int tileIndex = random.Next(tiles.Length);
                    Point tilePosition = GetTilePosition(i, j);
                    var entity = new Entity()
                        .AddComponent(new SpriteAtlas(tilesSpriteSheet,
                            tiles[tileIndex]))
                        .AddComponent(new SpriteAtlasRenderer(DefaultLayers.Alpha))
                        .AddComponent(new Transform2D() { X = tilePosition.X, Y = tilePosition.Y, Scale = GetTileScale() });
                    entities.Add(entity);
                }
            }


            return entities;
        }
    }
}
