using Api.DataContext.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api.DataContext.DataRepositories
{
    public class EfDataRepository<T> : IDataRepositiry<T> where T : BaseEntity
    {
        private readonly EfDbContext _db;

        protected DbSet<T> Set { get; }

        protected virtual IQueryable<T> Items => Set;

        public EfDataRepository(EfDbContext db)
        {
            _db = db;
            Set = _db.Set<T>();
        }

        public async Task<int> Create(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            await _db.Set<T>().AddAsync(item, cancel).ConfigureAwait(false);
            await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
            return item.Id;
        }

        public async Task<bool> Delete(int id, CancellationToken cancel = default)
        {
            var item = Set.Local.FirstOrDefault(x => x.Id == id);

            if (item is null)
            {
                item = await Set.FirstOrDefaultAsync(x => x.Id == id, cancel).ConfigureAwait(false);
            }

            if (item is null)
            {
                return false;
            }

            _db.Set<T>().Remove(item);
            await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
            return true;
        }

        public async Task<IQueryable<T>> Read(CancellationToken cancel = default)
        {
            var result = await Task.Run(() => _db.Set<T>(), cancel).ConfigureAwait(false);
            return result;
        }

        public async Task<T> ReadById(int id, CancellationToken cancel = default)
        {

            switch(Items)
            {
                case DbSet<T> set:
                    return await set.FindAsync(new object[] { id }, cancel).ConfigureAwait(false);

                case IQueryable<T> items:
                    return await items.FirstOrDefaultAsync(item => item.Id == id, cancel).ConfigureAwait(false);

                default: throw new InvalidOperationException();
            }           
        }

        public async Task<int> Update(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            _db.Update(item);
            await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
            return item.Id;
        }
    }
}

