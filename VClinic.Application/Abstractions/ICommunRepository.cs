using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Domain.Entities;

namespace VClinic.Application.Abstractions
{
    public interface ICommunRepository<T>
        where T : class
    {
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<T>> GetByAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);


        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

        Task<bool> HasRelatedDataAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> ExistsDataAsync(int id, string name, CancellationToken cancellationToken = default);

    }
}
