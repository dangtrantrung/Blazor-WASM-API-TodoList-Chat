using ToDoList.Models;

namespace BlazingChat.Shared.Services
{

public interface ITaskAPIClient
{
    Task<List<TaskDto>> GetTaskList ();
    Task<TaskDto> GetTaskDetail (string id);
}


}