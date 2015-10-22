﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Match3
{
    class Configuration
    {
        public int Columns { get; private set; }
        public int Rows { get; private set; }
        public int Tiles { get; private set; }

        public void ReadConfiguration()
        {
            System.Xml.XmlDocument doc = new XmlDocument();
            doc.Load("Config.xml");
            XmlNode columnsNode = doc.DocumentElement.SelectSingleNode("/board/columns");

            Columns = Convert.ToInt32(columnsNode.InnerText);
            XmlNode rowsNode = doc.DocumentElement.SelectSingleNode("/board/rows");
            Rows = Convert.ToInt32(rowsNode.InnerText);


            XmlNode tilesNode = doc.DocumentElement.SelectSingleNode("/board/tiles");
            Tiles = Convert.ToInt32(tilesNode.InnerText);
        }
    }
}