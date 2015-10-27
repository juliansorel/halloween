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
		
        private int _selectedTileColumn = -1;
        private int _selectedTileRow = -1;
        private Random _random = new Random();
        private int _noTilesKinds;
        private double chanceSpecial1 = 0.0;



        public Board(int x, int y, int width, int height, int columns, int rows, int tileSide)
        {

            X = x;
            Y = y;
            Width = width;
            Height = height;
            Columns = columns;
            Rows = rows;
            TileSide = tileSide;
            ActualTileSide = (int)(GetTileScale(TileSide, TileSide).X * TileSide);
            this.entity = new Entity() { Name = "Board" }.AddComponent(new BoardBehavior(this));
            this.Tag = "board";
            Configuration config = new Configuration();
            config.ReadConfiguration();
            chanceSpecial1 = config.Levels[0].Boards[0].Special1Chance;
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

        public void SelectTile(int column, int row)
        {
			if (Tiles[column,row].Index == 5)
			{
				if (_selectedTileColumn != -1)
				{
					Tiles[_selectedTileColumn, _selectedTileRow].Selected = false;
					_selectedTileColumn = -1;
				}
				this.entity.FindComponent<BoardBehavior>().RemoveEntity(Tiles[column, row].Entity);
				Tiles[column, row] = null;
				ShuffleTiles(column, row);
				DropTiles();
				FindMatchesScoreAndDropTiles();

				return;
			}
            if (_selectedTileColumn != -1)
            {
                Tiles[_selectedTileColumn, _selectedTileRow].Selected = false;
                // swap adjacent tiles
                if ((Math.Abs(column - _selectedTileColumn) ==1 && row == _selectedTileRow) ||
                       (Math.Abs(row - _selectedTileRow) == 1 && column == _selectedTileColumn))
                {
                    SwapTiles(column, row, _selectedTileColumn, _selectedTileRow);
                    List<Match> matchesAfterSwapping = FindMatches();
                    if (matchesAfterSwapping.Count == 0)
                    {
                        SwapTiles(column, row, _selectedTileColumn, _selectedTileRow);
                    } else
                    {
						FindMatchesScoreAndDropTiles(matchesAfterSwapping);
                        
                    }
                    _selectedTileColumn = -1;
                }
                else
                {
                    _selectedTileColumn = column;
                    _selectedTileRow = row;
                    Tiles[column, row].Selected = true;
                }
            }
            else
            {
                _selectedTileColumn = column;
                _selectedTileRow = row;
                Tiles[column, row].Selected = true;
            }
        }

        public void SwapTiles(int column1, int row1, int column2, int row2)
        {
			Tile firstTile = Tiles[column1, row1];
			Tile secondTile = Tiles[column2, row2];
			PutTileAtPosition(firstTile, column2, row2);
			PutTileAtPosition(secondTile, column1, row1);
        }

        public List<Match> FindMatches()
        {
            int skipTiles = 0;
            
            List<Match> matchesFound = new List<Match>();
            for (int j = 0; j < Rows; j++)
            {
                for (int i = 0; i < Columns - 2; i++)
                {
					if (Tiles[i,j].Index == 5)
					{
						continue;
					}
                    if ((Tiles[i,j].Index == Tiles[i+1,j].Index) && (Tiles[i, j].Index == Tiles[i+2,j].Index))
                    {
                        if (skipTiles != 0)
                        {
                            skipTiles--;
                            continue;
                        }
                        Match newMatch = new Match { Tiles = new List<Tile>() { Tiles[i, j], Tiles[i + 1, j], Tiles[i + 2, j] } };
                        if (i+3 < Columns && (Tiles[i, j].Index == Tiles[i + 3, j].Index))
                        {
                            newMatch.Tiles.Add(Tiles[i + 3, j]);
                            skipTiles = 1;
                            if (i + 4 < Rows && (Tiles[i, j].Index == Tiles[i+4, j].Index))
                            {
                                newMatch.Tiles.Add(Tiles[i+4, j]);
                                skipTiles = 2;
                            }
                        }
                        matchesFound.Add(newMatch);
                    }
                }
            }
            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows-2; j++)
                {
					if (Tiles[i, j].Index == 5)
					{
						continue;
					}
					if (skipTiles != 0)
                    {
                        skipTiles--;
                        continue;
                    }
                    if ((Tiles[i, j].Index == Tiles[i, j + 1].Index) && (Tiles[i, j].Index == Tiles[i, j + 2].Index))
                    {
                        Match newMatch = new Match { Tiles = new List<Tile>() { Tiles[i, j], Tiles[i, j + 1], Tiles[i, j + 2] } };
                        if (j + 3 < Rows && (Tiles[i, j].Index == Tiles[i, j+3].Index))
                        {
                            newMatch.Tiles.Add(Tiles[i, j+3]);
                            skipTiles = 1;
                            if (j+4 < Rows && (Tiles[i, j].Index == Tiles[i, j + 4].Index))
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

        public void DropTiles()
        {
            for (int j = 0; j < Rows; j++)
            {
                for (int i = 0; i< Columns; i++)
                {
                    if (Tiles[i,j] == null)
                    {
                        for (int k = j-1; k >= 0; k--)
                        {
                            if (Tiles[i, k] != null)
                            {
                                Tiles[i, k + 1] = Tiles[i, k];
                                Tiles[i, k] = null;
                                Point newTilePosition = GetTilePosition(i, k+1);
                                Tiles[i, k+1].Entity.FindComponent<Transform2D>().X = newTilePosition.X;
                                Tiles[i, k+1].Entity.FindComponent<Transform2D>().Y = newTilePosition.Y;
                                Tiles[i, k + 1].BoardRow = k + 1;
                            }
                        }
                        var entity = GenerateRandomTile();
						PutTileAtPosition(entity, i, 0);
                        //Fix this: 
                        this.entity.FindComponent<BoardBehavior>().AddEntity(Tiles[i, 0].Entity);
                    }
                }
            }
        }

        private Tile GenerateRandomTile()
        {
            Tile entity = null;
            if (_random.NextDouble() < chanceSpecial1)
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

		private void FindMatchesScoreAndDropTiles(List<Match> initialMatches = null)
		{

			List<Match> matchesAfterSwapping = initialMatches??FindMatches();
			while (matchesAfterSwapping.Count != 0)
			{
				foreach (Match match in matchesAfterSwapping)
				{
					switch (match.Tiles.Count)
					{
						case 3:
							((MyScene)this.Entity.Scene).scoreboardPanel.Scores += 1;
							break;
						case 4:
							((MyScene)this.Entity.Scene).scoreboardPanel.Scores += 5;
							break;
						case 5:
							((MyScene)this.Entity.Scene).scoreboardPanel.Scores += 20;
							break;
						default:
							((MyScene)this.Entity.Scene).scoreboardPanel.Scores += 1;
							break;
					}
					foreach (Tile tile in match.Tiles)
					{
						if (Tiles[tile.BoardColumn, tile.BoardRow] != null)
						{
							// TODO: Fix this:
							this.entity.FindComponent<BoardBehavior>().RemoveEntity(Tiles[tile.BoardColumn, tile.BoardRow].Entity);
						}
						Tiles[tile.BoardColumn, tile.BoardRow] = null;
					}
				}
				// TEMPORARY
				DropTiles();
				matchesAfterSwapping = FindMatches();
			}
		}

		private void PutTileAtPosition(Tile tile, int column, int row)
		{
			Point tilePosition = GetTilePosition(column, row);

			tile.X = tilePosition.X;
			tile.Y = tilePosition.Y;
			tile.BoardColumn = column;
			tile.BoardRow = row;
			Tiles[column, row] = tile;
		}

		private void ShuffleTiles(int column, int row)
		{
			List<Tuple<int, int>> positionToShuffle = new List<Tuple<int, int>>()
			{
				new Tuple<int, int>( column -1, row-1 ), new Tuple<int, int>( column, row-1 ), new Tuple<int, int>( column +1, row-1 ),
				new Tuple<int, int>( column -1, row ),                                         new Tuple<int, int>( column +1, row ),
                new Tuple<int, int>( column -1, row+1 ), new Tuple<int, int>( column, row+1 ), new Tuple<int, int>( column +1, row+1 )
			};
			List<Tile> tilesToShuffle = new List<Tile>();

			for (int i = positionToShuffle.Count -1; i >= 0; i--)
			{
				if (positionToShuffle[i].Item1 < 0 || positionToShuffle[i].Item1 > Columns-1 ||
					positionToShuffle[i].Item2 < 0 || positionToShuffle[i].Item2 > Rows-1)
				{
					positionToShuffle.RemoveAt(i);
				}
				else
				{
					tilesToShuffle.Add(Tiles[positionToShuffle[i].Item1, positionToShuffle[i].Item2]);
				}

			}
			foreach (var position in positionToShuffle)
			{
				int randomTileIndex = _random.Next(tilesToShuffle.Count);
				PutTileAtPosition(tilesToShuffle[randomTileIndex], position.Item1, position.Item2);
				tilesToShuffle.RemoveAt(randomTileIndex);
			}
		}
    }
}
