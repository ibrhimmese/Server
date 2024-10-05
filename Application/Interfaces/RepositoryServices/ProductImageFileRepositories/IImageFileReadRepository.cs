using Application.Interfaces.GenericRepositoryServices;
using Domain.BaseProjeEntities.FileEntities;

namespace Application.Repositories.ProductImageFileRepositories;

public interface IImageFileReadRepository:IReadRepository<ImageFile, Guid>
{
}
