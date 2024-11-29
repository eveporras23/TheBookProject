using Microsoft.AspNetCore.Http.Headers;
using Microsoft.OpenApi.Models;
using TheBookProject.Context;
using TheBookProject.Services;

var builder = WebApplication.CreateBuilder(args);

    
// add the authentication / authorization services
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(defaultScheme: "Bearer").AddJwtBearer();

// add dbcontext
builder.Services.AddDbContext<TheBookProjectDbContext>();

// Add services to the container.

builder.Services.AddControllers();
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
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);


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

string? GoodReadsAPIKey = app.Configuration.GetValue<string>("AppSettings:GoodReadsAPIKey");
 
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

app.MapControllers();

app.Run();