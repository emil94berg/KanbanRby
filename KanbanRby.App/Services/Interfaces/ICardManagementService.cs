using KanbanRby.Models;
using Task = System.Threading.Tasks.Task;

namespace KanbanRby.Services.Interfaces;

public interface ICardManagementService
{
    Task<Card> CreateCardAsync(string name, string description,  int columnId);
    Task<Card> GetCardByIdAsync(int id);
    Task DeleteCardByIdAsync(int id);
    Task<Card> UpdateCardAsync(Card card);
    Task<List<Card>> GetAllCardsAsync();
    Task MoveCardBetweenColumnsAsync(int cardId, int newColumnId);
}