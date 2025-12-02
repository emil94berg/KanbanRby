using KanbanRby.Factories;
using KanbanRby.Models;
using KanbanRby.Services;
using KanbanRby.Services.Interfaces;
using Microsoft.Extensions.Configuration;
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
    public async Task ShouldAddCardToSupabase()
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
    public async Task ShouldMoveCardBetweenColumns()
    {
        var expectedColumnId = 2;
        var name = "test card";
        var description = "test card description";
        var columnId = 1;
        
        var actual = await _cardManagementService.CreateCardAsync(name, description, columnId);
        await _cardManagementService.MoveCardBetweenColumnsAsync(actual.Id, expectedColumnId);
        var cardWithNewColumn = await _cardManagementService.GetCardByIdAsync(actual.Id);
        
        Assert.Equal(expectedColumnId, cardWithNewColumn.ColumnId);
        _createdCardIds.Add(actual.Id);
        
    }

    [Fact]
    public async Task ShouldMoveCardUpOnePositionInColumn()
    {
        var name = "test card";
        var description = "test card description";
        var columnId = 1;
        var intChange = -1;
        List<Card> allCards = new();
        
        var actual = await _cardManagementService.CreateCardAsync(name, description, columnId);
        var actual2 = await _cardManagementService.CreateCardAsync("test card2", description, columnId);
        var actual3 = await _cardManagementService.CreateCardAsync("test card3", description, columnId);
        var actual4 = await _cardManagementService.CreateCardAsync("test card4", description, 2);
        
        allCards =  await _cardManagementService.GetAllCardsAsync(columnId);
        await _cardManagementService.ChangeOrderOfCardsAsync(actual3.Id, intChange, allCards);
        actual3 = await _cardManagementService.GetCardByIdAsync(actual3.Id);
        
        
        Assert.NotNull(allCards);
        Assert.Equal(2, actual3.RowId);
        Assert.Equal(3, allCards.Count);
        
        _createdCardIds.Add(actual.Id);
        _createdCardIds.Add(actual2.Id);
        _createdCardIds.Add(actual3.Id);
        _createdCardIds.Add(actual4.Id);
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