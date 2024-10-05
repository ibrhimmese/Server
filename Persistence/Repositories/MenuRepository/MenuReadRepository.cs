using Domain.BaseProjeEntities.IdentityEntities;
using Application.Repositories.MenuRepositories;
using Persistence.Contexts;
using Persistence.GenericRepositories;

namespace Persistence.Repositories.MenuRepository;

public class MenuReadRepository : ReadRepository<Menu, Guid, BaseDbContext>, IMenuReadRepository
{
    public MenuReadRepository(BaseDbContext context) : base(context)
    {
    }
}

