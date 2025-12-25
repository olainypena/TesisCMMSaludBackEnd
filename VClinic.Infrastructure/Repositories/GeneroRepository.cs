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
    public class GeneroRepository : ICommunRepository<Genero>
    {
        private readonly AppDbContext _context;

        public GeneroRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Genero entity, CancellationToken cancellationToken = default)
        {
            await _context.Generos.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(Genero entity, CancellationToken cancellationToken = default)
        {
            _context.Generos.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Genero entity, CancellationToken cancellationToken = default)
        {
            _context.Generos.Update(entity);
            await Task.CompletedTask; // para respetar la firma async
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Genero>> GetByAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Generos
                 .AsNoTracking()
                 .ToListAsync(cancellationToken);
        }

        public async Task<Genero?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Generos
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IdGenero == id, cancellationToken);
        }

        public async Task<bool> HasRelatedDataAsync(int id, CancellationToken cancellationToken = default)
        {
            return true;
        }

        public async Task<bool> ExistsDataAsync(int id, string name, CancellationToken cancellationToken = default)
        {
            var existeNombre = await _context.Generos
               .AnyAsync(s => s.IdGenero != id && s.Nombre.ToUpper() == name.ToUpper(), cancellationToken);

            return existeNombre;
        }

    }
}
