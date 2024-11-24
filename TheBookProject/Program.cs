using Microsoft.AspNetCore.Http.Headers;
using TheBookProject.Context;
using TheBookProject.Services;

var builder = WebApplication.CreateBuilder(args);
 // add dbcontext
    
builder.Services.AddDbContext<TheBookProjectDbContext>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ADD API
builder.Services.AddHttpClient<IGoodReadsService, GoodReadsService>("GoodReadsAPI",client =>
{
    client.BaseAddress = new Uri("https://goodreads12.p.rapidapi.com"); 
    client.DefaultRequestHeaders.Add("x-rapidapi-key", "cecc827dbbmsh879fc314bc7573fp16bf4fjsn0d4917e5d591");
    client.DefaultRequestHeaders.Add("x-rapidapi-host", "goodreads12.p.rapidapi.com");
});

builder.Services.AddHttpClient<IGoogleBooksService, GoogleBooksService>("GoogleBooksAPI",client =>
{
    client.BaseAddress = new Uri("https://www.googleapis.com/"); 
});

builder.Services.AddScoped<IGoodReadsService, GoodReadsService>();
builder.Services.AddScoped<IGoogleBooksService, GoogleBooksService>();

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