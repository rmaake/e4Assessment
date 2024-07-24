using UsersWebApp.Models.Config;
using UsersWebApp.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton(builder.Configuration.GetSection("AppSettings").Get<AppSettings>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();


app.MapControllers();

app.MapFallbackToFile("index.html"); ;

app.Run();
