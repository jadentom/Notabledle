using System.Drawing;

namespace Notabledle.NotableModel.GameDisplay
{
    public class PropertyCellFromIcon : IPropertyCellFromProperty
    {
        public string HeaderName => "Icon";

        public string HelpText => "The notable icon.";

        public PropertyCell PropertyCellFromProperty(Notable guessGem, Notable targetGem, int i) =>
            new PropertyCell($"images/notable_{guessGem.Id}.webp");
    }
}
