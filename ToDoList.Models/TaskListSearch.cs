using ToDoList.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ToDoList.Models;
public class TaskListSearch 
{
        public string? Name {get;set;}
        public Guid? AssigneeId {get;set;}
        public Priority? Priority {get;set;}
}
