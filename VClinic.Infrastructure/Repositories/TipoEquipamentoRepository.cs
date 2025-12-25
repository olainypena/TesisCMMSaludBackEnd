using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Application.Abstractions;
using VClinic.Domain.Entities.Catalogos;
using VClinic.Infrastructure.Persistence;

namespace VClinic.Infrastructure.Repositories
{
    public class TipoEquipamentoRepository : ICommunRepository<TipoEquipamento>
    {
        private readonly AppDbContext _context;

        public TipoEquipamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TipoEquipamento entity, CancellationToken cancellationToken = default)
        {
            await _context.TipoEquipamentos.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(TipoEquipamento entity, CancellationToken cancellationToken = default)
        {
            _context.TipoEquipamentos.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(TipoEquipamento entity, CancellationToken cancellationToken = default)
        {
            _context.TipoEquipamentos.Update(entity);
            await Task.CompletedTask; // para respetar la firma async
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<TipoEquipamento>> GetByAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.TipoEquipamentos
                 .AsNoTracking()
                 .ToListAsync(cancellationToken);
        }

        public async Task<TipoEquipamento?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.TipoEquipamentos
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IdTipoEquipamento == id, cancellationToken);
        }

        public async Task<bool> HasRelatedDataAsync(int id, CancellationToken cancellationToken = default)
        {
            return true;
        }

        public async Task<bool> ExistsDataAsync(int id, string name, CancellationToken cancellationToken = default)
        {
            var existeNombre = await _context.TipoEquipamentos
               .AnyAsync(s => s.IdTipoEquipamento != id && s.Nombre.ToUpper() == name.ToUpper(), cancellationToken);

            return existeNombre;
        }
    }
}
