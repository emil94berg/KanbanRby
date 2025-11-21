namespace KanbanRby.Services.Interfaces;

public interface ISupabaseService
{
    Task<Supabase.Client> GetClientAsync();
}