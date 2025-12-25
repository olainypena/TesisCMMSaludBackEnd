using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Domain.Entities;

namespace VClinic.Application.Abstractions
{
    public interface ICentroMedicoRepository
    {
        Task<CentroMedico?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<CentroMedico>> GetByAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(CentroMedico centroMedico, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);

        // NUEVOS
        Task UpdateAsync(CentroMedico centroMedico, CancellationToken cancellationToken = default);
        Task DeleteAsync(CentroMedico centroMedico, CancellationToken cancellationToken = default);

        /// <summary>
        /// Indica si el centro médico tiene datos relacionados en otras tablas
        /// (sucursales, médicos, pacientes, empleados, etc.).
        /// </summary>
        Task<bool> HasRelatedDataAsync(int idCentroMedico, CancellationToken cancellationToken = default);

        Task<bool> ExistsDataAsync(int id, string nombre, string rnc, CancellationToken cancellationToken = default);


    }
}
