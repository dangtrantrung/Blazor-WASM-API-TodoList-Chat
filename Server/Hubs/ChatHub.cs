using Microsoft.AspNetCore.SignalR;
using blazingchat.Server.Models;

namespace BlazingChat.Server.Hubs;

public class ChatHub : Hub
{
    private readonly BlazingChatContext _context;
    
    private readonly ILogger<ChatHub> _logger;
    //private readonly UserManager<AppUser> _userManager;

    public ChatHub(BlazingChatContext context,ILogger<ChatHub> logger)
    {
        _logger=logger;
        _context=context;
        
    }
    public async Task SendMessage(string user, string message, DateTime date, string connectionId )
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message,date, Context.ConnectionId);

        // logic for public talk & private talk
        // public talk
        // update to database
         var msg = new Message ()
         {   
             UserId=Context.ConnectionId,
             UserName=user,
             Text=message,
             When=date};
         var newuser= new AppUser()
             {
                Id=Context.ConnectionId,
                UserName=user
             };
             var finduser = await _context.Users.FindAsync(Context.ConnectionId);
             if( finduser==null)
             {
                  await _context.Users.AddAsync(newuser);
                  await _context.Messages.AddAsync(msg);
                  await _context.SaveChangesAsync();
             }
             else if(finduser!=null)
             {
                  
                  await _context.Messages.AddAsync(msg);
                  await _context.SaveChangesAsync();
             }
         
         


    }
}