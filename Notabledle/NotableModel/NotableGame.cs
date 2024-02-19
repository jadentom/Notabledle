using Notabledle.NotableModel.GameDisplay;

namespace Notabledle.NotableModel
{
    public class NotableGame
    {
        // TODO:
        // Add "you win"
        // Add free play feature
        // Actual CI/CD (manual deploy)
        // Add icon column
        // Playtesting
        //   Ordering of columns
        //   Difficulty
        //   Other properties
        // Images and descriptions on victory
        // Reference tree with names descriptions and distances
        //   Zoom in when tree area is known and grey out other areas
        // Add code to switch to a new notable every day
        // Prettify UI with CSS
        Notable targetNotable = NotableList.Value[305]; // Rampart in Duelist
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
            var returnValue = DisplayCellsFromGuesses(notableGuesses, targetNotable);
            returnValue.Reverse();
            return returnValue;
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

        public bool IsWin()
        {
            return notableGuesses.Contains(targetNotable);
        }
    }
}
