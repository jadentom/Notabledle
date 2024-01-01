using System.Drawing;

namespace Notabledle.NotableModel.GameDisplay
{
    public class PropertyCellFromGroupNodeCount : IPropertyCellFromProperty
    {
        public string HeaderName => "Group Node Count";

        public string HelpText => "The number of nodes in the notable's group";

        public PropertyCell PropertyCellFromProperty(Notable guessGem, Notable targetGem, int i) =>
            new PropertyCell(guessGem.GroupNodeCount, targetGem.GroupNodeCount, true);
    }
}
