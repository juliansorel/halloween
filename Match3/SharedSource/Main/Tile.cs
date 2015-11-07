using System;
using System.Collections.Generic;
using System.Text;
using WaveEngine.Common.Math;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;

namespace Match3
{
    public class Tile : BaseDecorator
    {
        public int Index { get; set; }
        public int BoardColumn { get; set; }
        public int BoardRow { get; set; }
        public Vector2 Scale { get; set; }
        public float X
        {
            get { return this.entity.FindComponent<Transform2D>().X; }
            set { this.entity.FindComponent<Transform2D>().X = value; }
        }
        public float Y
        {
            get { return this.entity.FindComponent<Transform2D>().Y; }
            set { this.entity.FindComponent<Transform2D>().Y = value; }
        }

        private string[] _tileSprites = {WaveContent.Tiles_spritesheet_TextureName.black,
                WaveContent.Tiles_spritesheet_TextureName.blue,
                WaveContent.Tiles_spritesheet_TextureName.green,
                WaveContent.Tiles_spritesheet_TextureName.red,
                WaveContent.Tiles_spritesheet_TextureName.yellow,
                WaveContent.Tiles_spritesheet_TextureName.special_1};

        public Tile(int spriteIndex, Vector2 scale): this(spriteIndex, -1000, -1000, scale)
        {

        }

        public Tile(int spriteIndex, int x, int y, Vector2 scale)
        {
            this.entity = new Entity()
                        .AddComponent(new SpriteAtlas(WaveContent.Tiles_spritesheet,
                            _tileSprites[spriteIndex]))
                        .AddComponent(new SpriteAtlasRenderer(DefaultLayers.Alpha))
                        .AddComponent(new Transform2D() { X = x, Y = y, Scale = scale, Origin = Vector2.Center })
                        .AddComponent(new TileBehavior(this));
            Selected = false;
            Index = spriteIndex;
			Matched = false;
			Gone = false;
        }

        public bool Selected { get; set; }
		public bool Matched { get; set; }
		public bool Gone { get; set; }

        public void Change(int spriteIndex)
        {
            Index = spriteIndex;
            this.entity.FindComponent<SpriteAtlas>().TextureName = _tileSprites[spriteIndex];
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}, {2}",Index, BoardColumn, BoardRow);
        }
    }

    public class Special1Tile : Tile
    {
        public Special1Tile(int x, int y, Vector2 scale) : base(5, x, y, scale)
        {

        }

        public Special1Tile(Vector2 scale):this (-1000, -1000, scale)
        {

        }
    }
}
