using KanbanRby.Services.Interfaces;
using KanbanRby.Sessions.Interfaces;
using Supabase.Gotrue;
using Task = System.Threading.Tasks.Task;

namespace KanbanRby.Sessions;

public class UserSession : IUserSession
{
    private ISupabaseAuthService _supabaseAuthService;
    public User? CurrentUser { get; set; }
    public event Action? OnChange;

    public UserSession(ISupabaseAuthService supabaseAuthService)
    {
        _supabaseAuthService = supabaseAuthService;
    }

    public async Task RefreshUserAsync()
    {
        CurrentUser = await _supabaseAuthService.CurrentUserAsync();
        NotifyStateChanged();
    }

    public async Task<bool> IsLoggedInAsync()
    {
        var user = await _supabaseAuthService.CurrentUserAsync();
        return user != null;
    }
    
    private void NotifyStateChanged() => OnChange?.Invoke();
    
}