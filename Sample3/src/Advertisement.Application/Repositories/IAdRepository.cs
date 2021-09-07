using System.Threading;
using System.Threading.Tasks;
using Advertisement.Domain;
using Advertisement.Domain.Shared;

namespace Advertisement.Application.Repositories
{
    public interface IAdRepository : IRepository<Ad, int>
    {
        Task<Ad> FindByIdWithUserInclude(int id, CancellationToken cancellationToken);
        
        Task<Ad> FindByIdWithUserAndCategory(int id, CancellationToken cancellationToken);
    }
}