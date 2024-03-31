using Employees.API.Mapping;
using Employees.Core.Mapping;
using Employees.Core.Repositories;
using Employees.Core.Services;
using Employees.Data;
using Employees.Data.Repositories;
using Employees.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var policy = "policy";
builder.Services.AddCors(option => option.AddPolicy(name: policy, policy =>
{
    policy.AllowAnyOrigin(); policy.AllowAnyHeader(); policy.AllowAnyMethod();
}));


//Service injections
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IPositionService, PositionService>();

//Repository injections
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();


//DataContext injection
builder.Services.AddDbContext<DataContext>();


//AutoMapper injection
builder.Services.AddAutoMapper(typeof(MappingProfile), typeof(ModelMappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(policy);

app.MapControllers();

app.Run();