namespace KanbanRby.Services.Interfaces;

public interface ITaskManagementService
{
    Task<List<Models.Task>> GetAllTasks();
    Task<Models.Task?> GetByIdAsync(int id);
    Task<List<Models.Task>> GetTasksByCardId(int cardId);
    Task<Models.Task> CreateTaskAsync(string name, int cardId);
    Task<Models.Task> UpdateTaskAsync(Models.Task task);
    Task DeleteTaskAsync(int id);
}