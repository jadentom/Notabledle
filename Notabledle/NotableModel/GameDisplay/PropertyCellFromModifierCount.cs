using System.Drawing;

namespace Notabledle.NotableModel.GameDisplay
{
    public class PropertyCellFromModifierCount : IPropertyCellFromProperty
    {
        public string HeaderName => "Modifier Count";

        public string HelpText => "The count of separate modifiers on a notable. Includes a mastery if it exists.";

        public PropertyCell PropertyCellFromProperty(Notable guessGem, Notable targetGem, int i) =>
            new PropertyCell(guessGem.ModifierCount, targetGem.ModifierCount, true);
    }
}
