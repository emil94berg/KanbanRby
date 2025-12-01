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
        var newCard = await _cardFactory.CreateAsync(new Card(){Name = name, Description = description,  ColumnId = columnId});
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
    // {
    //     //var card = await _cardFactory.GetByIdAsync(id);
    //     await _cardFactory.UpdateAsync(card);
    //     return card;
    // }
    public async Task<List<Card>> GetAllCardsAsync()
    {
        return await _cardFactory.GetAllAsync();
    }

    public async Task MoveCardBetweenColumnsAsync(int cardId, int newColumnId)
    {
        var card = await _cardFactory.GetByIdAsync(cardId); 
        card.ColumnId  = newColumnId;
        await _cardFactory.UpdateAsync(card);       
    }
    
    // public async Task ChangeOrderOfCardsAsync(int cardId, int intChange)
    // {
    //     var card = await _cardFactory.GetByIdAsync(cardId);
    //     var cards = await _cardFactory.GetAllAsync();
    //     int currentPos = 0;
    //     int index = 0;
    //
    //     foreach (var cardInCards in cards)
    //     {
    //         if(card == cardInCards)
    //         {
    //             currentPos = index;
    //         }
    //         index++;
    //     }
    //     
    //     cards[currentPos] = card;
    //
    //
    // }
}