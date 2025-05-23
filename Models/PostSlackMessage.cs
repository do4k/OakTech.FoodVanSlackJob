using Newtonsoft.Json;

namespace OakTech.FoodVanSlackJob.Models;

public class PostSlackMessage
{
    [JsonProperty(PropertyName="message")]
    public string Message { get; set; }

    public PostSlackMessage(string message)
    {
        Message = message;
    }

    // implicit cast from string
    public static implicit operator PostSlackMessage(string message)
    {
        return new PostSlackMessage(message);
    }
}
