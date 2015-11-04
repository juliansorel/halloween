using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Match3
{
    public class ObjectiveConfiguration
    {
        public int TileIndex { get; set; }
        public int RequiredAmount { get; set; }
        public string Name { get; set; }
    }

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
        public string Name { get; set; }
        public List<ObjectiveConfiguration> Objectives { get; set; }
		public int Time { get; set; }
		public List<int> Thresholds { get; set; }
		public List<double> TimeFactors { get; set; }
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
				newLevel.MatchRewards = new Dictionary<int, int>();
				foreach (XmlNode matchReward in levelNode.SelectNodes("./matchRewards/reward"))
				{
					int match = Convert.ToInt32(matchReward.Attributes["Match"].Value);
					int reward = Convert.ToInt32(matchReward.InnerText);
					newLevel.MatchRewards.Add(match, reward);
				}

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

					XmlNode timeNode = boardNode.SelectSingleNode("./time");
					newBoard.Time = Convert.ToInt32(timeNode.InnerText);

					newBoard.Name = boardNode.Attributes["Id"].Value;

                    newBoard.Objectives = new List<ObjectiveConfiguration>();
                    foreach (XmlNode objective in boardNode.SelectSingleNode("./objectives").ChildNodes)
                    {
                        if(objective.Name == "tile_objective")
                        {
                            ObjectiveConfiguration newObjective = new ObjectiveConfiguration()
                            {
                                Name = objective.Attributes["Name"].Value,
                                TileIndex = Convert.ToInt32(objective.Attributes["Index"].Value),
                                RequiredAmount = Convert.ToInt32(objective.Attributes["Amount"].Value),
                            };
                            newBoard.Objectives.Add(newObjective);
                        }
                    }

					newBoard.Thresholds = new List<int>();
					newBoard.TimeFactors = new List<double>();
					foreach (XmlNode threshold in boardNode.SelectSingleNode("./timeThresholds").ChildNodes)
					{
						newBoard.Thresholds.Add(Convert.ToInt32(threshold.Attributes["After"].Value));
						newBoard.TimeFactors.Add(Convert.ToDouble(threshold.InnerText));
					}

                    newLevel.Boards.Add(newBoard);
                }

                Levels.Add(newLevel);
            }

            




		}
    }
}
