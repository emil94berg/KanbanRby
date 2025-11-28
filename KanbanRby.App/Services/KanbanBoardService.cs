using KanbanRby.Factories.Interfaces;
using KanbanRby.Models;
using KanbanRby.Services.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace KanbanRby.Services;

public class KanbanBoardService : IKanbanBoardService
{
    private readonly ICrudFactory<Kanban> _kanbanFactory;

    public KanbanBoardService(ICrudFactory<Kanban> kanbanFactory)
    {
        _kanbanFactory = kanbanFactory;
    }
    
    #region CRUD Operations
    public async Task<List<Kanban>> GetAllAsync() => await _kanbanFactory.GetAllAsync();
    
    public async Task<Kanban?> GetByIdAsync(int id) => await _kanbanFactory.GetByIdAsync(id);
    
    public async Task<Kanban> CreateKanban(string name, string description)
    {
        var newBoard = new Kanban()
        {
            Name = name,
            Description = description
        };
        var createdBoard = await _kanbanFactory.CreateAsync(newBoard);
        return createdBoard;
    }
    
    public async Task UpdateAsync(Kanban board) => await _kanbanFactory.UpdateAsync(board);
    public async Task DeleteAsync(object id) => await _kanbanFactory.DeleteAsync(id);
    #endregion
    
}