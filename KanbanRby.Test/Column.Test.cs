using KanbanRby.Factories;
using KanbanRby.Models;
using KanbanRby.Services;
using KanbanRby.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Task = System.Threading.Tasks.Task;

namespace KanbanRby.Test;

public class Column_Test : IAsyncLifetime
{
    private ISupabaseService _supabaseService;
    private IColumnManagerService _columnManagementService;
    private IKanbanBoardService _kanbanBoardService;
    private List<int> _createdColumnIds = new();
    
    public async Task InitializeAsync()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Test.json")
            .Build();
        _supabaseService = new SupabaseService(configuration);
        await _supabaseService.GetClientAsync();

        var columnFactory = new CrudFactory<Column>(_supabaseService);
        _columnManagementService = new ColumnManagementService(columnFactory);
    }

    [Fact]
    public async Task ShouldAddColumnToSupabaseAsync()
    {
        //Arrange
        var column = new Column() { Name = "Test", Description = "TestDesc", CreatedAt = DateTime.Now };
        
        //Act
        var actual = await _columnManagementService.CreateColumnAsync(column.Name,  column.Description, 44);
        
        //Assert
        Assert.NotNull(actual);
        Assert.Equal(column.Name, actual.Name);
        Assert.Equal(column.Description, actual.Description);
        
        _createdColumnIds.Add(actual.Id);
    }

    [Fact]
    public async Task ShouldUpdateColumnFromSupabaseAsync()
    {
        //Arrange
        var column = await _columnManagementService.GetByIdAsync(54);
        //Act
        column.Description = "omajfakkinggad2222";
        await _columnManagementService.UpdateColumnAsync(column);
        var actual = await _columnManagementService.GetByIdAsync(54);
        //Assert
        Assert.NotNull(actual);
        Assert.Equal(column.Name, actual.Name);
        Assert.Equal(column.Description, actual.Description);
    }

    [Fact]
    public async Task ShouldGetASpecificColumnBasedOnKanbanIdAsync()
    {
        //Arrange
        var actual = await _columnManagementService.GetColumnsByKanbanIdAsync(44);
        //Act
        var expected = 1;
        //Assert
        Assert.Equal(expected, actual.Count);
    } 
    
    //Delete crud, is tested here
    public async Task DisposeAsync()
    {
        foreach (var columnId in _createdColumnIds)
        {
            try
            {
                await _columnManagementService.DeleteColumnAsync(columnId);
            }
            catch { }
        }
    }
    
    
    
    
    
}