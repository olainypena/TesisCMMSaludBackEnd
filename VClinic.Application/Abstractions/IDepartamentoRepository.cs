using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Domain.Entities;
using VClinic.Domain.Entities.Catalogos;

namespace VClinic.Application.Abstractions
{
    public interface IDepartamentoRepository
    {
        Task<Departamento?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Departamento>> GetByAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Departamento departamento, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);

       
        Task UpdateAsync(Departamento departamento, CancellationToken cancellationToken = default);
        Task DeleteAsync(Departamento departamento, CancellationToken cancellationToken = default);
                
        Task<bool> HasRelatedDataAsync(int departamento, CancellationToken cancellationToken = default);

        Task<bool> ExistsDataAsync(int id, string departamento, CancellationToken cancellationToken = default);

    }
}
