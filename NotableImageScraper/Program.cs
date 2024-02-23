// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

HttpClient client = new();
var response = await client.GetAsync("https://poedb.tw/us/Notable");
var content = await response.Content.ReadAsStringAsync();
Console.WriteLine(content);