using KanbanRby.Factories.Interfaces;
using KanbanRby.Services;
using Moq;
using Task = System.Threading.Tasks.Task;

namespace KanbanRby.Test;

public class TaskManagement_Test
{
    private readonly Mock<ICrudFactory<Models.Task>> _mockTaskFactory;
    private readonly TaskManagementService _taskService;

    public TaskManagement_Test()
    {
        _mockTaskFactory = new Mock<ICrudFactory<Models.Task>>();
        _taskService = new TaskManagementService(_mockTaskFactory.Object);
    }

    [Fact]
    public async Task ShouldCreateTaskWithPosZeroWhenNoTasksExist()
    {
        // Arrange
        var taskName = "Write tests";
        var cardId = 1;

        _mockTaskFactory.Setup(f => f.GetByForeignIdAsync("card_id", cardId)).ReturnsAsync(new List<Models.Task>());

        _mockTaskFactory.Setup(f => f.CreateAsync(It.IsAny<Models.Task>())).ReturnsAsync((Models.Task t) =>
            {
                t.Id = 1;
                return t;
            });

        // Act
        var result = await _taskService.CreateTaskAsync(taskName, cardId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(taskName, result.Name);
        Assert.Equal(cardId, result.CardId);
        Assert.Equal(0, result.RowId);
    }

[Fact]
    public async Task ShouldIncrementRowIdWhenTasksExist()
    {
        // Arrange
        var taskName = "New task";
        var cardId = 1;
        var existingTasks = new List<Models.Task>
        {
            new Models.Task { Id = 1, RowId = 0 },
            new Models.Task { Id = 2, RowId = 1 }
        };

        _mockTaskFactory.Setup(f => f.GetByForeignIdAsync("card_id", cardId))
            .ReturnsAsync(existingTasks);

        _mockTaskFactory.Setup(f => f.CreateAsync(It.IsAny<Models.Task>()))
            .ReturnsAsync((Models.Task t) => { t.Id = 3; return t; });

        // Act
        var result = await _taskService.CreateTaskAsync(taskName, cardId);

        // Assert
        Assert.Equal(2, result.RowId);
    }

    [Fact]
    public async Task GetTasksByCardIdAndReturnTaskList()
    {
        // Arrange
        var cardId = 1;
        var expectedTasks = new List<Models.Task>
        {
            new Models.Task { Id = 1, CardId = cardId },
            new Models.Task { Id = 2, CardId = cardId }
        };

        _mockTaskFactory.Setup(f => f.GetByForeignIdAsync("card_id", cardId)).ReturnsAsync(expectedTasks);

        // Act
        var result = await _taskService.GetTasksByCardId(cardId);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, t => Assert.Equal(cardId, t.CardId));
    }

    [Fact]
    public async Task ShouldReturnTask()
    {
        // Arrange
        var taskId = 1;
        var expectedTask = new Models.Task { Id = taskId, Name = "Test task" };

        _mockTaskFactory.Setup(f => f.GetByIdAsync(taskId)).ReturnsAsync(expectedTask);

        // Act
        var result = await _taskService.GetByIdAsync(taskId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(taskId, result.Id);
    }
}
