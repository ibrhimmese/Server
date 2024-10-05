using Domain.BaseProjeEntities.FileEntities;
using Application.Repositories.ProductImageFileRepositories;
using Persistence.Contexts;
using Persistence.GenericRepositories;

namespace Persistence.Repositories.ImageFileRepository
{
    public class ImageFileWriteRepository : WriteRepository<ImageFile, Guid, BaseDbContext>, IImageFileWriteRepository
    {
        public ImageFileWriteRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
