using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamBilet2.Migrations
{
    public partial class CreatedTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descriptiom = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TwitterUrl = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FbUrl = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    InstaUrl = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LnUrl = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
