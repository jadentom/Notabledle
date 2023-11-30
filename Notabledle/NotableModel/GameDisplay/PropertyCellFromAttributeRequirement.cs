using System.Drawing;

namespace Notabledle.NotableModel.GameDisplay
{
    public class PropertyCellFromTreeArea : IPropertyCellFromProperty
    {
        public string HeaderName => "Tree Area";

        public PropertyCell PropertyCellFromProperty(Notable guessGem, Notable targetGem, int i) =>
            new PropertyCell(guessGem.TreeArea, targetGem.TreeArea);
    }
}
