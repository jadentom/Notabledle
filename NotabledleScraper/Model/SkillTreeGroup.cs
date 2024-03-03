using System.Text.Json.Serialization;

namespace NotabledleScraper.Model
{
    public class SkillTreeGroup
    {
        //"22": {
        //    "x": -8947.77,
        //    "y": -2419.3,
        //    "orbits": [
        //        0
        //    ],
        //    "nodes": [
        //        "5624"
        //    ]
        //},
        [JsonPropertyName("nodes")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public int[]? NodeIds { get; set; }

        [JsonPropertyName("x")]
        public double X { get; set; }

        [JsonPropertyName("y")]
        public double Y { get; set; }
    }
}
