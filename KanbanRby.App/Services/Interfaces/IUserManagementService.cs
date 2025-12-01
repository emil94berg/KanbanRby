namespace KanbanRby.Services.Interfaces;
using KanbanRby.Models;
using Task = System.Threading.Tasks.Task;

public interface IUserManagementService
{
    Task<List<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User> CreateUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);
    Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
}