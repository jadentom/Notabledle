using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace NotabledleScraper.Model
{
    /// <summary>
    /// The model for the whole Json
    /// </summary>
    public class SkillTreeJsonModel
    {
        // {
        //   "nodes": [],
        //   "groups": [],
        // }
        [JsonPropertyName("nodes")]
        public Dictionary<string, SkillTreeNode> Nodes { get; set; } = new();

        [JsonPropertyName("groups")]
        // This is a string because there's a "root" node
        public Dictionary<int, SkillTreeGroup> Groups { get; set; } = new();

        // Doing this manually:
        //var doc = JsonDocument.Parse(rawJson);
        //var groupsElement = doc.RootElement.GetProperty("groups");
        //var groupsObjects = groupsElement.EnumerateObject();
        //var groupsDictionary = groupsObjects.ToDictionary(x => x.Name, x => x.Value.Deserialize<SkillTreeGroup>());
        //var nodesElement = doc.RootElement.GetProperty("nodes");
        //var nodesObjects = nodesElement.EnumerateObject();
        //var nodesDictionary = nodesObjects.ToDictionary(x => x.Name, x => x.Value.Deserialize<SkillTreeNode>());
    }
}
