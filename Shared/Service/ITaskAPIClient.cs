using ToDoList.Models;

namespace BlazingChat.Shared.Services
{

public interface ITaskAPIClient
{
    Task<List<TaskDto>> GetTaskList (TaskListSearch taskListSearch);
    Task<TaskDto> GetTaskDetail (string id);
}


}