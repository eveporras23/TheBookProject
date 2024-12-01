using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TheBookProject.Db.Context;
using TheBookProject.Middlewares;
using TheBookProject.Services;

var builder = WebApplication.CreateBuilder(args);

    
// add the authentication / authorization services
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(defaultScheme: "Bearer").AddJwtBearer();
 
// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "TheBookProject", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a token",
        Name ="Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id ="Bearer"
                }
            },
            new string[]{}
        }
    });
});


//add the app settings file 
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);


// ADD reverse proxy
builder.Services.AddHttpClient<IGoodReadsService, GoodReadsService>("GoodReadsAPI",client =>
{
    client.BaseAddress = new Uri("https://goodreads12.p.rapidapi.com"); 
    client.DefaultRequestHeaders.Add("x-rapidapi-key", builder.Configuration.GetValue<string>("APIKeys:GoodReads"));
    client.DefaultRequestHeaders.Add("x-rapidapi-host", builder.Configuration.GetValue<string>("APIHost:GoodReads"));
});

builder.Services.AddHttpClient<IGoogleBooksService, GoogleBooksService>("GoogleBooksAPI",client =>
{
    client.BaseAddress = new Uri("https://www.googleapis.com/"); 
});

builder.Services.AddTheBookProjectContext(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddScoped<IBookService,BookService>();
builder.Services.AddScoped<IReviewService,ReviewService>();
builder.Services.AddScoped<IGoodReadsService, GoodReadsService>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var dbContext = provider.GetRequiredService<TheBookProjectDbContext>();
    var bookService = provider.GetRequiredService<IBookService>();
    var reviewService = provider.GetRequiredService<IReviewService>();
    return new GoodReadsService(httpClientFactory, dbContext,bookService, reviewService);
});
builder.Services.AddScoped<IGoogleBooksService, GoogleBooksService>(provider =>
{
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var dbContext = provider.GetRequiredService<TheBookProjectDbContext>();
    var bookService = provider.GetRequiredService<IBookService>();
    return new GoogleBooksService(httpClientFactory, dbContext,bookService);
});


var app = builder.Build();

bool isMaintainanceMode = app.Configuration.GetValue<bool>("isMaintainanceMode");

app.UseCustomExceptionHandlerMiddleware();

// add middleware authorization
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<MaintenanceModeMiddleware>(isMaintainanceMode);

app.MapControllers();

app.MapGet("/Maintenance", () => "Estamos en mantenimiento");

app.Run();