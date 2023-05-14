
using ToDoList.API.Entities;

namespace ToDoList.API.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUserList();
        
    }
}