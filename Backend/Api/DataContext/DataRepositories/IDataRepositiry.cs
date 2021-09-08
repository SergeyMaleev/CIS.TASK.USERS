using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DataContext.DataRepositories
{
    public interface IDataRepositiry
    {
        Task<T> Get<T>(string name, CancellationToken cancel = default) where T : class, IEntity;
    }
}
