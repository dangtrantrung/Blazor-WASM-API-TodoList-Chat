using ToDoList.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models;

public class TaskCreateRequest
{
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [MaxLength(250)]
        [Required]
        public string Name { get; set; }
        
        public Priority Priority {get;set;}
        public Status Status {get;set;}
}
