// See https://aka.ms/new-console-template for more information
using NotabledleScraper;
using NotabledleScraper.Model;
using System.Text;
using System.Text.Json;

// Output format:
//
//public Notable(string name,
//    TreeArea treeArea,
//    MasteryType masteryType,
//    HashSet<Catalysts> applicableCatalysts,
//    HashSet<KnownColor> iconColors,
//    int startingDistance,
//    int modifierCount,
//    int groupNodeCount,
//    OilRecipe oilRecipe)
//
//new Notable(
//    "Born to Fight",
//    TreeArea.Marauder,
//    MasteryType.None,
//    new HashSet<Catalysts> { Catalysts.Abrasive, Catalysts.Accelerating, Catalysts.Intrinsic, Catalysts.Noxious },
//    new HashSet<KnownColor> { KnownColor.Pink, KnownColor.Blue },
//    5,
//    3,
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

var rawJson = await File.ReadAllTextAsync("SkillTree.json")!;
var parsedJson = JsonSerializer.Deserialize<SkillTreeJsonModel>(rawJson);
if (parsedJson is null)
{
    throw new JsonException("Parsed json is null");
}
Console.WriteLine($"Deserialized successfully. {parsedJson.Groups.Count} groups, {parsedJson.Nodes.Count} nodes.");

var outString = new StringBuilder();
foreach (var (key, node) in parsedJson.Nodes)
{
    if (node.Recipe is null || !node.Recipe.Any())
    {
        continue;
    }
    if (!parsedJson.Groups.TryGetValue(node.GroupId, out var associatedGroup))
    {
        throw new KeyNotFoundException($"{node.Name} no group?");
    }
    if (associatedGroup.NodeIds is null)
    {
        throw new KeyNotFoundException($"{node.Name} associated group {node.GroupId} has no nodes?");
    }
    if (node.Stats is null)
    {
        throw new KeyNotFoundException($"{node.Name} no stats?");
    }

    var oils = node.Recipe
        .Reverse()
        .Select(o => $"BlightOil.{o[..^3]}");

    var siblingNodes = associatedGroup.NodeIds.Select(i =>
    {
        if (!parsedJson.Nodes.TryGetValue(i.ToString(), out var outNode))
        {
            throw new KeyNotFoundException($"{node.Name} associated group {node.GroupId} node {i} not found.");
        }
        return outNode;
    });
    var masteryString = "None";
    var masteryNode = siblingNodes.FirstOrDefault(n => n.IsMastery);
    if (masteryNode is not null)
    {
        var masteryStringComponents = masteryNode.Name
            .Split()
            .SkipLast(1) // always just "Mastery"
            .ToArray();
        for ( var i = 0; i < masteryStringComponents.Length; i++)
        {
            // It's the only possible lower case word
            if (masteryStringComponents[i].Equals("and", StringComparison.Ordinal))
            {
                masteryStringComponents[i] = "And";
            }
        }
        masteryString = string.Join("", masteryStringComponents);
    }

    outString.AppendLine($@"new Notable(
    ""{node.Name}"",
    TreeArea.{TreeAreaFromPosition.Get(associatedGroup.X, associatedGroup.Y)},
    MasteryType.{masteryString},
    new HashSet<Catalysts> {{ {"[UNIMPLEMENTED: CATALYSTS (MANUAL)]"} }},
    new HashSet<KnownColor> {{ {"[UNIMPLEMENTED: COLORS (MANUAL)]"} }},
    {"[UNIMPLEMENTED: START DISTANCE (MANUAL)]"},
    {node.Stats.Length},
    {siblingNodes.Count()},
    new OilRecipe({string.Join(", ", oils)})),");
}
var outPath = "output.txt";
File.WriteAllText(outPath, outString.ToString());

Console.WriteLine($"Wrote output to {Path.GetFullPath(outPath)}");