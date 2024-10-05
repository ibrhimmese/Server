using Application.Interfaces.GenericRepositoryServices;
using Domain.BaseProjeEntities.IdentityEntities;

namespace Application.Repositories.MenuRepositories;

public interface IMenuWriteRepository : IWriteRepository<Menu, Guid>
{

}