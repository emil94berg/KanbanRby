using KanbanRby.Factories;
using KanbanRby.Factories.Interfaces;
using KanbanRby.Models;
using KanbanRby.Services;
using Supabase.Postgrest.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using KanbanRby.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Supabase.Realtime;
using Task = System.Threading.Tasks.Task;

namespace KanbanRby.Test;

public class KanbanBoard_Test : IAsyncLifetime
{
    private ISupabaseService _supabaseService;
    private IKanbanBoardService _kanbanBoardService;
    private List<int> _createdKanbanIds = new();

    public async Task InitializeAsync()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Test.json")
            .Build();
        
        _supabaseService = new SupabaseService(configuration);
        await _supabaseService.GetClientAsync();
        
        var kanbanFactory = new CrudFactory<Kanban>(_supabaseService);
        _kanbanBoardService = new KanbanBoardService(kanbanFactory);
    }
    
    [Fact]
    public async void ShouldAddKanbanBoardToSupabase()
    {
        // Arrange
        var name = "Test Kanban";
        var description = "Created through integration test";
        
        // Act
        var result = await _kanbanBoardService.CreateKanban(name, description);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(name, result.Name);
        Assert.Equal(description, result.Description);
        Assert.True(result.Id > 0);
        
        _createdKanbanIds.Add(result.Id);
    }

    public async Task DisposeAsync()
    {
        foreach (var id in _createdKanbanIds)
        {
            try
            {
                await _kanbanBoardService.DeleteAsync(id);
            }
            catch { }
        }
    }
}