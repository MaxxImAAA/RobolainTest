using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robolain.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        Task CreateAsync(T entity);

        Task DeleteAsync(T entity);

        Task UpdateAsync(T entity);

        

    }
}
