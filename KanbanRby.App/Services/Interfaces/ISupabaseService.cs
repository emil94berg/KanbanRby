using KanbanRby.Models;
using Supabase;

namespace KanbanRby.Services.Interfaces;

public interface ISupabaseService
{
    Task<Client> GetClientAsync();
}