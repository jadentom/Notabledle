using Notabledle.NotableModel.PropertyEnums;
using System.ComponentModel;
using System.Drawing;

namespace Notabledle.NotableModel
{
    public class Notable
    {
        public Notable(
            int id,
            string name,
            string[] description,
            TreeArea treeArea,
            MasteryType masteryType,
            HashSet<Catalyst> applicableCatalysts,
            HashSet<KnownColor> iconColors,
            int startingDistance,
            int groupNodeCount,
            OilRecipe oilRecipe)
        {
            Id = id;
            Name = name;
            Description = description;
            TreeArea = treeArea;
            MasteryType = masteryType;
            // There could be no applicable catalysts
            ApplicableCatalysts = applicableCatalysts;
            if (!iconColors.Any())
            {
                throw new ArgumentException($"{name} has no {nameof(iconColors)}");
            }
            if (iconColors.Any(c => !allowedIconColors.Contains(c)))
            {
                throw new InvalidEnumArgumentException($"{name} has invalid {nameof(iconColors)}");
            }
            IconColors = iconColors;
            StartingDistance = startingDistance;
            GroupNodeCount = groupNodeCount;
            OilRecipe = oilRecipe;
        }

        /// <summary>
        /// The numeric id of the notable
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The name of the notable
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The description of the notable
        /// </summary>
        public string[] Description { get; }

        /// <summary>
        /// The closest starting area to the notable.
        /// </summary>
        /// <remarks>
        /// Scion is anything inside the jewels between all other class starting areas
        /// Dividing lines between all other classes are large jewel socket attribute lines
        /// It's possible for a different class to be the closest in node distance.
        /// </remarks>
        public TreeArea TreeArea { get; }

        /// <summary>
        /// Mastery type. None if the wheel doesn't have a mastery (e.g charge/starting area)
        /// </summary>
        public MasteryType MasteryType { get; }

        /// <summary>
        /// The catalysts that would apply to the mods granted by the notable.
        /// If a notable with an unscalable mod like Cleansed Thoughts (double chaos resistance), still say that a prismatic would apply
        /// </summary>
        public HashSet<Catalyst> ApplicableCatalysts { get; }

        /// <summary>
        /// The color of the icon.
        /// </summary>
        public HashSet<KnownColor> IconColors { get; }

        static readonly List<KnownColor> allowedIconColors = new List<KnownColor>()
        {
            // Primary
            KnownColor.Red,
            KnownColor.Green,
            KnownColor.Blue,
            KnownColor.Yellow,
            KnownColor.Purple,
            KnownColor.Cyan,
            // Other common
            KnownColor.Orange,
            KnownColor.Pink,
        };

        /// <summary>
        /// The shortest distance from the starting node of <see cref="TreeArea"/>
        /// </summary>
        public int StartingDistance { get; }

        /// <summary>
        /// The count of mods in the group
        /// The JSON will be the final arbiter
        /// </summary>
        public int GroupNodeCount { get; }

        /// <summary>
        /// The oil recipe
        /// </summary>
        public OilRecipe OilRecipe { get; }
    }
}
