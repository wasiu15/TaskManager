using ApprovalEngine.Shared.Logger;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureDatabaseContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureHttpclient();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureAuthorization();
builder.Services.ConfigureTokenManager();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureHangfire(builder.Configuration);
builder.Services.AddHangfireServer(options => options.WorkerCount = 1);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Host.UseSerilog(Logger.Configure);
builder.Services.ConfigureSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHangfireDashboard();
app.UseHangfireServer();

app.UseSerilogRequestLogging(opt => opt.EnrichDiagnosticContext = LogEnricher.EnrichFromRequest);
app.UseHttpsRedirection();

app.ConfigureExceptionHandler();
app.UseAuthorization();

app.MapControllers();

app.Run();
