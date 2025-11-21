using KanbanRby.Services.Interfaces;
using Supabase;

namespace KanbanRby.Services;

public class SupabaseService :  ISupabaseService
{
    private Client _client;

    public SupabaseService(IConfiguration config)
    {
        var url = config["Supabase:Url"];
        var key = config["Supabase:Key"];

        var options = new SupabaseOptions
        {
            AutoConnectRealtime = true
        };
        
        _client = new Client(url, key, options);
    }

    public async Task<Client> GetClientAsync()
    {
        await _client.InitializeAsync();
        return _client;
    }
}