using KanbanRby.Models;
using Supabase;

namespace KanbanRby.Services.Interfaces;

public interface ISupabaseService
{
    Task<Client> GetClientAsync();
    Task<Kanban> CreateKanbanAsync(string name, string description);
    Task<List<Kanban>> GetKanbanAsync();
}