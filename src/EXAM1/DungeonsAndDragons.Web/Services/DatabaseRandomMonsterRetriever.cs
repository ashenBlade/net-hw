using DungeonsAndDragons.Shared.Models;

namespace DungeonsAndDragons.Web.Services;

public class DatabaseRandomMonsterRetriever : IRandomMonsterRetriever
{
    private readonly HttpClient _client;
    public readonly string _endpoint;
    public DatabaseRandomMonsterRetriever(IConfiguration configuration, HttpClient client)
    {
        _client = client;
        _endpoint = configuration["RandomMonsterRetrieveEndpoints"];
    }
    
    public async Task<Entity?> GetRandomMonsterAsync()
    {
        return await _client.GetFromJsonAsync<Entity>(_endpoint);
    }
}