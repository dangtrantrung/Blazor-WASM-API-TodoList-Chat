using Microsoft.AspNetCore.Mvc;
using blazingchat.Server.Models;
using BlazingChat.Shared;
using Microsoft.AspNetCore.Identity;

namespace blazingchat.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatUserController : ControllerBase
{
    
    private readonly ILogger<UserController> _logger;
    private readonly BlazingChatContext _context;
    private readonly UserManager<AppUser> _userManager;

    public ChatUserController(ILogger<UserController> logger, BlazingChatContext context, UserManager<AppUser> userManager)
    {
        _logger = logger;
        _context=context;
        _userManager=userManager;
    }

    [HttpGet]
     public async Task<AppUser> Get()
    {
        var currentUser= await _userManager.GetUserAsync(User);
       
        return currentUser;
    }

}
