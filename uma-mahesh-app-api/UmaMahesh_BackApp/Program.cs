global using UmaMahesh_BackApp.Features.Custom.Email;
global using UmaMahesh_BackApp.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Enrichers.AspnetcoreHttpcontext;
using Microsoft.AspNetCore.Mvc.Versioning;
using System.Reflection;
using Logger.Common.LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UmaMahesh_BackApp.Exceptions.Global;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApiVersioning(
    options =>
    {
        options.ReportApiVersions = true;
        options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ApiVersionReader = new QueryStringApiVersionReader("uma-api-version");
        //options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataRepositoryContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("UmaMaheshConnectionString")));

builder.Services.AddDbContext<MongoDBRepositoryContext>(options  =>
            options.UseMongoDB(builder.Configuration.GetConnectionString("UmaMongoDB")!,
                               builder.Configuration.GetSection("AppSettings:MongoDBName").Value!));

builder.Services.AddScoped<EmailService>()
                .AddExceptionHandler<AppExceptionHandler>()
                .AddExceptionHandler<GeneralExceptionHandler>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("JwtSettings:Key").Value!)),
                ValidAudience= builder.Configuration.GetSection("JwtSettings:Audience").Value!,
                ValidIssuer= builder.Configuration.GetSection("JwtSettings:Issuer").Value!,
                ValidateAudience =false,
                ValidateIssuer=false
            };

            x.Events = new JwtBearerEvents 
            {                
                OnChallenge = context =>
                {
                    throw new UnauthorizedAccessException();
                }

            };


        });


builder.WebHost.UseSerilog((provider, context, loggerConfig) =>
LoggerService.WithSimpleConfiguration(loggerConfig, provider, Assembly.GetExecutingAssembly().FullName, builder.Configuration)
//LoggerServiceLocal.WithSinkConfiguration(loggerConfig, provider, Assembly.GetExecutingAssembly().FullName, builder.Configuration)
);

builder.Services.AddCors(
options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
        //.WithOrigins("*")
        //.WithHeaders("X-API-Version");
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();    
}
else
{
    app.UseHsts();
}

app.UseExceptionHandler(_ => { });

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

//app.UseSerilogRequestLogging();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
//app.UseCors("corspolicy");

app.MapControllers();

app.Run();
