using System;
using System.Collections.Generic;

namespace blazingchat.Server.Models;

public partial class ChatHistory
{
    public string ChatHistoryId { get; set; }

    public string FromUserId { get; set; }

    public string ToUserId { get; set; }

    public string Message { get; set; } = null!;

    public DateTime? CreatedDate { get; set; } = null!;

    public virtual AppUser FromUser { get; set; } = null!;

    public virtual AppUser ToUser { get; set; } = null!;
}
