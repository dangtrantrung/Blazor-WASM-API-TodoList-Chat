using ToDoList.Models;
using Task=ToDoList.API.Entities.Task;
using ToDoList.API.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.API.Repositories
{
    
    public class TaskRepository: ITaskRepository
    {
        private readonly ToDoListDbContext _context;
        public TaskRepository(ToDoListDbContext context)
        {
            _context=context;
        }

        public async Task<IEnumerable<TaskDto>> GetTaskList()
        {
           return await _context.Tasks.Include(x=>x.Assignee)
           .Select(x=>new TaskDto()
           {
              Status=x.Status,
              Name=x.Name,
              AssigneeId=x.AssigneeId,
              CreatedDate=x.CreatedDate,
              Priority=x.Priority,
              Id=x.Id,
              AssigneeName=x.Assignee!=null? x.Assignee.FirstName + ' '+ x.Assignee.LastName:"N/A"
           })

           .ToListAsync();
        }
        public async Task<Task> Create(Task task)
        {
           await _context.Tasks.AddAsync(task);
           await _context.SaveChangesAsync();
           return task;
        }
        public async Task<Task> Update(Task task)
        {
           _context.Tasks.Update(task);
           await _context.SaveChangesAsync();
           return task;
        }
        public async Task<Task> Delete(Task task)
        {
           _context.Tasks.Remove(task);
          await _context.SaveChangesAsync();
           return task;
        }
        public async Task<Task> GetById(Guid id)
        {
           return await _context.Tasks.FindAsync(id); 
        }
    }
}