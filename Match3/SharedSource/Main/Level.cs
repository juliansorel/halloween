using System;
using System.Collections.Generic;
using System.Text;

namespace Match3
{
    class Level
    {
        public List<BoardConfiguration> Boards { get; set; }
		public Dictionary<int, int> MatchRewards { get; set; }
    }
}
