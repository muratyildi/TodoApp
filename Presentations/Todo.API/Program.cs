using Data.EntityFramework;
using JwtManager.Configurations;
using JwtManager.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    c.CustomSchemaIds(x => x.FullName);


    //Swagger arayüzüne jwt auth ekliyoruz.
    #region JWT Auth

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Lütfen JWT Bearer Token değerini giriniz.",

        Reference = new OpenApiReference
        {
            Id = "Authorization",
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });

    #endregion
});

#region JWT Auth

builder.Services.AddJwtManager(builder.Configuration);

var serviceProvider = builder.Services.BuildServiceProvider();
var jwtHandler = serviceProvider.GetService<IJwtHandler>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = jwtHandler.Parameters;
    });

#endregion

//#region PostgreSql
//builder.Services.AddDbContext<DataContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), x => x.EnableRetryOnFailure()));
//#endregion

#region Entity Framework

builder.Services.AddDbContext<DataContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), x => x.EnableRetryOnFailure()); });

#endregion



#region AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
