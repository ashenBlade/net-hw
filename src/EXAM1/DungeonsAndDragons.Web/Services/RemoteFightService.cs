using DungeonsAndDragons.Shared;

namespace DungeonsAndDragons.Web.Services;

public class RemoteFightService : IFightService
{
    private readonly HttpClient _client;
    private readonly string _endpoint;
    public RemoteFightService(IConfiguration configuration, 
                              HttpClient client)
    {
        _client = client;
        _endpoint = configuration["FightServiceEndpoint"];
    }
    
    public async Task<FightEndDTO> SimulateFightAsync(FightStartDTO dto)
    {
        var result = await _client.PostAsJsonAsync(_endpoint, dto);
        var fightEnd = await result.Content.ReadFromJsonAsync<FightEndDTO>();
        return fightEnd;
    }
}