using Domain.BaseProjeEntities.IdentityEntities;
using Application.Repositories.MenuRepositories;
using Persistence.Contexts;
using Persistence.GenericRepositories;

namespace Persistence.Repositories.MenuRepository;

public class MenuWriteRepository : WriteRepository<Menu, Guid, BaseDbContext>, IMenuWriteRepository
{
    public MenuWriteRepository(BaseDbContext context) : base(context)
    {
    }
}

