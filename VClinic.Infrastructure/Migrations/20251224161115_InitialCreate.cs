using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VClinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CentrosMedicos",
                columns: table => new
                {
                    IdCentroMedico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EstaActivo = table.Column<bool>(type: "bit", nullable: false),
                    FechaFundacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rnc = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentrosMedicos", x => x.IdCentroMedico);
                });

            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    IdDepartamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EstaActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.IdDepartamento);
                });

            migrationBuilder.CreateTable(
                name: "Equipamento",
                columns: table => new
                {
                    IdEquipamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdTipoEquipo = table.Column<int>(type: "int", nullable: false),
                    EstaActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamento", x => x.IdEquipamento);
                });

            migrationBuilder.CreateTable(
                name: "EstadoCivil",
                columns: table => new
                {
                    IdEstadoCivil = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EstaActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoCivil", x => x.IdEstadoCivil);
                });

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    IdGenero = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EstaActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.IdGenero);
                });

            migrationBuilder.CreateTable(
                name: "GrupoSangre",
                columns: table => new
                {
                    IdGrupoSangre = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoSangre = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    GrupoABO = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    FactorRH = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EstaActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoSangre", x => x.IdGrupoSangre);
                });

            migrationBuilder.CreateTable(
                name: "Ocupacions",
                columns: table => new
                {
                    IdOcupacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    EstaActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocupacions", x => x.IdOcupacion);
                });

            migrationBuilder.CreateTable(
                name: "Posiciones",
                columns: table => new
                {
                    IdPosicion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    EstaActivo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posiciones", x => x.IdPosicion);
                });

            migrationBuilder.CreateTable(
                name: "Profesion",
                columns: table => new
                {
                    IdProfesion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EstaActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesion", x => x.IdProfesion);
                });

            migrationBuilder.CreateTable(
                name: "TipoEquipamento",
                columns: table => new
                {
                    IdTipoEquipamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EstaActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEquipamento", x => x.IdTipoEquipamento);
                });

            migrationBuilder.CreateTable(
                name: "TipoIdentificacion",
                columns: table => new
                {
                    IdTipoIdentificacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EstaActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoIdentificacion", x => x.IdTipoIdentificacion);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    IdPersona = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdTipoIdentificacion = table.Column<int>(type: "int", nullable: false),
                    NumeroIdentificacion = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IdEstadoCivil = table.Column<int>(type: "int", nullable: false),
                    IdGenero = table.Column<int>(type: "int", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.IdPersona);
                    table.ForeignKey(
                        name: "FK_Personas_EstadoCivil_IdEstadoCivil",
                        column: x => x.IdEstadoCivil,
                        principalTable: "EstadoCivil",
                        principalColumn: "IdEstadoCivil",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personas_Generos_IdGenero",
                        column: x => x.IdGenero,    
                        principalTable: "Generos",
                        principalColumn: "IdGenero",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Personas_TipoIdentificacion_IdTipoIdentificacion",
                        column: x => x.IdTipoIdentificacion,
                        principalTable: "TipoIdentificacion",
                        principalColumn: "IdTipoIdentificacion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    IdEmpleado = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersona = table.Column<long>(type: "bigint", nullable: false),
                    CodigoEmpleado = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EstaActivo = table.Column<bool>(type: "bit", nullable: false),
                    IdProfesion = table.Column<int>(type: "int", nullable: false),
                    IdDepartamento = table.Column<int>(type: "int", nullable: false),
                    IdPosicion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.IdEmpleado);
                    table.ForeignKey(
                        name: "FK_Empleados_Departamentos_IdDepartamento",
                        column: x => x.IdDepartamento,
                        principalTable: "Departamentos",
                        principalColumn: "IdDepartamento",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Empleados_Personas_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Personas",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Empleados_Posiciones_IdPosicion",
                        column: x => x.IdPosicion,
                        principalTable: "Posiciones",
                        principalColumn: "IdPosicion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Empleados_Profesion_IdProfesion",
                        column: x => x.IdProfesion,
                        principalTable: "Profesion",
                        principalColumn: "IdProfesion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    IdMedico = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersona = table.Column<long>(type: "bigint", nullable: false),
                    NumeroColegiado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EstaActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.IdMedico);
                    table.ForeignKey(
                        name: "FK_Medicos_Personas_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Personas",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    IdPaciente = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersona = table.Column<long>(type: "bigint", nullable: false),
                    IdGrupoSangre = table.Column<int>(type: "int", nullable: false),
                    GrupoSanguinioIdGrupoSangre = table.Column<int>(type: "int", nullable: false),
                    IdOcupacion = table.Column<int>(type: "int", nullable: false),
                    OcupacionIdOcupacion = table.Column<int>(type: "int", nullable: false),
                    IdHistorial = table.Column<int>(type: "int", nullable: false),
                    EstaActivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.IdPaciente);
                    table.ForeignKey(
                        name: "FK_Pacientes_GrupoSangre_GrupoSanguinioIdGrupoSangre",
                        column: x => x.GrupoSanguinioIdGrupoSangre,
                        principalTable: "GrupoSangre",
                        principalColumn: "IdGrupoSangre",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pacientes_Ocupacions_OcupacionIdOcupacion",
                        column: x => x.OcupacionIdOcupacion,
                        principalTable: "Ocupacions",
                        principalColumn: "IdOcupacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pacientes_Personas_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Personas",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_CodigoEmpleado",
                table: "Empleados",
                column: "CodigoEmpleado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_IdDepartamento",
                table: "Empleados",
                column: "IdDepartamento");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_IdPersona",
                table: "Empleados",
                column: "IdPersona",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_IdPosicion",
                table: "Empleados",
                column: "IdPosicion");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_IdProfesion",
                table: "Empleados",
                column: "IdProfesion");

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_IdPersona",
                table: "Medicos",
                column: "IdPersona",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_NumeroColegiado",
                table: "Medicos",
                column: "NumeroColegiado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_GrupoSanguinioIdGrupoSangre",
                table: "Pacientes",
                column: "GrupoSanguinioIdGrupoSangre");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_IdHistorial",
                table: "Pacientes",
                column: "IdHistorial",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_IdPersona",
                table: "Pacientes",
                column: "IdPersona",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_OcupacionIdOcupacion",
                table: "Pacientes",
                column: "OcupacionIdOcupacion");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_IdEstadoCivil",
                table: "Personas",
                column: "IdEstadoCivil");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_IdGenero",
                table: "Personas",
                column: "IdGenero");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_IdTipoIdentificacion_NumeroIdentificacion",
                table: "Personas",
                columns: new[] { "IdTipoIdentificacion", "NumeroIdentificacion" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CentrosMedicos");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Equipamento");

            migrationBuilder.DropTable(
                name: "Medicos");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "TipoEquipamento");

            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropTable(
                name: "Posiciones");

            migrationBuilder.DropTable(
                name: "Profesion");

            migrationBuilder.DropTable(
                name: "GrupoSangre");

            migrationBuilder.DropTable(
                name: "Ocupacions");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "EstadoCivil");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "TipoIdentificacion");
        }
    }
}
