using System.Drawing;

namespace Notabledle.NotableModel.GameDisplay
{
    public class PropertyCellFromMasteryType : IPropertyCellFromProperty
    {
        public string HeaderName => "Mastery Type";

        public string HelpText => "The mastery type of the notable. May be 'None'";

        public PropertyCell PropertyCellFromProperty(Notable guessGem, Notable targetGem, int i) =>
            new PropertyCell(guessGem.MasteryType, targetGem.MasteryType);
    }
}
