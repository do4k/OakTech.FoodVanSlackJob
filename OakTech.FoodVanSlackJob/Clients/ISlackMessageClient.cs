using Refit;

namespace OakTech.FoodVanSlackJob.Clients;

public interface ISlackMessageClient
{
    [Post("/{webhookPath}")]
    public Task<IApiResponse<string>> PostMessageAsync(string webhookPath, string message);
}