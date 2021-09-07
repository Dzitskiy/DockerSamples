using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Repositories;
using Advertisement.Domain;
using Advertisement.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Advertisement.Infrastructure.DataAccess.Repositories
{
    public sealed class AdRepository : EfRepository<Ad, int>, IAdRepository
    {
         public AdRepository(DatabaseContext dbСontext) : base(dbСontext)
        {
        }

        public async Task<Ad> FindByIdWithUserInclude(int id, CancellationToken cancellationToken)
        {
            return await DbСontext
                .Set<Ad>()
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task<Ad> FindByIdWithUserAndCategory(int id, CancellationToken cancellationToken)
        {
            return await DbСontext
                .Set<Ad>()
                .Include(a => a.Owner)
                .Include(a => a.Category)
                .Include(a => a.Category.ChildCategories)
                .Include(a => a.Category.ParentCategory)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }
    }
}