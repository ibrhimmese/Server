using Application.Interfaces.GenericRepositoryServices;
using Domain.BaseProjeEntities.IdentityEntities;

namespace Application.Repositories.MenuRepositories;

public interface IMenuReadRepository : IReadRepository<Menu, Guid>
{
}
