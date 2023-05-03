using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace blazingchat.Server.Models;

public  class BlazingChatContext : IdentityDbContext<AppUser>
{
    public BlazingChatContext (DbContextOptions<BlazingChatContext> options):base(options)
      {
          // option se dc DI inject
        
      }

    public virtual DbSet<ChatHistory> ChatHistories { get; set; }

    public virtual DbSet<AppUser> Users { get; set; }
    public virtual DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
        //=> optionsBuilder.UseSqlite("Name=BlazingChat");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Message>()
             .HasOne<AppUser>(a=>a.Sender)
             .WithMany(d=>d.Messages)
             .HasForeignKey(d=>d.UserId);

        modelBuilder.Entity<ChatHistory>(entity =>
        {
            entity.ToTable("ChatHistory");

            entity.Property(e => e.ChatHistoryId).HasColumnName("chat_history_id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("DATE")
                .HasColumnName("created_date");
            entity.Property(e => e.FromUserId)
            
                .HasColumnName("from_user_id");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.ToUserId)
                
                .HasColumnName("to_user_id");

            entity.HasOne(d => d.FromUser).WithMany(p => p.ChatHistoryFromUsers)
                .HasForeignKey(d => d.FromUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ToUser).WithMany(p => p.ChatHistoryToUsers)
                .HasForeignKey(d => d.ToUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.ToTable("AppUser");
            //entity.HasNoKey();

           
            entity.Property(e => e.AboutMe).HasColumnName("about_me");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("DATE")
                .HasColumnName("created_date");
            entity.Property(e => e.DarkTheme).HasColumnName("dark_theme");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("DATETIME")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.EmailAddress).HasColumnName("email_address");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.LastName).HasColumnName("last_name");
            entity.Property(e => e.Notifications).HasColumnName("notifications");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.ProfilePictureUrl).HasColumnName("profile_picture_url");
            entity.Property(e => e.Source).HasColumnName("source");
        });

    }

}
