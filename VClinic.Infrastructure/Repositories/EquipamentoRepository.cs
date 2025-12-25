using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Application.Abstractions;
using VClinic.Domain.Entities;
using VClinic.Domain.Entities.Catalogos;
using VClinic.Infrastructure.Persistence;

namespace VClinic.Infrastructure.Repositories
{
    public class EquipamentoRepository : ICommunRepository<Equipamento>
    {
        private readonly AppDbContext _context;

        public EquipamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Equipamento entity, CancellationToken cancellationToken = default)
        {
            await _context.Equipamentos.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(Equipamento entity, CancellationToken cancellationToken = default)
        {
            _context.Equipamentos.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Equipamento entity, CancellationToken cancellationToken = default)
        {
            _context.Equipamentos.Update(entity);
            await Task.CompletedTask; // para respetar la firma async
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Equipamento>> GetByAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Equipamentos
                 .AsNoTracking()
                 .ToListAsync(cancellationToken);
        }

        public async Task<Equipamento?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Equipamentos
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IdEquipamento == id, cancellationToken);
        }

        public async Task<bool> HasRelatedDataAsync(int id, CancellationToken cancellationToken = default)
        {
            return true;
        }

        public async Task<bool> ExistsDataAsync(int id, string name, CancellationToken cancellationToken = default)
        {
            var existeNombre = await _context.Equipamentos
               .AnyAsync(s => s.IdEquipamento != id && s.Nombre.ToUpper() == name.ToUpper(), cancellationToken);

            return existeNombre;
        }
    }
}
