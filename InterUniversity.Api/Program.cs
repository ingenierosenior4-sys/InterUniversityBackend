using InterUniversity.Api.Extensions;
using InterUniversity.Api.Middlewares;
using InterUniversity.Application;
using InterUniversity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

string specificOrigins = "specificOrigins";

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerExtension();
builder.Services.AddJwtAuthenticationExtension(builder.Configuration);
builder.Services.AddCorsExtension(builder.Configuration, specificOrigins);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(specificOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

await app.RunAsync();
