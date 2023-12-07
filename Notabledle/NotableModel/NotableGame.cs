using Notabledle.NotableModel.GameDisplay;

namespace Notabledle.NotableModel
{
    public class NotableGame
    {
        // TODO:
        // Alphabetical order of notables
        // Manually populate populated_node_manual_stats.json
        // CI/CD
        // Playtesting
        //   Ordering
        //   Difficulty
        //   Other properties
        // Add new notable each day/free play feature
        // Prettify UI with CSS
        Notable targetGem = NotableList.Value[2]; // Adamant in Duelist
        List<Notable> notableGuesses = new List<Notable>();
        List<IPropertyCellFromProperty> propertyCellFromProperties = new List<IPropertyCellFromProperty>()
        {
            new PropertyCellFromRowCount(),
            new PropertyCellFromName(),
            new PropertyCellFromTreeArea(),
            new PropertyCellFromMasteryType(),
            new PropertyCellFromCatalystList(),
            new PropertyCellFromIconColors(),
            new PropertyCellFromStartingDistance(),
            new PropertyCellFromModifierCount(),
            new PropertyCellFromGroupNodeCount(),
            new PropertyCellFromOilRecipe(),
        };

        public NotableGame() { }

        public List<List<PropertyCell>> GuessNotable(string notableName)
        {
            if (NotableList.AsDictionary.Value.TryGetValue(notableName, out var notable))
            {
                notableGuesses.Add(notable);
            }
            return DisplayCellsFromGuesses(notableGuesses, targetGem);
        }

        List<List<PropertyCell>> DisplayCellsFromGuesses(List<Notable> notableGuesses, Notable targetGem)
        {
            var returnValue = new List<List<PropertyCell>>();
            for (var i = 0; i < notableGuesses.Count; i++)
            {
                var rowGuess = notableGuesses[i];
                var row = propertyCellFromProperties.Select(p => p.PropertyCellFromProperty(rowGuess, targetGem, i)).ToList();
                returnValue.Add(row);
            }
            return returnValue;
        }

        public List<string> GetHeaders()
        {
            return propertyCellFromProperties.Select(p => p.HeaderName).ToList();
        }
    }
}
