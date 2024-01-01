namespace Notabledle.NotableModel.GameDisplay
{
    public class PropertyCellFromName : IPropertyCellFromProperty
    {
        public string HeaderName => "Name";

        public string HelpText => "The name of the notable.";

        public PropertyCell PropertyCellFromProperty(Notable guessGem, Notable targetGem, int i) =>
            new PropertyCell(guessGem.Name){ Color = DisplayColor.Transparent };
    }
}
