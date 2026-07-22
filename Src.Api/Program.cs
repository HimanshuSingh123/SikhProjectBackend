using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Src.Infrastructure.Persistance;
using System.Text;
using Src.Api.ServiceExtensions;
using Src.Application.Features.ItemMetaData.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHealthChecks();

// Mapster: scan API assembly for IRegister profiles (ItemProfile, etc.)
//MapsterConfig.Configure();
builder.Services.AddSingleton(TypeAdapterConfig.GlobalSettings);
builder.Services.AddScoped<IMapper, ServiceMapper>();

// Repos / user context
StartupServiceExtensions.HandleRepositoryScopedServices(builder);

//dbcontext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// 1. Register JWT Bearer authentication scheme

builder.Services.AddAuthentication()
.AddJwtBearer(jwtOptions =>
{
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,

        ValidAudiences = [builder.Configuration["Api:Audience"]],
        ValidIssuers = [builder.Configuration["Api:Issuer"]],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Api:key"]!)
        ),

        ValidateLifetime = true
    };

    jwtOptions.MapInboundClaims = false;
});



// for mediatr, scan Application assembly for handlers
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(ItemMetaDataGetRequestQueryHandler).Assembly);
});

StartupServiceExtensions.HandleRoles(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    });
}

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
var scope = scopeFactory.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
var logger = loggerFactory.CreateLogger("StartupServiceExtensions");

await StartupServiceExtensions.SyncRolesAsync(context, logger);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
