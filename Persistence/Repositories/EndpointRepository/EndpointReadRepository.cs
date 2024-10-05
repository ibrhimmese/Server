using Domain.BaseProjeEntities.IdentityEntities;
using Application.Repositories.EndpointRepositories;
using Persistence.Contexts;
using Persistence.GenericRepositories;

namespace Persistence.Repositories.EndpointRepository;

public class EndpointReadRepository : ReadRepository<Endpoint, Guid, BaseDbContext>, IEndpointReadRepository
{
    public EndpointReadRepository(BaseDbContext context) : base(context)
    {
    }
}
