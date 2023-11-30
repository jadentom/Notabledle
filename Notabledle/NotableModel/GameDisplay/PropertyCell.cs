namespace Notabledle.NotableModel.GameDisplay
{
    public class PropertyCell
    {
        public PropertyCell(string guessText)
        {
            GuessText = guessText;
        }

        public PropertyCell(IComparable guessGemProperty, IComparable targetGemProperty, bool upDown = false)
        {
            var upDownText = "";
            var isEqual = guessGemProperty.Equals(targetGemProperty);

            Color = isEqual ? DisplayColor.Green : DisplayColor.Red;
            if (upDown && !isEqual)
            {
                upDownText = guessGemProperty.CompareTo(targetGemProperty) > 0 ? " ↓" : " ↑";
            }
            GuessText = guessGemProperty.ToString()! + upDownText;
        }

        public string GuessText { get; }

        public DisplayColor Color { get; set; } = DisplayColor.Red; 
    }
}
