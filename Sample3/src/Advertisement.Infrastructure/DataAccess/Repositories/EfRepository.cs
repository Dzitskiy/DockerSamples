using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Repositories;
using Advertisement.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Advertisement.Infrastructure.DataAccess.Repositories
{
    public class EfRepository<TEntity, TId>
        : IRepository<TEntity, TId>
        where TEntity : Entity<TId>
    {

        protected readonly DatabaseContext DbСontext;

        public EfRepository(DatabaseContext dbСontext)
        {
            DbСontext = dbСontext;
        }

        public async Task<TEntity> FindById(TId id, CancellationToken cancellationToken)
        {
            return await DbСontext.FindAsync<TEntity>(new object[] {id}, cancellationToken: cancellationToken);
        }

        public async Task Save(TEntity entity, CancellationToken cancellationToken)
        {
            var entry = DbСontext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                await DbСontext.AddAsync(entity, cancellationToken);
            }

            await DbСontext.SaveChangesAsync(cancellationToken);
        }

        public async Task<TEntity> FindWhere(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken)
        {
            var data = DbСontext.Set<TEntity>().AsNoTracking();;

            return await data.Where(predicate).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> Count(CancellationToken cancellationToken)
        {
            var data = DbСontext.Set<TEntity>().AsNoTracking();;

            return await data.CountAsync(cancellationToken);
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {

            var data = DbСontext.Set<TEntity>().AsNoTracking();;

            return await data.Where(predicate).CountAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            var data = DbСontext.Set<TEntity>().AsNoTracking();;

            return await data.OrderBy(e => e.Id).Take(limit).Skip(offset).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetPaged(Expression<Func<TEntity, bool>> predicate, int offset,
            int limit, CancellationToken cancellationToken)
        {
            var data = DbСontext.Set<TEntity>().AsNoTracking();

            return await data.Where(predicate).OrderBy(e => e.Id).Take(limit).Skip(offset)
                .ToListAsync(cancellationToken);
        }
    }
}