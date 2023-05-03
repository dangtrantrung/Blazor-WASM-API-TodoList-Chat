using blazingchat.Server.Models;
using Microsoft.AspNetCore.ResponseCompression;
using BlazingChat.Server.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;

var builder = WebApplication.CreateBuilder(args);
//từ net6.0 phải cấu hình server Kestrel: httpsDefaultoption, ssl protocol để phù hợp win7 ssl protocol (tls 1.2, 1.3)
builder.WebHost.ConfigureKestrel(kestrelServerOptions=>
{
    kestrelServerOptions.ConfigureHttpsDefaults(httpsOptions =>
    {
        httpsOptions.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13;
    });
});
/* builder.WebHost.ConfigureKestrel(kestrelServerOptions =>
{
    // ...
    // Thiết lập lắng nghe trên cổng 8090 với IP bất kỳ
    kestrelServerOptions.Listen(IPAddress.Any, 8090);
}); */

//từ net6.0 phải cấu hình server Kestrel: httpsDefaultoption, ssl protocol để phù hợp win7 ssl protocol (tls 1.2, 1.3)
builder.WebHost.ConfigureKestrel(kestrelServerOptions=>
{
    kestrelServerOptions.ConfigureHttpsDefaults(httpsOptions =>
    {
        httpsOptions.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13;
    });
});

builder.Services.AddDbContext<BlazingChatContext>(options=>{

string connectstring=builder.Configuration.GetConnectionString("BlazingChatAppConnectString");
options.UseSqlServer(connectstring);
});
var identityservice = builder.Services.AddIdentity<AppUser, IdentityRole>();

// Thêm triển khai EF lưu trữ thông tin về Identity (theo AppDbContext -> MS SQL Server).
identityservice.AddEntityFrameworkStores<BlazingChatContext>();

// Thêm Token Provider - nó sử dụng để phát sinh token (reset password, confirm email ...)
// đổi email, số điện thoại ...
identityservice.AddDefaultTokenProviders(); 

// Truy cập IdentityOptions
builder.Services.Configure<IdentityOptions> (options => {
    // Thiết lập về Password
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
    options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

    // Cấu hình Lockout - khóa user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes (1); // Khóa 1 phút
    options.Lockout.MaxFailedAccessAttempts = 3; // Thất bại 5 lần thì khóa
    options.Lockout.AllowedForNewUsers = true;

    // Cấu hình về User.
    options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;  // Email là duy nhất

   // Cấu hình đăng nhập.
    options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
    options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
    options.SignIn.RequireConfirmedAccount=true;
    
});

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//builder.Services.AddEntityFrameworkSqlite().AddDbContext<BlazingChatContext>();
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});
var app = builder.Build();
app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();

app.MapHub<ChatHub>("/chathub");
app.MapFallbackToFile("index.html");

app.Run();
