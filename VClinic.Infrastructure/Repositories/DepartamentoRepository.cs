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
    public class DepartamentoRepository: IDepartamentoRepository
    {
        private readonly AppDbContext _context;

        public DepartamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Departamento departamento, CancellationToken cancellationToken = default)
        {
            await _context.Departamentos.AddAsync(departamento, cancellationToken);
        }

        public async Task<List<Departamento>> GetByAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Departamentos
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Departamento?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Departamentos
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IdDepartamento == id, cancellationToken);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Departamento departamento, CancellationToken cancellationToken = default)
        {            
            _context.Departamentos.Update(departamento);
            await Task.CompletedTask; // para respetar la firma async
        }

        public async Task DeleteAsync(Departamento departamento, CancellationToken cancellationToken = default)
        {
            _context.Departamentos.Remove(departamento);
            await Task.CompletedTask;
        }

        public async Task<bool> HasRelatedDataAsync(int departamento, CancellationToken cancellationToken = default)
        {
            return true;
        }

        public async Task<bool> ExistsDataAsync(int id, string nombre, CancellationToken cancellationToken = default)
        {
            var existeNombre = await _context.Departamentos
               .AnyAsync(s => s.IdDepartamento != id && s.Nombre.ToUpper() == nombre.ToUpper(), cancellationToken);
     
            return existeNombre;
        }
    }
}
