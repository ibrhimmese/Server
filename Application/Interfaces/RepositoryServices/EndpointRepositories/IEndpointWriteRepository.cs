using Application.Interfaces.GenericRepositoryServices;
using Domain.BaseProjeEntities.IdentityEntities;

public interface IEndpointWriteRepository : IWriteRepository<Endpoint, Guid>
{
}
