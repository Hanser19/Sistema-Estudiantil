using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INSCRIPCION_ESTUDIANTIL.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ASIGNATURAS",
                columns: table => new
                {
                    ID_ASIGNATURAS = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE_ASIGNATURA = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    DESCRIPCION = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CREDITOS = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ASIGNATU__138FA4E4796D614A", x => x.ID_ASIGNATURAS);
                });

            migrationBuilder.CreateTable(
                name: "CURSO",
                columns: table => new
                {
                    ID_CURSO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE_CURSO = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    DESCRIPCION_CURSO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CURSO__9B4AE79840085B5F", x => x.ID_CURSO);
                });

            migrationBuilder.CreateTable(
                name: "INSCRIPCION",
                columns: table => new
                {
                    ID_INSCRIPCION = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FECHA_INSCRIPCION = table.Column<DateTime>(type: "date", nullable: true),
                    ID_ESTUDIANTE = table.Column<string>(type: "varchar(9)", unicode: false, maxLength: 9, nullable: true),
                    ID_CURSO = table.Column<int>(type: "int", nullable: true),
                    ESTADO_INSCRIPCION = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__INSCRIPC__28EB027EB76FB055", x => x.ID_INSCRIPCION);
                    table.ForeignKey(
                        name: "FK_CURSO",
                        column: x => x.ID_CURSO,
                        principalTable: "CURSO",
                        principalColumn: "ID_CURSO");
                });

            migrationBuilder.CreateTable(
                name: "SECCIONES",
                columns: table => new
                {
                    ID_SECCIONES = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE_SECCION = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CUPOS_SECCION = table.Column<int>(type: "int", nullable: true),
                    ID_CURSO = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SECCIONE__4A6892CA12E1BC28", x => x.ID_SECCIONES);
                    table.ForeignKey(
                        name: "FK_CURSO_SECCIONES",
                        column: x => x.ID_CURSO,
                        principalTable: "CURSO",
                        principalColumn: "ID_CURSO");
                });

            migrationBuilder.CreateTable(
                name: "PAGOS",
                columns: table => new
                {
                    ID_PAGO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FECHA_PAGO = table.Column<DateTime>(type: "date", nullable: true),
                    ID_INSCRIPCION = table.Column<int>(type: "int", nullable: true),
                    MONTO = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PAGOS__B68D23DF2BEA63D3", x => x.ID_PAGO);
                    table.ForeignKey(
                        name: "FK_INSCRIPCION",
                        column: x => x.ID_INSCRIPCION,
                        principalTable: "INSCRIPCION",
                        principalColumn: "ID_INSCRIPCION");
                });

            migrationBuilder.CreateTable(
                name: "PROFESORES",
                columns: table => new
                {
                    ID_PROFESOR = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE_PROFESOR = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    CORREO_PROFESOR = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ID_SECCIONES = table.Column<int>(type: "int", nullable: true),
                    ID_ASIGNATURAS = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PROFESOR__5D30231FC2E4B858", x => x.ID_PROFESOR);
                    table.ForeignKey(
                        name: "FK_ASIGNATURA",
                        column: x => x.ID_ASIGNATURAS,
                        principalTable: "ASIGNATURAS",
                        principalColumn: "ID_ASIGNATURAS");
                    table.ForeignKey(
                        name: "FK_SECCIONES",
                        column: x => x.ID_SECCIONES,
                        principalTable: "SECCIONES",
                        principalColumn: "ID_SECCIONES");
                });

            migrationBuilder.CreateIndex(
                name: "IX_INSCRIPCION_ID_CURSO",
                table: "INSCRIPCION",
                column: "ID_CURSO");

            migrationBuilder.CreateIndex(
                name: "IX_PAGOS_ID_INSCRIPCION",
                table: "PAGOS",
                column: "ID_INSCRIPCION");

            migrationBuilder.CreateIndex(
                name: "IX_PROFESORES_ID_ASIGNATURAS",
                table: "PROFESORES",
                column: "ID_ASIGNATURAS");

            migrationBuilder.CreateIndex(
                name: "IX_PROFESORES_ID_SECCIONES",
                table: "PROFESORES",
                column: "ID_SECCIONES");

            migrationBuilder.CreateIndex(
                name: "IX_SECCIONES_ID_CURSO",
                table: "SECCIONES",
                column: "ID_CURSO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PAGOS");

            migrationBuilder.DropTable(
                name: "PROFESORES");

            migrationBuilder.DropTable(
                name: "INSCRIPCION");

            migrationBuilder.DropTable(
                name: "ASIGNATURAS");

            migrationBuilder.DropTable(
                name: "SECCIONES");

            migrationBuilder.DropTable(
                name: "CURSO");
        }
    }
}
