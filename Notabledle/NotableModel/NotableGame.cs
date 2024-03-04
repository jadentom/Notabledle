using Notabledle.NotableModel.GameDisplay;

namespace Notabledle.NotableModel
{
    public class NotableGame
    {
        // TODO:
        // Actual CI/CD (manual deploy)
        // Add icon column
        //   Hover over icon for more info
        //   Icon and description on win
        // Add extra help 10, 15, 20 guesses
        //   Icon
        //   First letter
        //   Second letter
        // Change webpage icon, remove sidebar
        // Playtesting
        //   Ordering of columns
        //   Difficulty
        //   Other properties
        // Images and descriptions on victory
        // Reference tree with names descriptions and distances
        //   Zoom in when tree area is known and grey out other areas
        // Add code to switch to a new notable every day
        // Prettify UI with CSS

        Notable targetNotable;
        List<Notable> notableGuesses = new List<Notable>();
        List<IPropertyCellFromProperty> propertyCellFromProperties = new List<IPropertyCellFromProperty>()
        {
            // Make sure this count is synced with Index.Razor
            new PropertyCellFromRowCount(), // 0
            new PropertyCellFromName(), // 1
            new PropertyCellFromIcon(), // 2
            new PropertyCellFromTreeArea(), // 3
            new PropertyCellFromMasteryType(), // 4
            new PropertyCellFromCatalystList(), // 5
            new PropertyCellFromIconColors(), // 6
            new PropertyCellFromStartingDistance(), // 7
            new PropertyCellFromModifierCount(), // 8
            new PropertyCellFromGroupNodeCount(), // 9
            new PropertyCellFromOilRecipe(), // 10
        };

        public NotableGame()
        {
            // targetNotable = NotableList.Value[305]; // Rampart in Duelist
            var dailyRandom = new Random((int)DateTime.UtcNow.Date.Ticks);
            var randomIndex = dailyRandom.Next(0, NotableList.Value.Count());
            targetNotable = NotableList.Value[randomIndex];
        }

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

        public bool Randomize()
        {
            if (notableGuesses.Count > 0)
            {
                return false;
            }

            var targetIndex = Random.Shared.Next(NotableList.Value.Count);
            targetNotable = NotableList.Value[targetIndex];
            return true;
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
