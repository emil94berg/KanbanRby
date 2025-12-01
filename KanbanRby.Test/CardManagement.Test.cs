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
using Xunit.Abstractions;
using Task = System.Threading.Tasks.Task;

namespace KanbanRby.Test;

public class CardManagement_Test : IAsyncLifetime
{
    private ISupabaseService _supabaseService;
    private ICardManagementService _cardManagementService;
    private List<int> _createdCardIds = new();
    
    public async Task InitializeAsync()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Test.json")
            .Build();
        _supabaseService = new SupabaseService(configuration);
        await _supabaseService.GetClientAsync();

        var cardFactory = new CrudFactory<Card>(_supabaseService);
        _cardManagementService = new CardManagementService(cardFactory);
    }
    
    [Fact]
    public async void ShouldAddCardToSupabase()
    {
        var name = "test card";
        var description = "test card description";
        var columnId = 1;
        
        var actual = await _cardManagementService.CreateCardAsync(name, description,  columnId);
        
        Assert.NotNull(actual);
        Assert.Equal(name, actual.Name);
        Assert.Equal(description, actual.Description);
        Assert.True(actual.Id > 0);
        
        _createdCardIds.Add(actual.Id);
    }
    
    [Fact]
    public async void ShouldMoveCardBetweenColumns()
    {
        var expected = "test card changed";
        var expectedColumnId = 2;
        var name = "test card";
        var description = "test card description";
        var columnId = 1;
        
        var actual = await _cardManagementService.CreateCardAsync(name, description, columnId);
        //actual.Name = expected;
        //var newcard= await _cardManagementService.UpdateCardAsync(actual);
        await _cardManagementService.MoveCardBetweenColumnsAsync(actual.Id, expectedColumnId);
        var cardWithNewColumn = await _cardManagementService.GetCardByIdAsync(actual.Id);
        
        Assert.Equal(expectedColumnId, cardWithNewColumn.ColumnId);
        _createdCardIds.Add(actual.Id);
        
    }

    public async Task DisposeAsync()
    {
        foreach (var cardId in _createdCardIds)
        {
            try
            {
                await _cardManagementService.DeleteCardByIdAsync(cardId);
            }
            catch { }
        }
    }
}