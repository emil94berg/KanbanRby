using KanbanRby.Models;
using Task = System.Threading.Tasks.Task;

namespace KanbanRby.Services.Interfaces;

public interface ISupabaseProfileService
{
    Task<List<PublicProfile>> GetAllProfiles();
}