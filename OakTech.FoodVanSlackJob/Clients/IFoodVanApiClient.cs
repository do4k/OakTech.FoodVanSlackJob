using OakTech.FoodVanSlackJob.Models;
using Refit;

namespace OakTech.FoodVanSlackJob.Clients;

public interface IFoodVanApiClient
{
    [Get("/")]
    public Task<List<FoodVan>> GetFoodVansAsync();
}