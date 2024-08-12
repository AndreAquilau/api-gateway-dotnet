using APIGateway.Api;
using APIGateway.Api.Middlewares;
using APIGateway.Domain.CEP.Services;
using APIGateway.Infrastructure.CEPService.Services;
using Microsoft.OpenApi.Models;
using APIGateway.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RequestDatabaseSettings>(
    builder.Configuration.GetSection("GatewayRequestStoreDatabase"));

builder.Services.Configure<ReponseDatabaseSettings>(
    builder.Configuration.GetSection("GatewayResponseStoreDatabase"));

// Add services to the container.
builder.Services.UseKafka(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<MiddlewareRequest>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API Gateway Corebanking",
        Description = "An ASP.NET Core Web API Gateway",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://example.com/license")
        }
    });

    // using System.Reflection;
    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    //options.IncludeXmlComments(xmlPath);

});


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors(op => op.AllowAnyOrigin());

app.MapControllers();

app.UseMiddleware<MiddlewareRequest>();

app.Run();