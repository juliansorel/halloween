using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;

namespace Match3
{
    class BoardBehavior : Behavior
    {
        private Board _parent;
        private bool _pressed;
		private ObjectivesPanel _objectivesPanel;
		private int _selectedTileColumn = -1;
		private int _selectedTileRow = -1;
		private System.Random _random = new System.Random();

		public BoardBehavior(Board parent)
        {
            _parent = parent;
			
        }

        protected override void Update(System.TimeSpan gameTime)
        {
			//Trace.WriteLine("a");
			var mouseState = WaveServices.Input.MouseState;

			for (int i = 0; i < _parent.Columns; i++)
			{
				for (int j = 0; j < _parent.Rows; j++)
				{
					if (_parent.Tiles[i, j].Gone)
					{
						_parent.Tiles[i, j] = null;
					}
					DropTiles();
				}
			}
			FindMatchesScoreAndDropTiles();
            if (mouseState.X > _parent.X && mouseState.X < _parent.X + _parent.Width &&
                mouseState.Y > _parent.Y && mouseState.Y < _parent.Y + _parent.Height)
            {
                if(_pressed && mouseState.LeftButton == WaveEngine.Common.Input.ButtonState.Release)
                {
                    _pressed = false;
                    int tileColumn = (int)Math.Floor((mouseState.X - _parent.X) / (100 * _parent.GetTileScale().X));
                    int tileRow = (int)Math.Floor((mouseState.Y - _parent.Y) / (100 * _parent.GetTileScale().Y));
                    SelectTile(tileColumn, tileRow);
                }
                else if (mouseState.LeftButton == WaveEngine.Common.Input.ButtonState.Pressed)
                {
                    _pressed = true;
                }
            }

        }

        protected override void ResolveDependencies()
        {
			_objectivesPanel = _parent.ObjectivesPanel;
		}

		public void SelectTile(int column, int row)
		{
			if (_parent.Tiles[column, row].Index == 5)
			{
				if (_selectedTileColumn != -1)
				{
					_parent.Tiles[_selectedTileColumn, _selectedTileRow].Selected = false;
					_selectedTileColumn = -1;
				}
				_parent.Tiles[column, row].Matched = true;
				ShuffleTiles(column, row);

				return;
			}
			if (_selectedTileColumn != -1)
			{
				_parent.Tiles[_selectedTileColumn, _selectedTileRow].Selected = false;
				// swap adjacent tiles
				if ((Math.Abs(column - _selectedTileColumn) == 1 && row == _selectedTileRow) ||
					   (Math.Abs(row - _selectedTileRow) == 1 && column == _selectedTileColumn))
				{
					SwapTiles(column, row, _selectedTileColumn, _selectedTileRow);
					List<Match> matchesAfterSwapping = _parent.FindMatches();
					if (matchesAfterSwapping.Count == 0)
					{
						SwapTiles(column, row, _selectedTileColumn, _selectedTileRow);
					}
					_selectedTileColumn = -1;
				}
				else
				{
					_selectedTileColumn = column;
					_selectedTileRow = row;
					_parent.Tiles[column, row].Selected = true;
				}
			}
			else
			{
				_selectedTileColumn = column;
				_selectedTileRow = row;
				_parent.Tiles[column, row].Selected = true;
			}
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

			for (int i = positionToShuffle.Count - 1; i >= 0; i--)
			{
				if (positionToShuffle[i].Item1 < 0 || positionToShuffle[i].Item1 > _parent.Columns - 1 ||
					positionToShuffle[i].Item2 < 0 || positionToShuffle[i].Item2 > _parent.Rows - 1)
				{
					positionToShuffle.RemoveAt(i);
				}
				else
				{
					tilesToShuffle.Add(_parent.Tiles[positionToShuffle[i].Item1, positionToShuffle[i].Item2]);
				}

			}
			foreach (var position in positionToShuffle)
			{
				int randomTileIndex = _random.Next(tilesToShuffle.Count);
				_parent.PutTileAtPosition(tilesToShuffle[randomTileIndex], position.Item1, position.Item2);
				tilesToShuffle.RemoveAt(randomTileIndex);
			}
		}

		public void DropTiles()
		{
			for (int j = 0; j < _parent.Rows; j++)
			{
				for (int i = 0; i < _parent.Columns; i++)
				{
					if (_parent.Tiles[i, j] == null)
					{
						for (int k = j - 1; k >= 0; k--)
						{
							if (_parent.Tiles[i, k] != null)
							{
								_parent.Tiles[i, k + 1] = _parent.Tiles[i, k];
								_parent.Tiles[i, k] = null;
								WaveEngine.Common.Math.Point newTilePosition = _parent.GetTilePosition(i, k + 1);
								_parent.Tiles[i, k + 1].Entity.FindComponent<Transform2D>().X = newTilePosition.X;
								_parent.Tiles[i, k + 1].Entity.FindComponent<Transform2D>().Y = newTilePosition.Y;
								_parent.Tiles[i, k + 1].BoardRow = k + 1;
							}
						}
						var entity = _parent.GenerateRandomTile();
						_parent.PutTileAtPosition(entity, i, 0);
						//Fix this: 
						AddEntity(_parent.Tiles[i, 0].Entity);
					}
				}
			}
		}



		private void FindMatchesScoreAndDropTiles(List<Match> initialMatches = null)
		{

			List<Match> matchesAfterSwapping = initialMatches ?? _parent.FindMatches();
			foreach (Match match in matchesAfterSwapping)
			{
				_objectivesPanel.UpdateObjectives(match);
				if (_parent.MatchRewards.ContainsKey(match.Tiles.Count))
				{
					_objectivesPanel.Time += TimeSpan.FromSeconds(_parent.MatchRewards[match.Tiles.Count]);
				}
				foreach (Tile tile in match.Tiles)
				{
					tile.Matched = true;
				}
			}

		}

		public void SwapTiles(int column1, int row1, int column2, int row2)
		{
			Tile firstTile = _parent.Tiles[column1, row1];
			Tile secondTile = _parent.Tiles[column2, row2];
			_parent.PutTileAtPosition(firstTile, column2, row2);
			_parent.PutTileAtPosition(secondTile, column1, row1);
		}

		

		internal void RemoveEntity(Entity entity)
        {
            if (EntityManager != null)
            {
                EntityManager.Remove(entity);
            }
        }

        internal void AddEntity(Entity entity)
        {
            if (EntityManager != null)
            {
                EntityManager.Add(entity);
            }
        }
    }
}
