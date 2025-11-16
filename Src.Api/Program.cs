using Mapster;
using MapsterMapper;
using QuoteApi.Api.ServiceExtensions;
using Src.Application.Features.Query.ItemMetaData;
using Src.Application.Interfaces;
using Src.Application.Interfaces.Common;
using Src.Infrastructure;
using Src.Infrastructure.Repository;

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
builder.Services.AddScoped<ICurrentUser, HttpCurrentUser>();

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
app.UseAuthorization();
app.MapControllers();
app.Run();
