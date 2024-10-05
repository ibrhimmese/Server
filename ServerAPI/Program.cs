
using Domain.BaseProjeEntities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Persistence.Contexts;
using ServerAPI.AuthorizationFiters;
using Persistence;
using Application;
using Infrastructure;
using Application.ExceptionTypes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.OpenApi.Models;
using Infrastructure.Exceptions.Extensions;
using Infrastructure.StorageConcretes.Local;
namespace ServerAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);



        //builder.Services.AddControllers(options =>
        //{
        //    options.Filters.Add<RolePermissionFilter>();
        //});

        builder.Services.AddControllers();
        builder.Services.AddScoped<AuthenticationFilter>();
        builder.Services.AddScoped<RolePermissionFilter>();

        builder.Services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.Password.RequiredLength = 3;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;

        }).AddEntityFrameworkStores<BaseDbContext>()
           .AddDefaultTokenProviders();

        builder.Services.AddPersistenceServices(builder.Configuration);

        builder.Services.AddApplicationServices();//

        builder.Services.AddInfrastructureServices(); //

        builder.Services.AddStorage<LocalStorage>(); //

        builder.Services.AddDistributedMemoryCache();
        //builder.Services.AddStackExchangeRedisCache(opt=>opt.Configuration="localhost:6379");

        builder.Services.AddHttpContextAccessor();//

        builder.Services.AddEndpointsApiExplorer();
        // builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options => //Cors Settings
        {
            options.AddDefaultPolicy(policy =>
            policy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed(policy => true));
        }); //Addedd

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Admin", options =>
        {
            var securityKey = builder.Configuration["Token:SecurityKey"];
            if (string.IsNullOrEmpty(securityKey))
            {
                throw new OperationException("Security key is not configured.");
            }

            options.TokenValidationParameters = new()
            {
                ValidateAudience = true,            //Oluþturulacak tokenin kimlerin hangi sitelerin kullanacaðýný belirteceðiz.
                ValidateIssuer = true,              //Oluþturulacak token deðerini kimin daðýttýðýný ifade edecez.
                ValidateLifetime = true,            //Oluþturulan token deðerinin süresini kontrol edecek olan doðrulamadýr.
                ValidateIssuerSigningKey = true,    //Üretilecek token deðerinin uygulamamýza ait bir deðer olduðunu ifade eden secury key verisini doðrulamasýdýr.

                ValidAudience = builder.Configuration["Token:Audience"],
                ValidIssuer = builder.Configuration["Token:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),
                LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,

                NameClaimType = ClaimTypes.Name
            };
        });


        builder.Services.AddSwaggerGen(setup => //Addedd
        {
            var jwtSecuritySheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecuritySheme, Array.Empty<string>() }
                });
        }); //Jwt

        var app = builder.Build();

     
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        if (app.Environment.IsDevelopment())
        {
            app.ConfigureCustomExceptionMiddleware(); //Middleware
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
