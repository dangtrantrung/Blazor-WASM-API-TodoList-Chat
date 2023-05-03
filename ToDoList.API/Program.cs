using System.Security.Authentication;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.Data;
using ToDoList.API.Repositories;
using ToDoList.API.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
//using Serilog;

var builder = WebApplication.CreateBuilder(args);
//từ net6.0 phải cấu hình server Kestrel: httpsDefaultoption, ssl protocol để phù hợp win7 ssl protocol (tls 1.2, 1.3)
builder.WebHost.ConfigureKestrel(kestrelServerOptions=>
{
    kestrelServerOptions.ConfigureHttpsDefaults(httpsOptions =>
    {
        httpsOptions.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13;
    });
});

// Add services to the container.

//add Dbcontext
builder.Services.AddDbContext<ToDoListDbContext>(options=>
{
    string connectstring=builder.Configuration.GetConnectionString("BlazingToDoListAppConnectString");
    options.UseSqlServer(connectstring);
 
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options=>
{
    options.AddPolicy("CorsPolicy",
                     builder=>builder
    .WithOrigins("*")
    .AllowAnyMethod()
    .AllowAnyHeader());
    //.AllowCredentials());
});

/* var host = CreateHostBuilder(args).Build();
host.MigrateDbContext<ToDoListDbContext>((context,services)=>
{
  var logger = services.GetService<ILogger<TodoListDbContextSeed>>();
  new TodoListDbContextSeed().SeedAsync(context,logger).Wait();
 
});
 host.Run(); */

 builder.Services.AddTransient<ITaskRepository,TaskRepository>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<TodoListDbContextSeed>>();
    var db= scope.ServiceProvider.GetRequiredService<ToDoListDbContext>();
    
    //var logger = services.GetService<ILogger<TodoListDbContextSeed>>();
    new TodoListDbContextSeed().SeedAsync(db,logger).Wait();
    db.Database.Migrate();

    //Xem SeriLog
    /* Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
    {
        return new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .Enrich.WithProperty("ApplicationContext",appName)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
    } */

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
