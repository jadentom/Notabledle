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
<tr><td>  <div class="passive-icon-container passive-icon-type__notable">
    <div class="passive-icon-frame"></div>
    <a href="/us/Broadside">
      <img loading="lazy" src="https://cdn.poedb.tw/image/Art/2DArt/SkillIcons/passives/BowDamage.webp">
    </a>
  </div></td><td><div><a data-hover='?t=PassiveSkills&id=20834' href='/us/Broadside'>Broadside</a></div><div class="implicitMod">Bow Skills have <span class='mod-value'>25</span>% increased Area of Effect</div></td></tr>
 */

var splitContent = content.Split("<div class=\"passive-icon-frame\"></div>");
var urlDictionary = new Dictionary<string, string>();
foreach (var contentForNotable in splitContent.Skip(1))
{
    var firstHref = contentForNotable.Split("href=\"")[1].Split('\"')[0];
    string key;
    try
    {
        key = contentForNotable.Split("'?t=PassiveSkills&id=")[1];
        key = key.Split('\'')[0];
    }
    catch
    {
        Console.WriteLine($"Stopping loop because encountered {firstHref}. This should be a timeless notable");
        break;
    }
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