﻿namespace Notabledle.NotableModel.GameDisplay
{
    public class PropertyCellFromRowCount : IPropertyCellFromProperty
    {
        public string HeaderName => "Guess #";

        public string HelpText => "Which guess this is.";

        public PropertyCell PropertyCellFromProperty(Notable guessGem, Notable targetGem, int i) =>
            new PropertyCell(i.ToString()){ Color = DisplayColor.Transparent };
    }
}
