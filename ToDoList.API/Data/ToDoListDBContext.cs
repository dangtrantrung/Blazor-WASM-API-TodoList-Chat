using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ToDoList.API.Entities;

using Task=ToDoList.API.Entities.Task;

namespace ToDoList.API.Data
{
    public class ToDoListDbContext:IdentityDbContext<User,Role,Guid>
    
    {
        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options)
        {

        }
        public DbSet<Task> Tasks {get;set;}
        public DbSet<User> Users {get;set;}
        public DbSet<Role> Roles {get;set;}

    }
}