using System.Net.Http.Json;
using ToDoList.Models;

namespace BlazingChat.Shared.Services
{

public class AssigneeAPIClient: IAssigneeAPIClient
{
     public HttpClient _httpClient;
     public AssigneeAPIClient (HttpClient httpClient)
     {
         _httpClient=httpClient;
     }
    public async Task<List<AssigneeDto>> GetAssignees ()
    {
        var result = await _httpClient.GetFromJsonAsync<List<AssigneeDto>>("Assignees");
        return result;
    }
  
}


}