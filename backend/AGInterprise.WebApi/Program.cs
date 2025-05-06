using Serilog;
using Serilog.Events;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using AGInterprise.Infrastructure.Persistence;
using AGInterprise.Domain.Entities.Seguridad;
using AGInterprise.Application.Interfaces;
using AGInterprise.Infrastructure.Repositories;
using AGInterprise.WebApi.Services;
using AGInterprise.Infrastructure.Services;
using Microsoft.OpenApi.Models;
; // asumiendo tu ExceptionHandlingMiddleware

var builder = WebApplication.CreateBuilder(args);

// --- Serilog bootstrap ---
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft",                 LogEventLevel.Warning)
    .MinimumLevel.Override("System",                    LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {SourceContext}] {Message:lj}{NewLine}{Exception}")
    .CreateBootstrapLogger();

builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(ctx.Configuration)
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("System",    LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {SourceContext}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.File(
        path: "logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {SourceContext}] {Message:lj}{NewLine}{Exception}"
    )
);

// --- Servicios ---
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity con Usuario (PK int)
builder.Services.AddIdentity<Usuario, IdentityRole<int>>(opts =>
    {
        opts.User.RequireUniqueEmail = true;
        opts.Password.RequiredLength = 6;
        // ... otras políticas si las necesitas ...
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// JWT Authentication
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer              = builder.Configuration["Jwt:Issuer"],
            ValidAudience            = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey         = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddHttpContextAccessor();

// Repositorios
builder.Services.AddScoped<IAlmacenRepository, AlmacenRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();
builder.Services.AddScoped<IDetalleFacturaRepository, DetalleFacturaRepository>();
builder.Services.AddScoped<IMovimientoInventarioRepository, MovimientoInventarioRepository>();
builder.Services.AddScoped<IDetalleMovimientoRepository, DetalleMovimientoRepository>();
builder.Services.AddScoped<IInventarioRepository, InventarioRepository>();

// Servicios de negocio
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFacturaService, FacturaService>();
builder.Services.AddScoped<IBulkProductoService, BulkProductoService>();
builder.Services.AddScoped<IMovimientoInventarioService, MovimientoInventarioService>();

// Controladores + FluentValidation + JSON options
#pragma warning disable CS0618 // Type or member is obsolete
builder.Services.AddControllers()
    .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Program>())
    .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
#pragma warning restore CS0618 // Type or member is obsolete

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // 1) Definir el esquema de seguridad “Bearer”
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n" +
                      "Enter ‘Bearer’ [space] and then your token in the text input below.\r\n\r\n" +
                      "Example: \"Bearer eyJhbGciOiJIUzI1NiIsInR…\"",
        Name         = "Authorization",
        In           = ParameterLocation.Header,
        Type         = SecuritySchemeType.Http,
        Scheme       = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


// --- Construir la app ---
var app = builder.Build();

// Seed de roles fijos
using var scope = app.Services.CreateScope();
var svcProvider = scope.ServiceProvider;

// Seed de roles…
var roleMgr = svcProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
// … tu código de roles

// Seed del admin por defecto…
var userMgr = svcProvider.GetRequiredService<UserManager<Usuario>>();
if (!await userMgr.Users.AnyAsync())
{
    var admin = new Usuario {
        UserName = builder.Configuration["DefaultAdmin:Username"]!,
        Email    = builder.Configuration["DefaultAdmin:Email"]!
    };

    var result = await userMgr.CreateAsync(admin, builder.Configuration["DefaultAdmin:Password"]!);
    if (result.Succeeded)
        await userMgr.AddToRoleAsync(admin, "Administrador");
    else
    {
        var logger = svcProvider.GetRequiredService<ILogger<Program>>();
        foreach (var err in result.Errors)
            logger.LogError("Error creando Admin: {Code} {Desc}", err.Code, err.Description);
    }
}

// Middleware de manejo de excepciones
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

// Autenticación & Autorización
app.UseAuthentication();
app.UseAuthorization();

// Swagger UI en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
