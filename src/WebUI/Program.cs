using System.Reflection;
using gql.Application.Gql.Queries;
using gql.Application.Gql.Schemas;
using gql.Application.Gql.Types;
using gql.Infrastructure.Persistence;
using GraphQL;
using GraphQL.Execution;
using GraphQL.Types;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog.Core;
using Serilog;
using WebUI.Middleware;
using Serilog.Debugging;

var builder = WebApplication.CreateBuilder(args);

// Config logging
var loggerFactory = new LoggerFactory()
    .AddSerilog(new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .WriteTo.File("/logs/log", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger());
builder.Services.AddSingleton<ILoggerFactory>(loggerFactory);

// Add swagger
builder.Services.AddSwaggerGen(options =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Type = SecuritySchemeType.ApiKey,
        Description = "Put \"Bearer {token}\" your JWT Bearer token on textbox below!",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = JwtBearerDefaults.AuthenticationScheme
        },
    };
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, jwtSecurityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            jwtSecurityScheme,
            new List<string>()
        }
    });
});

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebUIServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();

    // Initialise and seed database
    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.ConfigureExceptionHandler(app.Logger);
app.UseHttpsRedirection();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger(b => {
        b.SerializeAsV2 = true;
    });
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");


// Setup Graphql
app.UseGraphQL<ISchema>(path: "/graphql");
if (app.Environment.IsDevelopment())
{
    app.UseGraphQLPlayground();
}

app.MapRazorPages();

app.MapFallbackToFile("index.html"); ;

app.Run();
