using KanbanRby.Factories.Interfaces;
using KanbanRby.Models;
using KanbanRby.Services;
using KanbanRby.Services.Interfaces;
using Moq;
using Task = System.Threading.Tasks.Task;

namespace KanbanRby.Test;

public class UserManagementTest
{
    public List<User> Userlist = new List<User>(){
        new User()
            { Id = 1, Name = "User1", Email = "Email123", Password = "Password123", CreatedAt = DateTime.Now },
        new User()
            { Id = 2, Name = "User2", Email = "Email124", Password = "Password124", CreatedAt = DateTime.Now }
    };
    
    
    
    [Fact]
    public void GetAllUsers()
    {
        //Arrange
        var mock = new Mock<IUserManagementService>();
        
        mock.Setup(m => m.GetAllUsersAsync()).ReturnsAsync(Userlist);
        
        //Act
        var obj = mock.Object;
        
        //Assert
        Assert.Equal(2, obj.GetAllUsersAsync().Result.Count);
        Assert.Contains("User2", obj.GetAllUsersAsync().Result.Select(u => u.Name));
    }

    [Fact]
    public void GetUserById()
    {
        //Arrange
        var mock = new Mock<IUserManagementService>();
        mock.Setup(m => m.GetUserByIdAsync(2)).ReturnsAsync(Userlist[1]);
        //Act
        var obj = mock.Object;
        //Assert
        Assert.Equal(2, obj.GetUserByIdAsync(2).Result.Id);
        Assert.Contains("User2", obj.GetUserByIdAsync(2).Result.Name);
    }
    
    [Fact]
    public void CreateNewUser()
    {
        //Arrange
        var mock = new Mock<IUserManagementService>();
        mock.Setup(m => m.CreateUserAsync(Userlist[1])).ReturnsAsync(Userlist[1]);
        //Act
        var obj = mock.Object;
        //Assert
        Assert.NotNull(obj);
        Assert.Equal(2, obj.CreateUserAsync(Userlist[1]).Result.Id);
    }

    [Fact]
    public void UpdateUser()
    {
        //Arrange
        var mock = new Mock<IUserManagementService>();
        mock.Setup(m => m.UpdateUserAsync(Userlist[1])).ReturnsAsync(Userlist[1]);
        //Act
        var obj  = mock.Object;
        //Assert
        Assert.NotNull(obj);
        Assert.Equal(2, obj.UpdateUserAsync(Userlist[1]).Result.Id);
    }

    [Fact]
    public async Task DeleteUser()
    {
        //Arrange
        var mock = new Mock<IUserManagementService>();
        mock.Setup(m => m.DeleteUserAsync(It.IsAny<int>())).Returns<int>(id =>
        {
            var user = Userlist.FirstOrDefault(u => u.Id == id);
            if (user != null) Userlist.Remove(user);
            return Task.CompletedTask;
        });
        //Act
        await mock.Object.DeleteUserAsync(2);
        //Assert
        Assert.DoesNotContain(Userlist, u => u.Id == 2);
        Assert.Single(Userlist);
    }
}