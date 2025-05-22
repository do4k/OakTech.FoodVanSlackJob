using OakTech.FoodVanSlackJob.Clients;
using Refit;

var webhookPath = args[0];
if (string.IsNullOrWhiteSpace(webhookPath))
{
    Console.WriteLine("No webhook path provided.");
    return;
}

var client = RestService.For<IFoodVanApiClient>("https://food-vans.o4k.uk");
var foodVans = await client.GetFoodVansAsync();

if (foodVans.Count == 0)
{
    Console.WriteLine("No food vans for today.");
    return;
}

Console.WriteLine($"Found {foodVans.Count} food vans for today.");
var slackClient = RestService.For<ISlackMessageClient>("https://hooks.slack.com/triggers");
var message = string.Join("\n", foodVans.Select((x, i) => 
    new {Index = i, Van = x})
    .Select(x => $"{x.Index + 1}. {x.Van.Name} - {x.Van.Link}"));

Console.WriteLine($"Preparing to send message:\n{message}");

var result = await slackClient.PostMessageAsync(webhookPath, message);

if (result.IsSuccessStatusCode)
{
    Console.WriteLine("Message successfully sent.");
}
else
{
    Console.WriteLine($"Failed to send message: {result.StatusCode}");
    Console.WriteLine($"Content: {result.Content}");
}