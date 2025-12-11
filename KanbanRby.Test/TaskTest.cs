using KanbanRby.Components.Pages;

namespace KanbanRby.Test;

public class TaskTest
{
    [Fact]
    public void ShouldChangeIsCompletedToTrueOnTask()
    {
        //Arrange
        var sut = new MyKanbans();
        var task = new Models.Task() { Name = "TestTask", IsCompleted = false };
        //Act
        sut.TaskCompleted(task);
        //Assert
        Assert.True(task.IsCompleted);
    }
    [Fact]
    public void ShouldChangeIsCompletedToFaslOnTask()
    {
        //Arrange
        var sut = new MyKanbans();
        var task = new Models.Task() { Name = "TestTask", IsCompleted = true };
        //Act
        sut.TaskCompleted(task);
        //Assert
        Assert.False(task.IsCompleted);
    }
}