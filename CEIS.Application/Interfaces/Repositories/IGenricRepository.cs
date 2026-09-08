using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Interfaces.Repositories
{
    public interface IGenricRepository<T> where T : class
    {
        // Read
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

        // Write
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);

        // Check
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    }
}
