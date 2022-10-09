using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Zygieldesk.Application;
using Zygieldesk.Application.Authentication;
using Zygieldesk.Application.Middlewares;
using Zygieldesk.Application.PipelineBehaviors;
using Zygieldesk.Persistance;
using Zygieldesk.Persistance.Seeder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
    };
});

builder.Services.AddZygieldeskApplication();
builder.Services.AddZygieldeskPersistanceEFServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Open",
        builder => builder.AllowAnyOrigin()
        .AllowAnyHeader().AllowAnyMethod());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Description = $"JWT Authorization.\r\n\r\n Paste jwtToken you receive from logging in,  into Value field and " +
            $"press Authorize button.",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer"
        });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement{
    {
        new OpenApiSecurityScheme{
            Reference = new OpenApiReference{
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
            }
        },new List<string>()
    }
});
});

builder.Services.AddDbContext<ZygieldeskDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ZygieldeskConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<ZygieldeskSeeder>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Open");

seeder.Seed();

app.UseAuthentication();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();