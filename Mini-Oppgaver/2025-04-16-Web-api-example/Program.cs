

using _2025_04_16_Web_api_example.Interfaces;
using _2025_04_16_Web_api_example.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IBookService, BookService>();

var app = builder.Build();

//чтобы иметь доступ к файлам в папке wwwroot
// https://localhost:7023/index.html
// https://localhost:7023/hello-world.txt
// http://localhost:5120/hello-world.txt
// http://localhost:5120/index.html
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.MapControllers();

app.Run();
