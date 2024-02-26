// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

HttpClient client = new();
var response = await client.GetAsync("https://poedb.tw/us/Notable");
var content = await response.Content.ReadAsStringAsync();
Console.WriteLine(content);

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
foreach (var contentForNotable in splitContent)
{
    var nameWithTrailing = contentForNotable.Split('>')[1];
    var name = nameWithTrailing.Split('<')[0];
    var nameFiltered = name.Where(char.IsLetterOrDigit);
    
}