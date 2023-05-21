
using Microsoft.AspNetCore.Components;
using ToDoList.Models;
using ToDoList.Models.Enums;

namespace blazingchat.Client;

public class TaskCreateBase: ComponentBase

{
  
        protected TaskCreateRequest Task =new TaskCreateRequest();
        protected string[] Priorities {get;set;}

        
        protected override async Task OnInitializedAsync ()
        {
           Priorities=Enum.GetNames(typeof(Priority));
        }
}