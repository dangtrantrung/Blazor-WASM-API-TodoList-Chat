using ToDoList.Models;

namespace BlazingChat.Shared.Services
{

public interface IAssigneeAPIClient
{
    Task<List<AssigneeDto>> GetAssignees();
    
}


}