using OakTech.FoodVanSlackJob.Clients;
using OakTech.FoodVanSlackJob.Models;
using Refit;
using System.Net.Http.Json;

var webhookPath = args.Length == 0 ? null : args[0];
if (string.IsNullOrWhiteSpace(webhookPath))
{
    Console.WriteLine("No webhook path provided.");
    return;
}

Console.WriteLine($"Got webhook path: {webhookPath}");

var client = RestService.For<IFoodVanApiClient>("http://127.0.0.1:8042");
var foodVansEnumerable = await client.GetFoodVansAsync();
var foodVans = foodVansEnumerable.ToList();
Console.WriteLine("Finished requesting vans");

if (foodVans.Count == 0)
{
    Console.WriteLine("No food vans for today.");
    return;
}

Console.WriteLine($"Found {foodVans.Count} food vans for today.");
var message = string.Join("\n", foodVans.Select((x, i) => 
    new {Index = i, Van = x})
    .Select(x => $"{x.Index + 1}. {x.Van.Name}"));

Console.WriteLine($"Preparing to send message:\n{message}");

var slackClient = new HttpClient();
var content = JsonContent.Create(new PostSlackMessage(message));
var result = await slackClient.PostAsync($"https://hooks.slack.com/triggers/{webhookPath}", content);

if (result.IsSuccessStatusCode)
{
    Console.WriteLine("Message successfully sent.");
}
