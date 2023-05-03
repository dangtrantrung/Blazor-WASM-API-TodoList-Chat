using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ToDoList.API.Entities;
using ToDoList.Models.Enums;
using Task = System.Threading.Tasks.Task;

namespace ToDoList.API.Data
{
   public class TodoListDbContextSeed
    {
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        private User seedUser {get;set;}
        public async Task SeedAsync(ToDoListDbContext context, ILogger<TodoListDbContextSeed> logger)
        {
            if (!context.Users.Any())
            {
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Mr",
                    LastName = "A",
                    Email = "admin1@gmail.com",
                    NormalizedEmail = "ADMIN1@GMAIL.COM",
                    PhoneNumber = "032132131",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                user.PasswordHash = _passwordHasher.HashPassword(user, "Admin@123$");
                context.Users.Add(user);
                seedUser=user;
            }

            if (!context.Tasks.Any())
            {
                context.Tasks.Add(new Entities.Task()
                {
                    Id = Guid.NewGuid(),
                    AssigneeId=seedUser.Id,
                    Assignee=seedUser,
                    Name = "Same tasks 1",
                    CreatedDate = DateTime.Now,
                    Priority = Priority.High,
                    Status = Status.Open
                });
            }

            await context.SaveChangesAsync();
        }
    }
}