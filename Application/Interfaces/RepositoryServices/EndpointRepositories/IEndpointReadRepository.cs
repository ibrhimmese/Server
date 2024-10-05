using Application.Interfaces.GenericRepositoryServices;
using Domain.BaseProjeEntities.IdentityEntities;

namespace Application.Repositories.EndpointRepositories
{
    public interface IEndpointReadRepository : IReadRepository<Endpoint, Guid>
    {
    }
}
