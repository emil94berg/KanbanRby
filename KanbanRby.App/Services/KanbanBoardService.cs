using KanbanRby.Factories.Interfaces;
using KanbanRby.Models;
using KanbanRby.Services.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace KanbanRby.Services;

public class KanbanBoardService : IKanbanBoardService
{
    private readonly ICrudFactory<Kanban> _kanbanFactory;
    private readonly ICrudFactory<KanbanUser> _kanbanUserFactory;

    public KanbanBoardService(ICrudFactory<Kanban> kanbanFactory, ICrudFactory<KanbanUser> kanbanUserFactory)
    {
        _kanbanFactory = kanbanFactory;
        _kanbanUserFactory = kanbanUserFactory;
    }
    
    #region CRUD Operations
    public async Task<List<Kanban>> GetAllAsync() => await _kanbanFactory.GetAllAsync();
    
    public async Task<Kanban?> GetByIdAsync(int id) => await _kanbanFactory.GetByIdAsync(id);
    
    public async Task<Kanban> CreateKanbanAsync(string name, string description, string userId)
    {
        var newBoard = new Kanban()
        {
            Name = name,
            Description = description,
            UserId = userId,
            CreatedAt = DateTime.Now
        };
        var createdBoard = await _kanbanFactory.CreateAsync(newBoard);
        return createdBoard;
    }
    
    public async Task UpdateAsync(Kanban board) => await _kanbanFactory.UpdateAsync(board);
    public async Task DeleteAsync(object id) => await _kanbanFactory.DeleteAsync(id);

    public async Task<List<Kanban>> GetUserKanbansAsync(string userId)
    {
        return await _kanbanFactory.GetByForeignIdAsync("user_id", userId);
    }

    public async Task<List<Kanban>> GetSharedKanbansAsync(string userId)
    {
        var kanbanUsers = await _kanbanUserFactory.GetByForeignIdAsync("user_id", userId);
        return kanbanUsers
            .Where(ku => ku.KanbanDetails != null)
            .Select(ku => ku.KanbanDetails)
            .ToList();
    }
    #endregion
    
}