using Supabase.Gotrue;
using KanbanRby.Models;
using Task = System.Threading.Tasks.Task;

namespace KanbanRby.Services.Interfaces;


public interface ISupabaseAuthService
{
    Task SetDisplayName(string displayName);
    Task<User> RegisterNewAccountAsync(string email, string password);
    Task<User?> LoginUserAsync(string email, string password);
    Task LogoutAsync();
    Task<User?> CurrentUserAsync();
    Task<Session?> GetSessionTokenAsync();

}