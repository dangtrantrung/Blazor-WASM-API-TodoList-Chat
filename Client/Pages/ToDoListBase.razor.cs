
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using BlazingChat.Shared.Services;
using ToDoList.Models;

namespace blazingchat.Client;
public class ToDoListBase: ComponentBase
    {
        //protected string Name= "tedu";
        [Inject] private ITaskAPIClient _taskAPIClient {get;set;}
        protected List<TaskDto> Tasks {get;set;}
        
        protected override async Task OnInitializedAsync ()
        {
            Tasks= await _taskAPIClient.GetTaskList();

        }
    }
