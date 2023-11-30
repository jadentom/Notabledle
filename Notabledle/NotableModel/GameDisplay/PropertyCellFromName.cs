namespace Notabledle.NotableModel.GameDisplay
{
    public class PropertyCellFromName : IPropertyCellFromProperty
    {
        public string HeaderName => "Name";

        public PropertyCell PropertyCellFromProperty(Notable guessGem, Notable targetGem, int i) =>
            new PropertyCell(guessGem.Name){ Color = DisplayColor.Transparent };
    }
}
