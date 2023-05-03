using ToDoList.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ToDoList.Models;
public class TaskUpdateRequest
{
           
        [MaxLength(250)]
        [Required]
        public string Name { get; set; }
        
        public Priority Priority {get;set;}
       // public Status Status {get;set;}
}
