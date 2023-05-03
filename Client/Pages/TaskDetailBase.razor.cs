
using Microsoft.AspNetCore.Components;
using BlazingChat.Shared.Services;
using ToDoList.Models;

namespace blazingchat.Client;

public class TaskDetailBase: ComponentBase

{

  [Inject] private ITaskAPIClient taskAPIClient {get;set;}


  [Parameter]
  public string TaskId {get;set;}

  protected TaskDto TaskIdDetail {get;set;}

  protected override async Task OnInitializedAsync()
  {
        TaskIdDetail= await taskAPIClient.GetTaskDetail(TaskId);
  }
}