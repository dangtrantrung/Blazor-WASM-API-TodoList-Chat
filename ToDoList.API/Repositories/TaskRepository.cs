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

        public async Task<IEnumerable<Entities.Task>> GetTaskList(TaskListSearch taskListSearch)
        {
            var query = _context.Tasks
                                 .Include(x=>x.Assignee).AsQueryable();
            if(!string.IsNullOrEmpty(taskListSearch.Name))
               {
                  query=query.Where(x=>x.Name.Contains(taskListSearch.Name));
               }
            if(taskListSearch.AssigneeId.HasValue)
               {
                  query=query.Where(x=>x.AssigneeId.Value== taskListSearch.AssigneeId.Value);
               }
            if(taskListSearch.Priority.HasValue)
               {
                  query=query.Where(x=>x.Priority== taskListSearch.Priority);
               }
            return await query.ToListAsync();
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