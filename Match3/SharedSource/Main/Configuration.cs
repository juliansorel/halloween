using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Match3
{
    public class BoardConfiguration
    {
        public int Columns { get; set; }
        public int Rows { get; set; }
        public int Tiles { get; set; }
        public double Special1Chance { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    class Configuration
    {


        public List<Level> Levels { get; private set; }

        public void ReadConfiguration()
        {
            Levels = new List<Level>();

            System.Xml.XmlDocument doc = new XmlDocument();
            doc.Load("Config.xml");
            foreach(XmlNode levelNode in doc.DocumentElement.SelectNodes("/levels/level"))
            {
                Level newLevel = new Level();
                newLevel.Boards = new List<BoardConfiguration>();
                foreach (XmlNode boardNode in levelNode.SelectNodes("./boards/board"))
                {
                    BoardConfiguration newBoard = new BoardConfiguration();

                    XmlNode columnsNode = boardNode.SelectSingleNode("./columns");
                    newBoard.Columns = Convert.ToInt32(columnsNode.InnerText);

                    XmlNode rowsNode = boardNode.SelectSingleNode("./rows");
                    newBoard.Rows = Convert.ToInt32(rowsNode.InnerText);

                    XmlNode tilesNode = boardNode.SelectSingleNode("./tiles");
                    newBoard.Tiles = Convert.ToInt32(tilesNode.InnerText);

                    XmlNode special1Node = boardNode.SelectSingleNode("./chanceSpecial1");
                    newBoard.Special1Chance = Convert.ToDouble(special1Node.InnerText);

                    XmlNode xNode = boardNode.SelectSingleNode("./x_position");
                    newBoard.X = Convert.ToInt32(xNode.InnerText);

                    XmlNode yNode = boardNode.SelectSingleNode("./y_position");
                    newBoard.Y = Convert.ToInt32(yNode.InnerText);

                    XmlNode widthNode = boardNode.SelectSingleNode("./width");
                    newBoard.Width = Convert.ToInt32(widthNode.InnerText);

                    XmlNode heightNode = boardNode.SelectSingleNode("./height");
                    newBoard.Height = Convert.ToInt32(heightNode.InnerText);

                    newLevel.Boards.Add(newBoard);
                }

                XmlNode timeNode = levelNode.SelectSingleNode("./timeSec");
                newLevel.TimeSec = Convert.ToInt32(timeNode.InnerText);

                Levels.Add(newLevel);
            }

            




		}
    }
}
