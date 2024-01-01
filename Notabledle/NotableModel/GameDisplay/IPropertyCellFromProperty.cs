namespace Notabledle.NotableModel.GameDisplay
{
    public interface IPropertyCellFromProperty
    {
        string HeaderName { get; }

        string HelpText { get; }

        PropertyCell PropertyCellFromProperty(Notable guessGem, Notable targetGem, int i);
    }
}
