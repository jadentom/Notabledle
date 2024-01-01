using System.Drawing;
using System.Linq;

namespace Notabledle.NotableModel.GameDisplay
{
    public class PropertyCellFromOilRecipe : IPropertyCellFromProperty
    {
        public string HeaderName => "Oil Recipe";

        public string HelpText => "The oil recipe (highest tier first). The notables are also compared in order and if the highest tier is the same, the guess will be orange.";

        public PropertyCell PropertyCellFromProperty(Notable guessGem, Notable targetGem, int i)
        {
            var guessOilRecipe = guessGem.OilRecipe;
            var targetOilRecipe = targetGem.OilRecipe;
            var oilString = guessGem.OilRecipe.ToString();
            var guessOilInt = (int)guessOilRecipe.LargestOil * 100 * 100
                + (int)guessOilRecipe.SecondOil * 100
                + (int)guessOilRecipe.ThirdOil;
            var targetOilInt = (int)targetOilRecipe.LargestOil * 100 * 100
                + (int)targetOilRecipe.SecondOil * 100
                + (int)targetOilRecipe.ThirdOil;
            if (guessOilInt == targetOilInt)
            {
                var matchCell = new PropertyCell(oilString)
                {
                    Color = DisplayColor.Green
                };
                return matchCell;
            }

            var upDownString = guessOilInt < targetOilInt ? " ↑" : " ↓";
            var returnValue = new PropertyCell(oilString + upDownString);
            if (guessOilRecipe.LargestOil == targetOilRecipe.LargestOil)
            {
                returnValue.Color = DisplayColor.Orange;
            }
            return returnValue;
        }
    }
}
