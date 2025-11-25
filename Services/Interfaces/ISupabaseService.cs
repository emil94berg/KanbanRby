using KanbanRby.Models;

namespace KanbanRby.Services.Interfaces;

public interface ISupabaseService
{
    Task<Supabase.Client> GetClientAsync();
    Task<Kanban> CreateKanbanAsync(string name, string description);
    Task<List<Kanban>> GetKanbanAsync();
    
}