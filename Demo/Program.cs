using Demo.Data;
using Demo.Interface.Repositories;
using Demo.Interface.Services;
using Demo.Repositories;
using Demo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAllowAccessRepository, AllowAccessRepository>();

builder.Services.AddScoped<IRoleService, RoleService>();
// builder.Services.AddScoped<IUserService, UserService>();
// builder.Services.AddScoped<IAllowAccessService, AllowAccessService>();


builder.Services.AddEndpointsApiExplorer(); 
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();


app.MapControllers();




app.Run();