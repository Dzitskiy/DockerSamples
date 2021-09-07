using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Repositories;
using Advertisement.Application.Services.Ad.Contracts;

namespace Advertisement.Application.Services.Ad.Interfaces
{
    public interface IAdService
    {
        Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken);

        Task Pay(Pay.Request request, CancellationToken cancellationToken);
        
        Task Delete(Delete.Request request, CancellationToken cancellationToken);

        Task<Get.Response> Get(Get.Request request, CancellationToken cancellationToken);
        Task<GetPaged.Response> GetPaged(GetPaged.Request request, CancellationToken cancellationToken);
    }
}