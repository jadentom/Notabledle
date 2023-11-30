using System.Drawing;

namespace Notabledle.NotableModel.GameDisplay
{
    public class PropertyCellFromCatalystList : IPropertyCellFromProperty
    {
        public string HeaderName => "Catalyst List";

        public PropertyCell PropertyCellFromProperty(Notable guessGem, Notable targetGem, int i)
        {
            var returnCatalystList = guessGem.ApplicableCatalysts.Select(a => a.ToString());
            var returnValue = new PropertyCell(string.Join(',', returnCatalystList));
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
