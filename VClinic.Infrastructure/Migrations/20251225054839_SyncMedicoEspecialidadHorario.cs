using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VClinic.Infrastructure.Migrations
{
    public partial class SyncMedicoEspecialidadHorario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Las columnas Especialidad y Horario
            // ya existen en la base de datos (creadas manualmente).
            // Esta migración solo sincroniza EF Core.
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // No rollback
        }
    }
}
