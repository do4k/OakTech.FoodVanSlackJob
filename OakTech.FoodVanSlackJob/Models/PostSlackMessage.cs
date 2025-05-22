namespace OakTech.FoodVanSlackJob.Models;

public record PostSlackMessage(string Message)
{
    // implicit cast from string
    public static implicit operator PostSlackMessage(string message)
    {
        return new PostSlackMessage(message);
    }
}