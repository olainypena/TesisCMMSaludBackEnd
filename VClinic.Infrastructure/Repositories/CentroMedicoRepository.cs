using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Application.Abstractions;
using VClinic.Domain.Entities;
using VClinic.Infrastructure.Persistence;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VClinic.Infrastructure.Repositories
{
    public sealed class CentroMedicoRepository : ICentroMedicoRepository
    {
        private readonly AppDbContext _context;

        public CentroMedicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CentroMedico centroMedico, CancellationToken cancellationToken = default)
        {
            await _context.CentroMedico.AddAsync(centroMedico, cancellationToken);
        }

        public async Task<List<CentroMedico>> GetByAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.CentroMedico
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<CentroMedico?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.CentroMedico
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IdCentroMedico == id, cancellationToken);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(CentroMedico centroMedico, CancellationToken cancellationToken = default)
        {
            // Si llega trackeado (GetByIdAsync() + modificar en el servicio), esto ni hace falta.
            // Pero es seguro hacer:
            _context.CentroMedico.Update(centroMedico);
            await Task.CompletedTask; // para respetar la firma async
        }

        public async Task DeleteAsync(CentroMedico centroMedico, CancellationToken cancellationToken = default)
        {
            _context.CentroMedico.Remove(centroMedico);
            await Task.CompletedTask;
        }

        public async Task<bool> HasRelatedDataAsync(int idCentroMedico, CancellationToken cancellationToken = default)
        {
            // Aquí revisas TODAS las tablas que dependan del centro médico.
            // Ejemplos (ajusta a tus entidades reales):

            //var tieneSucursales = await _context.Sucursales
            //    .AnyAsync(s => s.IdCentroMedico == idCentroMedico, cancellationToken);

            //var tieneMedicos = await _context.Medicos
            //    .AnyAsync(m => m.IdCentroMedico == idCentroMedico, cancellationToken);

            //var tieneEmpleados = await _context.Empleados
            //    .AnyAsync(e => e.IdCentroMedico == idCentroMedico, cancellationToken);

            //var tienePacientes = await _context.Pacientes
            //    .AnyAsync(p => p.IdCentroMedico == idCentroMedico, cancellationToken);

            //// Puedes agregar más tablas si lo necesitas.
            //return tieneSucursales || tieneMedicos || tieneEmpleados || tienePacientes;

            return true;
        }

        public async Task<bool> ExistsDataAsync(int id, string nombre, string rnc, CancellationToken cancellationToken = default)
        {
            var existeNombre = await _context.CentroMedico
               .AnyAsync(s => s.IdCentroMedico != id && s.Nombre.ToUpper() == nombre.ToUpper(), cancellationToken);

            var existeRnc = await _context.CentroMedico
               .AnyAsync(s => s.IdCentroMedico != id && s.Rnc.ToUpper() == rnc.ToUpper(), cancellationToken);

            return existeNombre || existeRnc; 
        }
    }
}
