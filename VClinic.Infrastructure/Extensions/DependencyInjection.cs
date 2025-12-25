using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VClinic.Application.Abstractions;
using VClinic.Application.Services;
using VClinic.Domain.Entities;
using VClinic.Domain.Entities.Catalogos;
using VClinic.Infrastructure.Persistence.Repositories;
using VClinic.Infrastructure.Repositories;

namespace VClinic.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Repositorios            
            services.AddScoped<ICommunRepository<TipoIdentificacion>, TipoIdentificacionRepository>();
            services.AddScoped<ICommunRepository<TipoEquipamento>, TipoEquipamentoRepository>();
            services.AddScoped<ICommunRepository<GrupoSanguinio>, GrupoSanguinioRepository>();
            services.AddScoped<ICommunRepository<Equipamento>, EquipamentoRepository>();
            services.AddScoped<ICommunRepository<Ocupacion>, OcupacionRepository>();
            services.AddScoped<ICommunRepository<Posiciones>, PosicionRepository>();
            services.AddScoped<ICommunRepository<Profesion>, ProfesionRepository>();
            services.AddScoped<ICentroMedicoRepository, CentroMedicoRepository>();
            services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
            services.AddScoped<IEstadoCivilRepository, EstadoCivilRepository>();
            services.AddScoped<ICommunRepository<Genero>, GeneroRepository>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
            services.AddScoped<IPersonaRepository, PersonaRepository>();
            services.AddScoped<IMedicoRepository, MedicoRepository>();
            services.AddScoped<IPersonaService, PersonaService>();

            


            // Servicios
            services.AddScoped<CentroMedicoService>();
            services.AddScoped<PacienteService>();
            services.AddScoped<EmpleadoService>();
            services.AddScoped<PersonaService>();
            services.AddScoped<MedicoService>();
            services.AddScoped<GrupoSanguinioService>();
            services.AddScoped<GeneroService>();
            services.AddScoped<EstadoCivilService>();
            services.AddScoped<DepartamentoService>();
            services.AddScoped<ProfesionService>();
            services.AddScoped<PosicionService>();
            services.AddScoped<OcupacionService>();
            services.AddScoped<EquipamentoService>();
            services.AddScoped<TipoEquipamentoService>();
            services.AddScoped<TipoIdentificacionService>();


            return services;

        }
    }
}
