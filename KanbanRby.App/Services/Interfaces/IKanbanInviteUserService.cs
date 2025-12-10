using Task = System.Threading.Tasks.Task;
using KanbanRby.Models;

namespace KanbanRby.Services.Interfaces;



public interface IKanbanInviteUserService
{
    Task CreateKanbanUser(KanbanUser kanbanUser);
}