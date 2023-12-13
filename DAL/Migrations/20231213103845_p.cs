using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class p : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    id_autor = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre_autor = table.Column<string>(type: "text", nullable: false),
                    apellidos_autor = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.id_autor);
                });

            migrationBuilder.CreateTable(
                name: "Colecciones",
                columns: table => new
                {
                    id_colecciones = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre_coleccion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colecciones", x => x.id_colecciones);
                });

            migrationBuilder.CreateTable(
                name: "Editoriales",
                columns: table => new
                {
                    id_editoriales = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre_editorial = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editoriales", x => x.id_editoriales);
                });

            migrationBuilder.CreateTable(
                name: "estamos_Prestamos",
                columns: table => new
                {
                    id_estado_prestamo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo_estado_prestamo = table.Column<int>(type: "integer", nullable: false),
                    descripcion_estado_prestamo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estamos_Prestamos", x => x.id_estado_prestamo);
                });

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    id_genero = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre_genero = table.Column<string>(type: "text", nullable: false),
                    descripcion_genero = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.id_genero);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dni_usuario = table.Column<string>(type: "text", nullable: false),
                    nombre_usuario = table.Column<string>(type: "text", nullable: false),
                    apellidos_usuario = table.Column<string>(type: "text", nullable: false),
                    tlf_usuario = table.Column<string>(type: "text", nullable: false),
                    email_usuario = table.Column<string>(type: "text", nullable: false),
                    clave_usuario = table.Column<string>(type: "text", nullable: false),
                    token_recuperacion = table.Column<string>(type: "text", nullable: true),
                    fch_alta_usuario = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "Libros",
                columns: table => new
                {
                    id_libro = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    isbn_libro = table.Column<string>(type: "text", nullable: false),
                    nombre_libro = table.Column<string>(type: "text", nullable: false),
                    edicion_libro = table.Column<string>(type: "text", nullable: false),
                    id_editorial = table.Column<int>(type: "integer", nullable: false),
                    id_genero = table.Column<int>(type: "integer", nullable: false),
                    id_coleccion = table.Column<int>(type: "integer", nullable: false),
                    imagen_libro = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libros", x => x.id_libro);
                    table.ForeignKey(
                        name: "FK_Libros_Colecciones_id_coleccion",
                        column: x => x.id_coleccion,
                        principalTable: "Colecciones",
                        principalColumn: "id_colecciones",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Libros_Editoriales_id_editorial",
                        column: x => x.id_editorial,
                        principalTable: "Editoriales",
                        principalColumn: "id_editoriales",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Libros_Generos_id_genero",
                        column: x => x.id_genero,
                        principalTable: "Generos",
                        principalColumn: "id_genero",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prestamos",
                columns: table => new
                {
                    id_prestamos = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_libro = table.Column<int>(type: "integer", nullable: false),
                    id_usuario = table.Column<int>(type: "integer", nullable: false),
                    fch_inicio_prestamo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fch_fin_prestamo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fch_entrega_prestamo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    id_estado_prestamo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestamos", x => x.id_prestamos);
                    table.ForeignKey(
                        name: "FK_Prestamos_Usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prestamos_estamos_Prestamos_id_estado_prestamo",
                        column: x => x.id_estado_prestamo,
                        principalTable: "estamos_Prestamos",
                        principalColumn: "id_estado_prestamo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rel_Autores_Libros",
                columns: table => new
                {
                    Autoresid_autor = table.Column<int>(type: "integer", nullable: false),
                    Librosid_libro = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rel_Autores_Libros", x => new { x.Autoresid_autor, x.Librosid_libro });
                    table.ForeignKey(
                        name: "FK_Rel_Autores_Libros_Autores_Autoresid_autor",
                        column: x => x.Autoresid_autor,
                        principalTable: "Autores",
                        principalColumn: "id_autor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rel_Autores_Libros_Libros_Librosid_libro",
                        column: x => x.Librosid_libro,
                        principalTable: "Libros",
                        principalColumn: "id_libro",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rel_Libros_Prestamos",
                columns: table => new
                {
                    Prestamosid_prestamos = table.Column<int>(type: "integer", nullable: false),
                    collectionLibroid_libro = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rel_Libros_Prestamos", x => new { x.Prestamosid_prestamos, x.collectionLibroid_libro });
                    table.ForeignKey(
                        name: "FK_Rel_Libros_Prestamos_Libros_collectionLibroid_libro",
                        column: x => x.collectionLibroid_libro,
                        principalTable: "Libros",
                        principalColumn: "id_libro",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rel_Libros_Prestamos_Prestamos_Prestamosid_prestamos",
                        column: x => x.Prestamosid_prestamos,
                        principalTable: "Prestamos",
                        principalColumn: "id_prestamos",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "id_usuario", "apellidos_usuario", "clave_usuario", "dni_usuario", "email_usuario", "fch_alta_usuario", "nombre_usuario", "tlf_usuario", "token_recuperacion" },
                values: new object[] { 1, "ADMIN", "ac9689e2272427085e35b9d3e3e8bed88cb3434828b43b86fc0596cad4c6e270", "1", "admin@gmail.com", new DateTime(2023, 12, 13, 10, 38, 45, 315, DateTimeKind.Utc).AddTicks(9982), "ADMIN", "1", null });

            migrationBuilder.CreateIndex(
                name: "IX_Libros_id_coleccion",
                table: "Libros",
                column: "id_coleccion");

            migrationBuilder.CreateIndex(
                name: "IX_Libros_id_editorial",
                table: "Libros",
                column: "id_editorial");

            migrationBuilder.CreateIndex(
                name: "IX_Libros_id_genero",
                table: "Libros",
                column: "id_genero");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_id_estado_prestamo",
                table: "Prestamos",
                column: "id_estado_prestamo");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_id_usuario",
                table: "Prestamos",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Rel_Autores_Libros_Librosid_libro",
                table: "Rel_Autores_Libros",
                column: "Librosid_libro");

            migrationBuilder.CreateIndex(
                name: "IX_Rel_Libros_Prestamos_collectionLibroid_libro",
                table: "Rel_Libros_Prestamos",
                column: "collectionLibroid_libro");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rel_Autores_Libros");

            migrationBuilder.DropTable(
                name: "Rel_Libros_Prestamos");

            migrationBuilder.DropTable(
                name: "Autores");

            migrationBuilder.DropTable(
                name: "Libros");

            migrationBuilder.DropTable(
                name: "Prestamos");

            migrationBuilder.DropTable(
                name: "Colecciones");

            migrationBuilder.DropTable(
                name: "Editoriales");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "estamos_Prestamos");
        }
    }
}
