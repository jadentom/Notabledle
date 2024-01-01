using Notabledle.NotableModel.GameDisplay;

namespace Notabledle.NotableModel
{
    public class NotableGame
    {
        // TODO:
        // Manually populate populated_node_manual_stats.json
        // Column help text
        // Don't include mastery in wheel node count
        // Invert list of guesses
        // Add free play feature
        // Actual CI/CD (manual deploy)
        // Playtesting
        //   Ordering of columns
        //   Difficulty
        //   Other properties
        // Images and descriptions on victory
        // Reference tree with descriptions
        // The tree should cross off areas and zoom once you know what it is.
        // Add new notable each day
        // Prettify UI with CSS
        Notable targetGem = NotableList.Value[305]; // Rampart in Duelist
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

        public List<(string Header, string HelpText)> GetHeaders()
        {
            return propertyCellFromProperties.Select(p => (p.HeaderName, p.HelpText)).ToList();
        }
    }
}
