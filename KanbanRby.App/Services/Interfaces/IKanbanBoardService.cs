using KanbanRby.Models;
using Task = System.Threading.Tasks.Task;

namespace KanbanRby.Services.Interfaces;

public interface IKanbanBoardService
{
    Task<List<Kanban>> GetAllAsync();
    Task<Kanban?> GetByIdAsync(int id);
    Task<Kanban> CreateKanban(string name, string description);
    Task UpdateAsync(Kanban board);
    Task DeleteAsync(object id);
}