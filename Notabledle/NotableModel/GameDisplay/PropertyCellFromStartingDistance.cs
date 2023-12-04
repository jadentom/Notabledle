using System.Drawing;

namespace Notabledle.NotableModel.GameDisplay
{
    public class PropertyCellFromStartingDistance : IPropertyCellFromProperty
    {
        public string HeaderName => "Distance from start";

        public PropertyCell PropertyCellFromProperty(Notable guessGem, Notable targetGem, int i) =>
            new PropertyCell(guessGem.StartingDistance, targetGem.StartingDistance, true);
    }
}
