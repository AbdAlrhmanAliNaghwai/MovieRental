using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.MovieRental.Migrations
{
    /// <inheritdoc />
    public partial class Phase1_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppMovie_AppDirector_DirectorId",
                table: "AppMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppMovie",
                table: "AppMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppDirector",
                table: "AppDirector");

            migrationBuilder.RenameTable(
                name: "AppMovie",
                newName: "AppMovies");

            migrationBuilder.RenameTable(
                name: "AppDirector",
                newName: "AppDirectors");

            migrationBuilder.RenameIndex(
                name: "IX_AppMovie_DirectorId",
                table: "AppMovies",
                newName: "IX_AppMovies_DirectorId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "AppMovies",
                type: "TEXT",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppDirectors",
                type: "TEXT",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 128);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppMovies",
                table: "AppMovies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppDirectors",
                table: "AppDirectors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppMovies_AppDirectors_DirectorId",
                table: "AppMovies",
                column: "DirectorId",
                principalTable: "AppDirectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppMovies_AppDirectors_DirectorId",
                table: "AppMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppMovies",
                table: "AppMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppDirectors",
                table: "AppDirectors");

            migrationBuilder.RenameTable(
                name: "AppMovies",
                newName: "AppMovie");

            migrationBuilder.RenameTable(
                name: "AppDirectors",
                newName: "AppDirector");

            migrationBuilder.RenameIndex(
                name: "IX_AppMovies_DirectorId",
                table: "AppMovie",
                newName: "IX_AppMovie_DirectorId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "AppMovie",
                type: "TEXT",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AppDirector",
                type: "TEXT",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppMovie",
                table: "AppMovie",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppDirector",
                table: "AppDirector",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppMovie_AppDirector_DirectorId",
                table: "AppMovie",
                column: "DirectorId",
                principalTable: "AppDirector",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
