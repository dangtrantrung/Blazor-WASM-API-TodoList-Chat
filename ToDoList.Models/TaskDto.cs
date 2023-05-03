using ToDoList.Models.Enums;
namespace ToDoList.Models;
public class TaskDto
{
        public Guid Id { get; set; }
       
        public string Name { get; set; }
        public Guid? AssigneeId { get; set; }
        public string AssigneeName {get;set;}
        
        //public User? Assignee { get; set; }
        public DateTime CreatedDate {get;set;}
        public Priority Priority {get;set;}
        public Status Status {get;set;}
}
