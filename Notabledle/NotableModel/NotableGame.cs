using Notabledle.NotableModel.GameDisplay;

namespace Notabledle.NotableModel
{
    public class NotableGame
    {
        Notable targetGem = NotableList.Value[0];
        List<Notable> notableGuesses = new List<Notable>();
        List<IPropertyCellFromProperty> propertyCellFromProperties = new List<IPropertyCellFromProperty>()
        {
            //   Notabledle (standalone)
            //     1. X Tree area (Scion or 1/6th) (Maybe also distance?)
            //     2. X Mastery type
            //     3. X Applicable catalysts (if it were catalystable)
            //     4. X Color(s) of notable image
            //     5. X Count of separate modifiers granted by notable
            //     6. X Count of nodes in wheel
            //     7. Blight oils (Red if highest not same, orange if highest same)
            new PropertyCellFromRowCount(),
            new PropertyCellFromName(),
            new PropertyCellFromTreeArea(),
            new PropertyCellFromMasteryType(),
            new PropertyCellFromCatalystList(),
            new PropertyCellFromIconColors(),
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
