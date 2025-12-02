using KanbanRby.Models;
using Task =  System.Threading.Tasks.Task;

namespace KanbanRby.Services.Interfaces;

public interface IColumnManagerService
{
    Task<List<Column>> GetAllAsync();
    Task<Column> GetByIdAsync(int id);
    Task<List<Column>> GetColumnsByKanbanIdAsync(int kanbanId);
    Task<Column> CreateColumnAsync(string name, string description);
    Task<Column> UpdateColumnAsync(Column column);
    Task DeleteColumnAsync(int columnId);
}