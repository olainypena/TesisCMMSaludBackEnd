using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Domain.Entities;
using VClinic.Domain.Entities.Catalogos;

namespace VClinic.Application.Abstractions
{
    public interface IEquipamentoRepository
    {
        Task<Equipamento?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Equipamento>> GetByAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Equipamento equipamento, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);

       
        Task UpdateAsync(Equipamento equipamento, CancellationToken cancellationToken = default);
        Task DeleteAsync(Equipamento equipamento, CancellationToken cancellationToken = default);
                
        Task<bool> HasRelatedDataAsync(int equipamento, CancellationToken cancellationToken = default);

        Task<bool> ExistsDataAsync(int id, string equipamento, CancellationToken cancellationToken = default);

    }
}
