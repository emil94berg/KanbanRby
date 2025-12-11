using KanbanRby.Models;
using KanbanRby.Services.Interfaces;
using Client = Supabase.Client;
using Task = System.Threading.Tasks.Task;
using KanbanRby.Factories.Interfaces;


namespace KanbanRby.Services;

public class KanbanInviteUserService : IKanbanInviteUserService
{
    private readonly ICrudFactory<KanbanUser> _kanbanUserFactory;

    public KanbanInviteUserService(ICrudFactory<KanbanUser> kanbanUserFactory)
    {
        _kanbanUserFactory = kanbanUserFactory;
    }
    public async Task CreateKanbanUser(KanbanUser kanbanUser) => await _kanbanUserFactory.CreateAsync(kanbanUser);
   
}