using Supabase.Gotrue;

namespace KanbanRby.Sessions.Interfaces;

public interface IUserSession
{
    User? CurrentUser { get; }
    event Action? OnChange;
    Task RefreshUserAsync();
    Task<bool> IsLoggedInAsync();
    
}