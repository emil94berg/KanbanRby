using KanbanRby.Services.Interfaces;
using Supabase;
using KanbanRby.Models;
using Task = System.Threading.Tasks.Task;

namespace KanbanRby.Services;

public class SupabaseService :  ISupabaseService
{
    private Client _client;
    private Task? _initTask;

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
        if (_initTask == null)
        {
            _initTask = _client.InitializeAsync();
        }
        await _initTask;
        return _client;
    }

}