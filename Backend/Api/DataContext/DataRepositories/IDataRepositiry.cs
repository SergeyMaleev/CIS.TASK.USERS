using Api.DataContext.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api.DataContext.DataRepositories
{
    public interface IDataRepositiry<T> where T : BaseEntity
    {
        Task<int> Create(T item, CancellationToken cancel = default);
        Task<IQueryable<T>> Read(CancellationToken cancel = default);
        Task<T> ReadById(int id, CancellationToken cancel = default);
        Task<int> Update(T item, CancellationToken cancel = default);
        Task<bool> Delete(int id, CancellationToken cancel = default);
    }
}
