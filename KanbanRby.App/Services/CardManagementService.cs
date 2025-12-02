using KanbanRby.Models;
using Task = System.Threading.Tasks.Task;
using KanbanRby.Factories.Interfaces;
using KanbanRby.Services.Interfaces;

namespace KanbanRby.Services;

public class CardManagementService : ICardManagementService
{
    private readonly ICrudFactory<Card>  _cardFactory;
    
    public CardManagementService(ICrudFactory<Card>  cardFactory)
    {
        _cardFactory = cardFactory;
    }

    public async Task<Card> CreateCardAsync(string name, string description, int columnId)
    {
        var cardsInColumn = (await GetAllCardsAsync(columnId)).Count;
        var rowId = cardsInColumn + 1; 
        var newCard = await _cardFactory.CreateAsync(new Card(){Name = name, Description = description,  ColumnId = columnId, RowId = rowId});
        return newCard;
    }
    
    public async Task<Card> GetCardByIdAsync(int id)
    {
        var getCard = await _cardFactory.GetByIdAsync(id);
        return getCard;
    }
    
    public async Task DeleteCardByIdAsync(int id)
    {
        await _cardFactory.DeleteAsync(id);
    }

    public async Task<Card> UpdateCardAsync(Card card) => await _cardFactory.UpdateAsync(card);
    
    public async Task<List<Card>> GetAllCardsAsync(int columnId)
    {
        var getAllCardsOfColumn = new List<Card>();
        var getAllCards = await _cardFactory.GetAllAsync();
        foreach (var card in getAllCards)
        {
            if (card.ColumnId == columnId)
            {
                getAllCardsOfColumn.Add(card);
            }
        }
        return getAllCardsOfColumn;
    }

    public async Task MoveCardBetweenColumnsAsync(int cardId, int newColumnId)
    {
        var card = await _cardFactory.GetByIdAsync(cardId); 
        card.ColumnId  = newColumnId;
        await _cardFactory.UpdateAsync(card);       
    }
    
    public async Task ChangeOrderOfCardsAsync(int cardId, int intChange, List<Card> cards)
    {
        var card = await _cardFactory.GetByIdAsync(cardId);
        
        var oldRowId = card.RowId;
        var newRowId = intChange < 0 ? card.RowId - 1 : card.RowId + 1;
        
        var cardToChangeWith =  cards.FirstOrDefault(c => c.RowId == newRowId);

        if (cardToChangeWith != null)
        {
            cardToChangeWith.RowId = oldRowId;
            await _cardFactory.UpdateAsync(cardToChangeWith);
        }

        card.RowId = newRowId;
        await _cardFactory.UpdateAsync(card);
    }
}