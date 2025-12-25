using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Application.Abstractions;
using VClinic.Domain.Entities.Catalogos;
using VClinic.Infrastructure.Persistence;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VClinic.Infrastructure.Repositories
{
    public class GrupoSanguinioRepository : ICommunRepository<GrupoSanguinio>
    {
        private readonly AppDbContext _context;

        public GrupoSanguinioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(GrupoSanguinio entity, CancellationToken cancellationToken = default)
        {
            await _context.GrupoSanguinios.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(GrupoSanguinio entity, CancellationToken cancellationToken = default)
        {
            _context.GrupoSanguinios.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(GrupoSanguinio entity, CancellationToken cancellationToken = default)
        {
            _context.GrupoSanguinios.Update(entity);
            await Task.CompletedTask; // para respetar la firma async
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<GrupoSanguinio>> GetByAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.GrupoSanguinios
                 .AsNoTracking()
                 .ToListAsync(cancellationToken);
        }

        public async Task<GrupoSanguinio?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.GrupoSanguinios
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IdGrupoSangre == id, cancellationToken);
        }

        public async Task<bool> HasRelatedDataAsync(int id, CancellationToken cancellationToken = default)
        {
            return true;
        }

        public async Task<bool> ExistsDataAsync(int id, string name, CancellationToken cancellationToken = default)
        {
            var existeNombre = await _context.GrupoSanguinios
               .AnyAsync(s => s.IdGrupoSangre != id && s.TipoSangre.ToUpper() == name.ToUpper(), cancellationToken);

            return existeNombre;
        }
    }
}
