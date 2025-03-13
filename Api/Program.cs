using System.Text;
using System.Threading.RateLimiting;
using Application.Auth.Register;
using Application.Books.Commands.Create;
using Application.Books.Commands.Update;
using Application.Books.Queries.GetAll;
using Application.Library.Commands.AddBook;
using Application.Library.Commands.Create;
using Application.Library.Commands.Update;
using Application.People.Commands.AddBook;
using Application.People.Commands.Create;
using Application.People.Commands.Update;
using Application.Transaction;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TokenHandler = Application.Token.TokenHandler;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<CreateBookCommandValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<UpdateBookCommandValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<RegisterCommandValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<CreatePersonCommandValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<UpdatePersonCommandValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<CreateLibraryCommandValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<UpdateLibraryCommandValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<AddBookToPersonCommandValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<AddBookToLibraryCommandValidator>();
    });

builder.Services.RegisterRepositories();
builder.Services.AddTransient<TokenHandler>();
builder.Services.AddTransient(typeof(DbTransactionHandlerMiddleware<>));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllBooksQuery).Assembly));

builder.Services.AddDistributedMemoryCache();

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 5;
        options.QueueLimit = 3;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidAudience = builder.Configuration["Token:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (builder.Configuration["Token:SecurityKey"])),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    option.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        }
    );
    option.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        }
    );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<DbTransactionHandlerMiddleware<DataContext>>();
app.UseRateLimiter();
app.MapControllers().RequireRateLimiting("fixed");

app.Run();