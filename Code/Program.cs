using Microsoft.EntityFrameworkCore;
using Meowy.Models.Tweet;
using Meowy.Models.User;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<TweetContext>(opt =>
    opt.UseInMemoryDatabase("Tweet"));

builder.Services.AddDbContext<UserContext>(opt =>
    opt.UseInMemoryDatabase("User"));

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

