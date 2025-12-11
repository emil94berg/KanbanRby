using KanbanRby.Factories.Interfaces;
using KanbanRby.Models;
using KanbanRby.Services.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace KanbanRby.Services;

public class TaskManagementService : ITaskManagementService
{
    private readonly ICrudFactory<Models.Task> _taskFactory;
    
    public TaskManagementService(ICrudFactory<Models.Task> taskFactory)
    {
        _taskFactory = taskFactory;
    }

    #region CRUD Operations
    public async Task<List<Models.Task>> GetAllTasks() => await _taskFactory.GetAllAsync();

    public async Task<Models.Task?> GetByIdAsync(int id) => await _taskFactory.GetByIdAsync(id);

    public async Task<List<Models.Task>> GetTasksByCardId(int cardId)
    {
        return await _taskFactory.GetByForeignIdAsync("card_id", cardId);
    }

    public async Task<Models.Task> CreateTaskAsync(string name, int cardId)
    {
        var existingTasks = await GetTasksByCardId(cardId);
        var nextPosition = existingTasks.Any() ? existingTasks.Max(t => t.RowId) + 1 : 0;
        
        var newTask = new Models.Task()
        {
            Name = name,
            CardId = cardId,
            RowId = nextPosition
        };
        
        var createdTask = await _taskFactory.CreateAsync(newTask);
        return createdTask;
    }

    public async Task<Models.Task> UpdateTaskAsync(Models.Task task) => await _taskFactory.UpdateAsync(task);
    
    public async Task DeleteTaskAsync(int id) => await _taskFactory.DeleteAsync(id);
    #endregion
}