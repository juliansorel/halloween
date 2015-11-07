using System;
using System.Collections.Generic;
using System.Text;
using WaveEngine.Common.Math;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;

namespace Match3
{
    public class Board : BaseDecorator
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Columns { get; set; }
        public int Rows { get; set; }
        public int TileSide { get; set; }
        public int ActualTileSide { get; private set; }
        public Tile[,] Tiles { get; set; }
        public double ChanceSpecial1
        {
            get; set;
        }



        private Random _random = new Random();
        private int _noTilesKinds;

        public ObjectivesPanel ObjectivesPanel;
		public Dictionary<int, int> MatchRewards { get; set; }

        public Board(BoardConfiguration config, int tileSide)
        {

            X = config.X;
            Y = config.Y;
            Width = config.Width;
            Height = config.Height;
            Columns = config.Columns;
            Rows = config.Rows;
            TileSide = tileSide;
            ActualTileSide = (int)(GetTileScale(TileSide, TileSide).X * TileSide);
            this.entity = new Entity() { Name = config.Name }.AddComponent(new BoardBehavior(this));
            this.Tag = "board";
            ChanceSpecial1 = config.Special1Chance;

            List<Objective> objectives = new List<Objective>();
            foreach(ObjectiveConfiguration objectiveConfig in config.Objectives)
            {
                Objective objective = new Objective(objectiveConfig.TileIndex, objectiveConfig.RequiredAmount, objectiveConfig.Name);
                objectives.Add(objective);
            }
            ObjectivesPanel = new ObjectivesPanel(objectives, Width, X, Y + Height);
            this.entity.AddChild(ObjectivesPanel.Entity);
			ObjectivesPanel.Time = TimeSpan.FromSeconds(config.Time);
			ObjectivesPanel.Thresholds = config.Thresholds;
			ObjectivesPanel.TimeFactors = config.TimeFactors;
        }

        public Vector2 GetTileScale(int tileWidth = 100, int tileHeight= 100)
        {
            // this wasn't tested for boards that don't have square tiles. Try to use boards with square tiles.
            float scaleX = ((float)Width / (float)Columns) / (float) tileWidth;
            float scaleY = ((float)Height / (float)Rows) / (float) tileHeight;
            return new Vector2(scaleX, scaleY);
        }

        public Point GetTilePosition(int column, int row)
        {

            return new Point(X + (column * ActualTileSide) + ActualTileSide/2, Y + (row * ActualTileSide) + ActualTileSide/2);
        }

        public List<Entity> GenerateRandomBoard(string tilesSpriteSheet, string[] tiles)
        {
            _noTilesKinds = tiles.Length;
            Tiles = new Tile[Columns, Rows];
            List<Entity> entities = new List<Entity>();

            for (int i = 0; i < Columns; i++)
            {
                for(int j = 0; j < Rows; j++)
                {
                    Tile entity = GenerateRandomTile();
                    Point tilePosition = GetTilePosition(i, j);
                    entity.X = tilePosition.X;
                    entity.Y = tilePosition.Y;
                    entity.BoardColumn = i;
                    entity.BoardRow = j;

                    Tiles[i,j] = entity;
                    entities.Add(entity.Entity);
                }
            }
            List<Match> initialMatches = new List<Match>();
            do {
                initialMatches = FindMatches();
                foreach (Match m in initialMatches)
                {
                    foreach (Tile tile in m.Tiles)
                    {
                        int tileIndex = _random.Next(tiles.Length);
                        tile.Change(tileIndex);
                    }
                }
            } while (initialMatches.Count != 0);

            return entities;
        }

		public List<Match> FindMatches()
		{
			int skipTiles = 0;

			List<Match> matchesFound = new List<Match>();
			for (int j = 0; j < Rows; j++)
			{
				for (int i = 0; i < Columns - 2; i++)
				{
					if (Tiles[i, j].Index == 5)
					{
						continue;
					}
					if (Tiles[i, j].Matched)
					{
						continue;
					}
					if ((Tiles[i, j].Index == Tiles[i + 1, j].Index) && (Tiles[i, j].Index == Tiles[i + 2, j].Index) && !Tiles[i +1, j].Matched && !Tiles[i+2, j].Matched)
					{
						if (skipTiles != 0)
						{
							skipTiles--;
							continue;
						}
						Match newMatch = new Match { Tiles = new List<Tile>() { Tiles[i, j], Tiles[i + 1, j], Tiles[i + 2, j] } };
						if (i + 3 < Columns && (Tiles[i, j].Index == Tiles[i + 3, j].Index) && !Tiles[i+3, j].Matched)
						{
							newMatch.Tiles.Add(Tiles[i + 3, j]);
							skipTiles = 1;
							if (i + 4 < Rows && (Tiles[i, j].Index == Tiles[i + 4, j].Index) && !Tiles[i+4, j].Matched)
							{
								newMatch.Tiles.Add(Tiles[i + 4, j]);
								skipTiles = 2;
							}
						}
						matchesFound.Add(newMatch);
					}
				}
			}
			for (int i = 0; i < Columns; i++)
			{
				for (int j = 0; j < Rows - 2; j++)
				{
					if (Tiles[i, j].Index == 5)
					{
						continue;
					}
					if (Tiles[i, j].Matched)
					{
						continue;
					}
					if (skipTiles != 0)
					{
						skipTiles--;
						continue;
					}
					if ((Tiles[i, j].Index == Tiles[i, j + 1].Index) && (Tiles[i, j].Index == Tiles[i, j + 2].Index) && !Tiles[i, j+1].Matched && !Tiles[i, j+2].Matched)
					{
						Match newMatch = new Match { Tiles = new List<Tile>() { Tiles[i, j], Tiles[i, j + 1], Tiles[i, j + 2] } };
						if (j + 3 < Rows && (Tiles[i, j].Index == Tiles[i, j + 3].Index) && !Tiles[i, j+3].Matched)
						{
							newMatch.Tiles.Add(Tiles[i, j + 3]);
							skipTiles = 1;
							if (j + 4 < Rows && (Tiles[i, j].Index == Tiles[i, j + 4].Index) && !Tiles[i, j+4].Matched)
							{
								newMatch.Tiles.Add(Tiles[i, j + 4]);
								skipTiles = 2;
							}
						}
						matchesFound.Add(newMatch);
					}
				}
			}

			return matchesFound;
		}





		public bool IsComplete()
        {
            return ObjectivesPanel.AreObjectivesMet();
        }

        

        public Tile GenerateRandomTile()
        {
            Tile entity = null;
            if (_random.NextDouble() < ChanceSpecial1)
            {
                entity = new Special1Tile(GetTileScale());
            }
            else
            {
                int tileIndex = _random.Next(_noTilesKinds);
                entity = new Tile(tileIndex, GetTileScale());
            }
            return entity;
        }

		

		public void PutTileAtPosition(Tile tile, int column, int row)
		{
			Point tilePosition = GetTilePosition(column, row);

			tile.X = tilePosition.X;
			tile.Y = tilePosition.Y;
			tile.BoardColumn = column;
			tile.BoardRow = row;
			Tiles[column, row] = tile;
		}

		
    }
}
