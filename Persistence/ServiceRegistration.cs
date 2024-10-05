using Application.JwtTokenHandlerInterface;
using Application.JwtTokenHandlerInterface.AuthenticationsInterfaces;
using Application.Repositories.EndpointRepositories;
using Application.Repositories.MenuRepositories;
using Application.Repositories.ProductImageFileRepositories;
using Application.StorageInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Authorize;
using Persistence.Contexts;
using Persistence.Repositories.EndpointRepository;
using Persistence.Repositories.ImageFileRepository;
using Persistence.Repositories.MenuRepository;

namespace Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSql")));
        //services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MSSQL")));

        services.AddScoped<IImageFileReadRepository, ImageFileReadRepository>();
        services.AddScoped<IImageFileWriteRepository, ImageFileWriteRepository>();

        services.AddScoped<IMenuWriteRepository, MenuWriteRepository>();
        services.AddScoped<IMenuReadRepository, MenuReadRepository>();

        services.AddScoped<IEndpointWriteRepository, EndpointWriteRepository>();
        services.AddScoped<IEndpointReadRepository, EndpointReadRepository>();

        services.AddScoped<IAuthorizationEndpointService, AuthorizationEndpointService>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
    }
}
