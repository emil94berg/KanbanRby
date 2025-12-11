using KanbanRby.Factories.Interfaces;
using KanbanRby.Models;
using KanbanRby.Services.Interfaces;
using Task =  System.Threading.Tasks.Task;

namespace KanbanRby.Services;

public class ColumnManagementService : IColumnManagerService
{
    private readonly ICrudFactory<Column> _columnFactory;

    public ColumnManagementService(ICrudFactory<Column> columnFactory)
    {
        _columnFactory = columnFactory;
    }
    
    #region CRUD Operations
    public async Task<List<Column>> GetAllAsync() => await _columnFactory.GetAllAsync();
    
    public async Task<Column> GetByIdAsync(int id) => await _columnFactory.GetByIdAsync(id);

    // Return all columns by kanbanId
    public async Task<List<Column>> GetColumnsByKanbanIdAsync(int kanbanId)
    {
        return await _columnFactory.GetByForeignIdAsync("kanban_id", kanbanId);
    }

    public async Task<Column> CreateColumnAsync(string name, string description, int boardId)
    {
        var existingColumns = await GetColumnsByKanbanIdAsync(boardId);
        var nextPosition = existingColumns.Any() ? existingColumns.Max(c => c.Position) + 1 : 0;
        
        var newColumn = new Column()
        {
            Name = name,
            Description = description,
            KanbanId = boardId,
            Position = nextPosition
        };
        var createdColumn = await _columnFactory.CreateAsync(newColumn);
        return createdColumn;
    }
    
    public async Task<Column> UpdateColumnAsync(Column column) =>  await _columnFactory.UpdateAsync(column);
    
    public async Task DeleteColumnAsync(int columnId) => await _columnFactory.DeleteAsync(columnId);

    public async Task ReorderColumnsInKanbanAsync(int kanbanId)
    {
        var columns = await GetColumnsByKanbanIdAsync(kanbanId);
        var index = 0;
        foreach (var column in columns.OrderBy(c => c.Position))
        {
            if (column.Position != index)
            {
                column.Position = index;
                await _columnFactory.UpdateAsync(column);
            }
            index++;
        }
    }
    #endregion
}
