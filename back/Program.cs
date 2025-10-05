using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using back.Context;
using back.Token;
using back.Hubs;
using Microsoft.IdentityModel.Logging;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:5194");
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSignalR();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Insira o token JWT no formato: Bearer {seu_token}",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            securityScheme,
            Array.Empty<string>()
        }
    });
});


var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<NasaChallengeContextDb>(c =>
{
    c.UseSqlServer(connectionString);
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddMvc();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

var tokenGenerator = new SymetricGenerator();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = tokenGenerator.Generator()
    };
    opt.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            // O erro de autenticação estará aqui
            Console.WriteLine($"[JWT ERROR] Causa: {context.Exception.Message}");
            IdentityModelEventSource.ShowPII = true;
            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

app.UseCors("AllowAny");

if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapHub<DependentsHub>("/dependentsHub");

app.MapControllers();

app.Run();