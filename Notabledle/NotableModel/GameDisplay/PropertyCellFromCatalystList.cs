using System.Drawing;

namespace Notabledle.NotableModel.GameDisplay
{
    public class PropertyCellFromCatalystList : IPropertyCellFromProperty
    {
        public string HeaderName => "Catalyst List";

        public string HelpText => "The catalysts which would be applicable to a notable were its stats granted by a piece of jewelry. E.G Fertile, Accelerating";

        public PropertyCell PropertyCellFromProperty(Notable guessGem, Notable targetGem, int i)
        {
            var returnCatalystList = guessGem.ApplicableCatalysts.Select(a => a.ToString());
            var catalystString = returnCatalystList.Any() ? string.Join(',', returnCatalystList) : "None";
            var returnValue = new PropertyCell(catalystString);
            if (guessGem.ApplicableCatalysts.SetEquals(targetGem.ApplicableCatalysts))
            {
                returnValue.Color = DisplayColor.Green;
            }
            else if (guessGem.ApplicableCatalysts.Intersect(targetGem.ApplicableCatalysts).Any())
            {
                returnValue.Color = DisplayColor.Orange;
            }
            return returnValue;
        }
    }
}
