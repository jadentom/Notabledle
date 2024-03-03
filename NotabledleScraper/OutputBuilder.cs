using NotabledleScraper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotabledleScraper
{
    static internal class OutputBuilder
    {
        public static void BuildOutputForNode(SkillTreeJsonModel? parsedSkillJson, Dictionary<string, NodeManualData> manualDataDictionary, StringBuilder outString, List<NodeManualData> unpopulatedNodeManualDataList, SkillTreeNode node)
        {
            if (!parsedSkillJson.Groups.TryGetValue(node.GroupId, out var associatedGroup))
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
                if (!parsedSkillJson.Nodes.TryGetValue(i.ToString(), out var outNode))
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
                for (var i = 0; i < masteryStringComponents.Length; i++)
                {
                    // It's the only possible lower case word
                    if (masteryStringComponents[i].Equals("and", StringComparison.Ordinal))
                    {
                        masteryStringComponents[i] = "And";
                    }
                }
                masteryString = string.Join("", masteryStringComponents);
            }

            var treeArea = TreeAreaFromPosition.Get(associatedGroup.X, associatedGroup.Y);

            var groupNodeCount = siblingNodes.Where(n => !n.IsMastery).Count();

            var description = string.Join(", ", node.Stats.Select(s => $"\"{s}\""));
            
            // Comment these out if re-generating the manual stats json
            var manualDataEntry = manualDataDictionary[node.Name];
            outString.AppendLine($@"new Notable(
    {node.Id},
    ""{node.Name}"",
    new[] {{ {description} }},
    TreeArea.{treeArea},
    MasteryType.{masteryString},
    new HashSet<Catalyst> {{ {CatalystsFromNodeManualData(manualDataEntry)} }},
    new HashSet<KnownColor> {{ {ColorsFromNodeManualData(manualDataEntry)} }},
    {manualDataEntry.Distance},
    {siblingNodes.Count()},
    new OilRecipe({string.Join(", ", oils)})),");

            var unpopulatedNodeManualData = new NodeManualData()
            {
                Name = node.Name,
                TreeArea = treeArea,
            };
            unpopulatedNodeManualDataList.Add(unpopulatedNodeManualData);
        }

        static string CatalystsFromNodeManualData(NodeManualData nodeManualData) =>
            string.Join(", ", nodeManualData.Catalysts.Select(c => $"Catalyst.{c}"));

        static string ColorsFromNodeManualData(NodeManualData nodeManualData) =>
            string.Join(", ", nodeManualData.Colors.Select(c => $"KnownColor.{c}"));
    }
}
