using ToDoList.Models;
using Task=ToDoList.API.Entities.Task;

namespace ToDoList.API.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskDto>> GetTaskList();
        Task<Task> Create(Task task);
        Task<Task> Update(Task task);
        Task<Task> Delete(Task task);
        Task<Task> GetById(Guid id);
    }
}