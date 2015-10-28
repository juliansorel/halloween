using System;
using System.Collections.Generic;
using System.Text;

namespace Match3
{
    class Objective
    {
        public int TileIndex { get; private set; }
        public int AmountLeft  { get; private set; }
        public string Name { get; set; }

        public Objective(int tileIndex, int requiredAmount, string name)
        {
            TileIndex = tileIndex;
            AmountLeft = requiredAmount;
            Name = name;
        }

        public bool IsMet()
        {
            return AmountLeft <= 0;
        }

        public void Update(Match matchMade)
        {
            if (TileIndex == matchMade.Tiles[0].Index)
            {
                AmountLeft -= matchMade.Tiles.Count;
            }
        }
    }
}
