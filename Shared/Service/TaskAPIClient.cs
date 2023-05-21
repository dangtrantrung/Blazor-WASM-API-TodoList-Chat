using System.Net.Http.Json;
using ToDoList.Models;

namespace BlazingChat.Shared.Services
{

public class TaskAPIClient:ITaskAPIClient
{
     public HttpClient _httpClient;
     public TaskAPIClient (HttpClient httpClient)
     {
         _httpClient=httpClient;
     }
    public async Task<List<TaskDto>> GetTaskList (TaskListSearch taskListSearch)
    {
        string url = $"tasks?name={taskListSearch.Name}&assigneeId={taskListSearch.AssigneeId}&priority={taskListSearch.Priority}";
        var result = await _httpClient.GetFromJsonAsync<List<TaskDto>>(url);
        return result;
    }
    public async Task<TaskDto> GetTaskDetail (string id)
    {
        var result = await _httpClient.GetFromJsonAsync<TaskDto>($"tasks/{id}");
        return result;
    }
}


}