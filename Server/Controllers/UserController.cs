using Microsoft.AspNetCore.Mvc;
using blazingchat.Server.Models;
using BlazingChat.Shared;

namespace blazingchat.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    
    private readonly ILogger<UserController> _logger;
    private readonly BlazingChatContext _context;

    public UserController(ILogger<UserController> logger, BlazingChatContext context)
    {
        _logger = logger;
        _context=context;
    }

    [HttpGet]
    public List<Contact> Get()
    {
        List<AppUser> users=_context.Users.ToList();
        List<Contact> contacts= new List<Contact>();
        int userId=0;
        foreach (var user in users)
        {
            contacts.Add(new Contact(userId, user.FirstName,user.LastName));
            userId++;
        }
        return contacts;
    }
}
