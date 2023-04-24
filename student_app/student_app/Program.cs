using Microsoft.EntityFrameworkCore;
using student_app.Models;
using student_app.Repository.RepositoryManager;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
}); ;


builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF dpendancy inject
builder.Services.AddDbContext<APPDBContext>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"))
);

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
