using Application.Interfaces.GenericRepositoryServices;
using Domain.BaseProjeEntities.FileEntities;

namespace Application.Repositories.ProductImageFileRepositories;

public interface IImageFileWriteRepository: IWriteRepository<ImageFile, Guid>
{

}
