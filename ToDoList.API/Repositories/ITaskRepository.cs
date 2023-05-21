using ToDoList.Models;
using Task=ToDoList.API.Entities.Task;

namespace ToDoList.API.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Task>> GetTaskList(TaskListSearch taskListSearch);
        Task<Task> Create(Task task);
        Task<Task> Update(Task task);
        Task<Task> Delete(Task task);
        Task<Task> GetById(Guid id);
    }
}