using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrpcService.Data.Migrations
{
    public partial class M1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Siswas",
                columns: table => new
                {
                    SiswaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NamaDepan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NamaBel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sekolah = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siswas", x => x.SiswaId);
                });

            migrationBuilder.InsertData(
                table: "Siswas",
                columns: new[] { "SiswaId", "NamaBel", "NamaDepan", "Sekolah" },
                values: new object[,]
                {
                    { 11, "Fox", "Ann", "Nursing" },
                    { 22, "Doe", "Sam", "Mining" },
                    { 33, "Cox", "Sue", "Business" },
                    { 44, "Lee", "Tim", "Computing" },
                    { 55, "Ray", "Jan", "Computing" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Siswas");
        }
    }
}
