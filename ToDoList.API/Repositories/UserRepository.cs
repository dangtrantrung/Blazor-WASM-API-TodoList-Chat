using ToDoList.API.Entities;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.Data;

namespace ToDoList.API.Repositories
{
    
    public class UserRepository: IUserRepository
    {
        private readonly ToDoListDbContext _context;
        public UserRepository(ToDoListDbContext context)
        {
            _context=context;
        }

        public async Task<List<User>> GetUserList()
        {
           return await _context.Users.ToListAsync();
           
        }
   }
}