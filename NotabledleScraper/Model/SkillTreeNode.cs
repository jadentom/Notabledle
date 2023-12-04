using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NotabledleScraper.Model
{
    internal class SkillTreeNode
    {
        //"5624": {
        //    "skill": 5624,
        //    "name": "Crusader",
        //    "icon": "Art/2DArt/SkillIcons/passives/MindPact.png",
        //    "isNotable": true,
        //    "isBlighted": true,
        //    "recipe": [
        //        "BlackOil",
        //        "GoldenOil",
        //        "GoldenOil"
        //    ],
        //    "stats": [
        //        "8% increased maximum Mana",
        //        "Transfiguration of Mind"
        //    ],
        //    "reminderText": [
        //        "(Increases and Reductions to Maximum Mana also apply to Damage at 30% of their value)"
        //    ],
        //    "group": 22,
        //    "orbit": 0,
        //    "orbitIndex": 0,
        //    "out": [],
        //    "in": []
        //},
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("recipe")]
        public string[]? Recipe { get; set; }

        [JsonPropertyName("group")]
        public int GroupId { get; set; }

        [JsonPropertyName("stats")]
        public string[]? Stats { get; set; }

        [JsonPropertyName("isMastery")]
        public bool IsMastery { get; set; }
    }
}
