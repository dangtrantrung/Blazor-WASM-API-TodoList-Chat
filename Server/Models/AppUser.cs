using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace blazingchat.Server.Models;

public class AppUser:IdentityUser
{
    public AppUser ()
    {
        Messages = new HashSet<Message>();
    }
    
    //public int Id { get; set; }
    public virtual ICollection<Message> Messages {get;set;}
    
    public string? EmailAddress { get; set; } = null!;

    public string? Password { get; set; } = null!;

    public string? Source { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? AboutMe { get; set; }

    public long? Notifications { get; set; }

    public long? DarkTheme { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<ChatHistory> ChatHistoryFromUsers { get; set; } = new List<ChatHistory>();

    public virtual ICollection<ChatHistory> ChatHistoryToUsers { get; set; } = new List<ChatHistory>();
}
