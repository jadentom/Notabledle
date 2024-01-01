using System.Drawing;
using System.Linq;

namespace Notabledle.NotableModel.GameDisplay
{
    public class PropertyCellFromIconColors : IPropertyCellFromProperty
    {
        public string HeaderName => "Icon Colors";

        public string HelpText => "Prominent colors of the icon (up to 2). Choices: Red, Orange, Yellow, Green, Blue, Purple, Pink, Cyan";

        public PropertyCell PropertyCellFromProperty(Notable guessGem, Notable targetGem, int i)
        {
            var returnColorList = guessGem.IconColors.Select(a => a.ToString());
            var returnValue = new PropertyCell(string.Join(',', returnColorList));
            if (guessGem.IconColors.SetEquals(targetGem.IconColors))
            {
                returnValue.Color = DisplayColor.Green;
            }
            else if (guessGem.IconColors.Intersect(targetGem.IconColors).Any())
            {
                returnValue.Color = DisplayColor.Orange;
            }
            return returnValue;
        }
    }
}
