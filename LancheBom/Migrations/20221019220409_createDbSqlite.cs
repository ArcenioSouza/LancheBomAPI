using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LancheBom.Migrations
{
    public partial class createDbSqlite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lanches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Preco = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lanches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LancheId = table.Column<int>(type: "INTEGER", nullable: false),
                    ValorPedido = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Lanches_LancheId",
                        column: x => x.LancheId,
                        principalTable: "Lanches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Adicionais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Preco = table.Column<double>(type: "REAL", nullable: false),
                    PedidoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adicionais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adicionais_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adicionais_PedidoId",
                table: "Adicionais",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_LancheId",
                table: "Pedidos",
                column: "LancheId");

            //Lanches
            migrationBuilder.Sql(@"INSERT INTO `lanches` (`Id`, `Nome`, `Preco`) VALUES ('1', 'X-Burguer', '5.00')");
            migrationBuilder.Sql(@"INSERT INTO `lanches` (`Id`, `Nome`, `Preco`) VALUES ('2', 'X-Salada', '4.50')");
            migrationBuilder.Sql(@"INSERT INTO `lanches` (`Id`, `Nome`, `Preco`) VALUES ('3', 'X-Tudo', '7.00')");

            //Adicionais
            migrationBuilder.Sql(@"INSERT INTO `adicionais` (`Id`, `Nome`, `Preco`) VALUES ('1', 'Batata-Frita', '2.00')");
            migrationBuilder.Sql(@"INSERT INTO `adicionais` (`Id`, `Nome`, `Preco`) VALUES ('2', 'Refrigerante', '2.50')");
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adicionais");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Lanches");
        }
    }
}
