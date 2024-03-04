// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using NotabledleScraper.Model;

HttpClient client = new();
var response = await client.GetAsync("https://poedb.tw/us/Notable");
var content = await response.Content.ReadAsStringAsync();
// Console.WriteLine(content);

// There's some scraper blocking it doesn't work
// https://superuser.com/questions/1288979/save-multiple-files-from-firefox-web-console
// Need to use CTRL + I now: https://support.mozilla.org/en-US/kb/firefox-page-info-window
// Save into AllPoedbNotablePageMedia

// A notable div looks like this:
/**
  </div></td><td><div><a data-hover='?t=PassiveSkills&id=48807' href='/us/Art_of_the_Gladiator'>Art of the Gladiator</a></div><div class="implicitMod"><span class='mod-value'>10</span>% increased Attack Speed<br/><span class='mod-value'>10</span>% increased Global Accuracy Rating<br/>Ignore all Movement Penalties from Armour<br/><span class='mod-value'>+20</span> to Dexterity</div></td></tr><tr><td>  <div class="passive-icon-container passive-icon-type__notable">
    <div class="passive-icon-frame"></div>
    <a href="/us/Agility">
      <img loading="lazy" src="https://cdn.poedb.tw/image/Art/2DArt/SkillIcons/passives/grace.webp">
    </a>
 */

var splitContent = content.Split("'?t=PassiveSkills&id=");
var urlDictionary = new Dictionary<string, string>();
foreach (var contentForNotable in splitContent)
{
    var key = contentForNotable.Split('\'')[0];
    try
    {
        var url = contentForNotable.Split('"').First(s => s.StartsWith("https://cdn.poedb.tw/image/"));
        urlDictionary.Add(key, url);
    }
    catch
    {
        Console.WriteLine($"Could not add key/url pair for key {key}");
    }
}

// Deserialize the tree to crossreference
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

var outDir = "output";
var rawDir = "AllPoedbNotablePageMedia";
Directory.CreateDirectory(outDir);
foreach (var (key, node) in parsedSkillJson.Nodes)
{
    if (node.Recipe is null || !node.Recipe.Any())
    {
        continue;
    }
    var imageUrl = urlDictionary[key];
    var imageName = imageUrl.Split('/').Last();
    imageName = imageName.Replace("%20", " ");
    var rawPath = Path.Combine(rawDir, imageName);
    var outPath = Path.Combine(outDir, $"notable_{key}.webp");
    try
    {
        File.Copy(rawPath, outPath);
    }
    catch (IOException e) when (e.Message.Contains("already exists", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine($"{outPath} already exists, skipping");
    }
}
Console.WriteLine($"Copied all notables to {Path.GetFullPath(outDir)}");