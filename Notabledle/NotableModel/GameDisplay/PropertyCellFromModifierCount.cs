using System.Drawing;

namespace Notabledle.NotableModel.GameDisplay
{
    public class PropertyCellFromModifierCount : IPropertyCellFromProperty
    {
        public string HeaderName => "Modifier Count";

        public PropertyCell PropertyCellFromProperty(Notable guessGem, Notable targetGem, int i) =>
            new PropertyCell(guessGem.ModifierCount, targetGem.ModifierCount, true);
    }
}
