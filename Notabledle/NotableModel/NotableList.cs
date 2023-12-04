using Notabledle.NotableModel.PropertyEnums;
using System.Drawing;

namespace Notabledle.NotableModel
{
    public static class NotableList
    {
        // Resources:
        // File: https://github.com/poe-tool-dev/passive-skill-tree-json/blob/master/3.21.0/SkillTree.json
        // Raw: https://raw.githubusercontent.com/poe-tool-dev/passive-skill-tree-json/master/3.21.0/SkillTree.json
        // How to parse: https://www.pathofexile.com/forum/view-thread/167255#p2097992
        // Double check: https://poeplanner.com/

        public static readonly List<Notable> Value = new List<Notable>()
        {
            new Notable(
                "Born to Fight",
                TreeArea.Marauder,
                MasteryType.None,
                new HashSet<Catalysts>{ Catalysts.Abrasive, Catalysts.Accelerating, Catalysts.Intrinsic, Catalysts.Noxious },
                new HashSet<KnownColor>{ KnownColor.Pink, KnownColor.Blue },
                5,
                3,
                1,
                new OilRecipe(BlightOil.Golden, BlightOil.Violet, BlightOil.Violet)),
            new Notable(
                "Anointed Flesh",
                TreeArea.Templar,
                MasteryType.Protection,
                new HashSet<Catalysts>{ Catalysts.Prismatic },
                new HashSet<KnownColor>{ KnownColor.Blue, KnownColor.Red },
                8,
                3,
                5,
                new OilRecipe(BlightOil.Golden, BlightOil.Golden, BlightOil.Violet)),
            new Notable(
                "Veteran Soldier",
                TreeArea.Scion,
                MasteryType.Physical,
                new HashSet<Catalysts>{ Catalysts.Fertile, Catalysts.Noxious },
                new HashSet<KnownColor>{ KnownColor.Red },
                9,
                2,
                5,
                new OilRecipe(BlightOil.Golden, BlightOil.Violet, BlightOil.Teal)),
        };

        public static readonly Lazy<Dictionary<string, Notable>> AsDictionary =
            new Lazy<Dictionary<string, Notable>>(() => Value.ToDictionary(v => v.Name, v => v));
    }
}
