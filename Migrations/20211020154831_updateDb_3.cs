using Microsoft.EntityFrameworkCore.Migrations;

namespace hrmProject.Migrations
{
    public partial class updateDb_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EducationId",
                table: "Users",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    EducationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EducationName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Specialization = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.EducationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_EducationId",
                table: "Users",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Educations_EducationId",
                table: "Users",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "EducationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Educations_EducationId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Users_EducationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "Users");
        }
    }
}
