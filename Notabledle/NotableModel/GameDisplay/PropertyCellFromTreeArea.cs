using System.Drawing;

namespace Notabledle.NotableModel.GameDisplay
{
    public class PropertyCellFromTreeArea : IPropertyCellFromProperty
    {
        public string HeaderName => "Tree Area";

        public string HelpText => "The starting area boundaries which the notable is within. This is dependent on the spatial position of the notable only. E.G Scion, Marauder, Shadow";

        public PropertyCell PropertyCellFromProperty(Notable guessGem, Notable targetGem, int i) =>
            new PropertyCell(guessGem.TreeArea, targetGem.TreeArea);
    }
}
