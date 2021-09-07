using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Repositories;
using Advertisement.Domain;

namespace Advertisement.Infrastructure.DataAccess.Repositories
{
    public sealed class InMemoryRepository : 
        IRepository<Ad, int>,
        IRepository<User, int>
    {
        private readonly ConcurrentDictionary<int, User> _users = new();
        private readonly ConcurrentDictionary<int, Ad> _ads = new();

        async Task<Ad> IRepository<Ad, int>.FindById(int id, CancellationToken cancellationToken)
        {
            if (_ads.TryGetValue(id, out var ad))
            {
                _users.TryGetValue(ad.OwnerId, out var user);
                ad.Owner = user;
                return ad;
            }

            return null;
        }

        public async Task Save(User entity, CancellationToken cancellationToken)
        {
            if (entity.Id == 0)
            {
                entity.Id = Guid.NewGuid().GetHashCode();
            }

            _users.AddOrUpdate(entity.Id, (e) => entity, (i, user) => entity);
        }

        public async Task<User> FindWhere(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return _users.Select(pair => pair.Value).Where(compiled).FirstOrDefault();
        }

        async Task<int> IRepository<User, int>.Count(CancellationToken cancellationToken)
        {
            return _users.Count;
        }

        public async Task<int> Count(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return _users.Select(pair => pair.Value).Where(compiled).Count();
        }

        async Task<IEnumerable<User>> IRepository<User, int>.GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            return _users
                .Select(pair => pair.Value)
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(limit);
        }

        public async Task<IEnumerable<User>> GetPaged(Expression<Func<User, bool>> predicate, int offset, int limit, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return _users
                .Select(pair => pair.Value)
                .OrderBy(u => u.Id)
                .Where(compiled)
                .Skip(offset)
                .Take(limit);
        }

        public async Task Save(Ad entity, CancellationToken cancellationToken)
        {
            if (entity.Id == 0)
            {
                entity.Id = Guid.NewGuid().GetHashCode();
            }

            _ads.AddOrUpdate(entity.Id, (e) => entity, (i, user) => entity);
        }

        public async Task<Ad> FindWhere(Expression<Func<Ad, bool>> predicate, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return _ads.Select(pair => pair.Value).Where(compiled).FirstOrDefault();
        }

        async Task<User> IRepository<User, int>.FindById(int id, CancellationToken cancellationToken)
        {
            if (_users.TryGetValue(id, out var user))
            {
                return user;
            }

            return null;
        }

        async Task<int> IRepository<Ad, int>.Count(CancellationToken cancellationToken)
        {
            return _ads.Count;
        }

        public async Task<int> Count(Expression<Func<Ad, bool>> predicate, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return _ads.Select(pair => pair.Value).Where(compiled).Count();
        }

        async Task<IEnumerable<Ad>> IRepository<Ad, int>.GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            return _ads
                .Select(pair => pair.Value)
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(limit);
        }

        public async Task<IEnumerable<Ad>> GetPaged(Expression<Func<Ad, bool>> predicate, int offset, int limit, CancellationToken cancellationToken)
        {
            var compiled = predicate.Compile();
            return _ads
                .Select(pair => pair.Value)
                .OrderBy(u => u.Id)
                .Where(compiled)
                .Skip(offset)
                .Take(limit);
        }
    }
}