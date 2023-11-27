using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxCalculations.API.Extensions;
using TaxCalculations.API.Services;
using TaxCalculations.Application.Contracts.Services;
using TaxCalculations.Identity.Contexts;
using TaxCalculations.Persistence.Contexts;
using TaxCalculations.Identity;
using TaxCalculations.Persistence;
using TaxCalculations.Application;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager _config = builder.Configuration;

builder.Services.AddApplicationLayer();
builder.Services.AddIdentityInfrastructure(_config);
builder.Services.AddPersistenceInfrastructure(_config);
builder.Services.AddSwaggerExtension();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddApiVersioningExtension();
builder.Services.AddHealthChecks();
builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Services.AddCors();
builder.Services.AddResponseCaching();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Log/application.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSerilog();
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerExtension();
app.UseErrorHandlingMiddleware();
app.UseHealthChecks("/health");
app.UseResponseCaching();
app.MapControllers();

app.Run();

Log.CloseAndFlush();

public partial class Program { }