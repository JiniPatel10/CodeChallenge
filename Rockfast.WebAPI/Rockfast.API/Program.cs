using Microsoft.EntityFrameworkCore;
using Rockfast.ApiDatabase;
using Rockfast.Dependencies;
using Rockfast.ServiceInterfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddDbContext<ApiDbContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddLogging(builder =>
{
    builder.AddConsole(); 
});
builder.Services.AddCors(options =>
{
    //TODO : Use a App setting instead of hardcoded URL
    options.AddPolicy("ApiCorsPolicy",
        s => s.WithOrigins(
                              "http://localhost:5000")
        .AllowAnyMethod()
              .AllowAnyHeader()
             );
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("ApiCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
