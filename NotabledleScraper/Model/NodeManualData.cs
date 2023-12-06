using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NotabledleScraper.Model
{
    internal class NodeManualData
    {
        public string Name { get; set; }
        public string TreeArea { get; set; }

        public string[] Catalysts { get; set; } = new string[]
        {
            "Abrasive",
            "Accelerating",
            "Fertile",
            "Imbued",
            "Intrinsic",
            "Noxious",
            "Prismatic",
            "Defence",
            "Turbulent",
            "Unstable",
        };

        public string[] Colors { get; set; } = new string[]
        {
            "Pink",
            "Red",
            "Orange",
            "Yellow",
            "Green",
            "Cyan",
            "Blue",
            "Purple",
        };

        public int Distance { get; set; } = -1;
    }
}
