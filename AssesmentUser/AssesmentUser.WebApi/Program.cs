using AssesmentUser.Persistance;
using AssesmentUser.Application;
using AssesmentUser.Infrastructure;
using Microsoft.OpenApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AssesmentUser.Application.Seeds;
using AssesmentUser.WebApi.Infrastructures;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<BaseResponseHandling>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "AssesmentUser", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

//add layer of Design Patter Clean Arcitecture
#region Depedency Injection
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPersistance(builder.Configuration);
#endregion

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    ApplicationDbContext appDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var dbContext = (DbContext)appDbContext;
    dbContext.Database.Migrate();

    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
    mediator.Send(new UserSeedCommand()).GetAwaiter().GetResult();
}

app.Run();

public partial class Program { }
