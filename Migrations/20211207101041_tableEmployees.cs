using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiWorkControllerServer.Migrations
{
    public partial class tableEmployees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChiefId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employes_Users_ChiefId",
                        column: x => x.ChiefId,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Employes_Users_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employes_ChiefId",
                table: "Employes",
                column: "ChiefId");

            migrationBuilder.CreateIndex(
                name: "IX_Employes_EmployeeId",
                table: "Employes",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employes");
        }
    }
}
