
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using BlazingChat.Shared.Services;
using ToDoList.Models;
using ToDoList.Models.Enums;

namespace blazingchat.Client
{
public class ToDoListBase: ComponentBase
    {
        //protected string Name= "tedu";
        [Inject] private ITaskAPIClient _taskAPIClient {get;set;}
        [Inject] private IAssigneeAPIClient _assgineeAPIClient {get;set;}
        protected List<TaskDto> Tasks {get;set;}
        protected TaskListSearch TaskListSearch = new TaskListSearch();
        protected List<AssigneeDto> Assginees {get;set;}
        protected string[] Priorities {get;set;}

        
        protected override async Task OnInitializedAsync ()
        {
            Tasks= await _taskAPIClient.GetTaskList(TaskListSearch);
            Assginees= await _assgineeAPIClient.GetAssignees();
            Priorities=Enum.GetNames(typeof(Priority));

        }

        protected async Task SearchForm()
        {
            //var name =TaskListSearch.Name;
            Tasks= await _taskAPIClient.GetTaskList(TaskListSearch);

        }
    }
        
}