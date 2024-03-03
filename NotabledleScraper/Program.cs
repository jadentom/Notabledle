// See https://aka.ms/new-console-template for more information
using NotabledleScraper;
using NotabledleScraper.Model;
using System.Drawing;
using System.Text;
using System.Text.Json;

// Output format:
//
//public Notable(
//    string id,
//    string name,
//    string[] description,
//    TreeArea treeArea,
//    MasteryType masteryType,
//    HashSet<Catalyst> applicableCatalysts,
//    HashSet<KnownColor> iconColors,
//    int startingDistance,
//    int groupNodeCount,
//    OilRecipe oilRecipe)
//
//new Notable(
//    58449,
//    "Born to Fight",
//    new[] { "4% increased Attack Speed", "+20 to Strength", "26% increased Physical Damage" },
//    TreeArea.Marauder,
//    MasteryType.None,
//    new HashSet<Catalyst> { Catalysts.Abrasive, Catalysts.Accelerating, Catalysts.Intrinsic, Catalysts.Noxious },
//    new HashSet<KnownColor> { KnownColor.Pink, KnownColor.Blue },
//    5,
//    1,
//    new OilRecipe(BlightOil.Golden, BlightOil.Violet, BlightOil.Violet)),
//
// Input format:
//
//"5624": {
//    "skill": 5624,
//    "name": "Crusader",
//    "icon": "Art/2DArt/SkillIcons/passives/MindPact.png",
//    "isNotable": true,
//    "isBlighted": true,
//    "recipe": [
//        "BlackOil",
//        "GoldenOil",
//        "GoldenOil"
//    ],
//    "stats": [
//        "8% increased maximum Mana",
//        "Transfiguration of Mind"
//    ],
//    "reminderText": [
//        "(Increases and Reductions to Maximum Mana also apply to Damage at 30% of their value)"
//    ],
//    "group": 22,
//    "orbit": 0,
//    "orbitIndex": 0,
//    "out": [],
//    "in": []
//},
//
// We care about x and y for skill tree section and the count of nodes.
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

var prettyPrintSerializerOptions = new JsonSerializerOptions()
{
    WriteIndented = true,
    AllowTrailingCommas = true, // Doesn't add them but we'll need them when deserializing
};
var rawSkillJson = await File.ReadAllTextAsync("SkillTree.json")!;
var parsedSkillJson = JsonSerializer.Deserialize<SkillTreeJsonModel>(rawSkillJson, prettyPrintSerializerOptions);
if (parsedSkillJson is null)
{
    throw new JsonException("Parsed skill json is null");
}
Console.WriteLine($"Deserialized skill tree successfully. {parsedSkillJson.Groups.Count} groups, {parsedSkillJson.Nodes.Count} nodes.");

var rawManualDataJson = await File.ReadAllTextAsync("populated_node_manual_stats.json");
// Uncomment this to parse the raw JSON
//var doc = JsonDocument.Parse(rawSkillJson);
//var objects = doc.RootElement.EnumerateArray();
//var parsedManualJson = objects.Select(o => o.Deserialize<NodeManualData>()).ToList();
var parsedManualJson = JsonSerializer.Deserialize<List<NodeManualData>>(rawManualDataJson, prettyPrintSerializerOptions);
if (parsedManualJson is null)
{
    throw new JsonException("Parsed manual data json is null");
}
Console.WriteLine($"Deserialized manual data successfully. {parsedManualJson.Count} entries.");
var manualDataDictionary = parsedManualJson.ToDictionary(n => n.Name, n => n);

var outString = new StringBuilder();
var unpopulatedNodeManualDataList = new List<NodeManualData>();
foreach (var (key, node) in parsedSkillJson.Nodes)
{
    if (node.Recipe is null || !node.Recipe.Any())
    {
        continue;
    }
    OutputBuilder.BuildOutputForNode(parsedSkillJson, manualDataDictionary, outString, unpopulatedNodeManualDataList, node);
}
var outPath = "output.txt";
File.WriteAllText(outPath, outString.ToString());
Console.WriteLine($"Wrote generated NotableList output to {Path.GetFullPath(outPath)}");

unpopulatedNodeManualDataList = unpopulatedNodeManualDataList.OrderBy(n => n.TreeArea).ToList();
var unpopulatedJsonPath = "unpopulated_node_manual_stats.json";
File.WriteAllText(unpopulatedJsonPath, JsonSerializer.Serialize(unpopulatedNodeManualDataList, prettyPrintSerializerOptions));
Console.WriteLine($"Wrote unpopulated json to {Path.GetFullPath(unpopulatedJsonPath)}");