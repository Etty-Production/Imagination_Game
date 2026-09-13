using Test_LR1.Hubs;
using Test_LR1.Services;
using Microsoft.EntityFrameworkCore;
using Test_LR1.Data; 

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddSignalR();

builder.Services.AddSingleton<IPlayerService, PlayerService>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepositoryPostgress>();
builder.Services.AddHostedService<PlayerFlushService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.WebHost.UseUrls("http://0.0.0.0:7000", "https://0.0.0.0:7001");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<MouseControllerHub>("/MouseControllerHub");

app.Run();
