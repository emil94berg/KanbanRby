using KanbanRby.Factories.Interfaces;
using KanbanRby.Models;
using KanbanRby.Services.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace KanbanRby.Services;

public class UserManagementService : IUserManagementService
{
    private readonly ICrudFactory<User> _userCrudFactory;

    public UserManagementService(ICrudFactory<User> userCrudFactory)
    {
        _userCrudFactory = userCrudFactory;
    }
    
    public async Task<List<User>> GetAllUsersAsync() => await _userCrudFactory.GetAllAsync();

    public async Task<User?> GetUserByIdAsync(int id)
    {
        var user = await _userCrudFactory.GetByIdAsync(id);
        return user;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        await _userCrudFactory.CreateAsync(user);
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        await _userCrudFactory.UpdateAsync(user);
        return user;
    }

    public async Task DeleteUserAsync(int id)
    {
        await _userCrudFactory.DeleteAsync(id);
    }
}