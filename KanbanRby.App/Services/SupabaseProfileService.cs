using KanbanRby.Models;
using KanbanRby.Services.Interfaces;
using Task = System.Threading.Tasks.Task;
using Client = Supabase.Client;

namespace KanbanRby.Services;

public class SupabaseProfileService : ISupabaseProfileService
{
    private ISupabaseService _supabaseService;

    public SupabaseProfileService(ISupabaseService supabaseService)
    {
        _supabaseService = supabaseService;
    }
    
    private async Task<Client> GetClient() => await _supabaseService.GetClientAsync();
    
    public async Task<List<PublicProfile>> GetAllProfiles()
    {
        var client = await GetClient();

        var result = await client.From<PublicProfile>().Get();
        
        return result.Models;
    }
}