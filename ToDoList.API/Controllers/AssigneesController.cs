using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Repositories;
using ToDoList.Models.Enums;
using ToDoList.Models;

namespace ToDoList.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AssigneesController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    public AssigneesController (IUserRepository UserRepository)
    {
          _userRepository=UserRepository;
    }
    //Viết API Get ALl Assignees - Users
    //api/tasks/
    [HttpGet]
    public async Task<IActionResult>  GetAll()
    {
        var users = await _userRepository.GetUserList();
        var assginees = users.Select(x=> new AssigneeDto()
        {
            Id=x.Id,
            FullName=x.FirstName + " " +x.LastName
        });
        return Ok(assginees);
    }
    
}
