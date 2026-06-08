using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Src.Application.Features.Query.ItemMetaData;
using Src.Application.Interfaces;
using Src.Application.Interfaces.Common;
using Src.Infrastructure;
using Src.Infrastructure.Persistance;
using Src.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IFavouriteRepository, FavouriteRepository>();
builder.Services.AddScoped<ICurrentUser, HttpCurrentUser>();

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

        ValidAudiences = new[] { builder.Configuration["Api:Audience"] },
        ValidIssuers = new[] { builder.Configuration["Api:Issuer"] },
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Api:key"]!)
        ),

        ValidateLifetime = true
    };

    jwtOptions.MapInboundClaims = false;
});

// 2. Configure token validation rules
// 3. Register authorization
// 4. UseAuthentication() to populate HttpContext.User -- this is done
// 5. UseAuthorization() to enforce [Authorize] -- this is done
// 6. Controllers can now use HttpContext.User / ICurrentUser

// for mediatr, scan Application assembly for handlers
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(ItemMetaDataGetRequestQueryHandler).Assembly);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    });
}

Console.WriteLine(app.Environment.EnvironmentName);
Console.WriteLine(app.Environment.IsDevelopment());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
