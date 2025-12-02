using KanbanRby.Factories.Interfaces;
using KanbanRby.Models;
using KanbanRby.Services.Interfaces;
using Task = System.Threading.Tasks.Task;
using Supabase.Gotrue;
using Supabase;
using Client = Supabase.Client;


namespace KanbanRby.Services;


public class SupabaseAuthService : ISupabaseAuthService
{
    private ISupabaseService _supabaseService;
    
    public SupabaseAuthService(ISupabaseService supabaseService)
    {
        _supabaseService = supabaseService;
    }

    private async Task<Client> GetClient() => await _supabaseService.GetClientAsync();
   

    public async Task<User> RegisterNewAccountAsync(string email, string password)
    {
        var client = await GetClient();
        var result = await client.Auth.SignUp(email, password);
        return result.User;
    }

    public async Task<User?> LoginUserAsync(string email, string password)
    {
        var client = await GetClient();
        var result = await client.Auth.SignIn(email, password);
        return result?.User;
    }

    public async Task LogoutAsync()
    {
        var client = await GetClient();
        await client.Auth.SignOut();
    }

    public async Task<User?> CurrentUserAsync()
    {
        var client = await GetClient();
        return client.Auth.CurrentUser;
    } 
    
    public async Task<Session?> GetSessionTokenAsync()
    {
        var client = await GetClient();
        return client.Auth.CurrentSession;
    }
    
    public async Task SetDisplayName(string displayName)
    {
        var client = await GetClient();
        var user = client.Auth.CurrentUser;
        if (user != null)
        {
            var attribute = new UserAttributes
            {
                Data = new Dictionary<string, object>
                {
                    { "display_name", displayName }
                }
            };
            await client.Auth.Update(attribute);
        }
    }
}