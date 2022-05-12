using Microsoft.EntityFrameworkCore;
using Meowy_deneme.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<TweetContext>(opt =>
    opt.UseInMemoryDatabase("Tweet"));

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// deneme commitiii gitignore check !!!!!!!!!!!!!!!!!!!!!!!!

