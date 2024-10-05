using Domain.BaseProjeEntities.FileEntities;
using Application.Repositories.ProductImageFileRepositories;
using Persistence.Contexts;
using Persistence.GenericRepositories;

namespace Persistence.Repositories.ImageFileRepository;

public class ImageFileReadRepository : ReadRepository<ImageFile, Guid, BaseDbContext>, IImageFileReadRepository
{
    public ImageFileReadRepository(BaseDbContext context) : base(context)
    {
    }
}
