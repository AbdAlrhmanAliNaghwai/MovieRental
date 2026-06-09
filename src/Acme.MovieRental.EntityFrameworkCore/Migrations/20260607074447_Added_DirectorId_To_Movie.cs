using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.MovieRental.Migrations
{
    /// <inheritdoc />
    public partial class Added_DirectorId_To_Movie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "YearOfRelease",
                table: "AppMovie",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "DirectorId",
                table: "AppMovie",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppMovie_DirectorId",
                table: "AppMovie",
                column: "DirectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppMovie_AppDirector_DirectorId",
                table: "AppMovie",
                column: "DirectorId",
                principalTable: "AppDirector",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppMovie_AppDirector_DirectorId",
                table: "AppMovie");

            migrationBuilder.DropIndex(
                name: "IX_AppMovie_DirectorId",
                table: "AppMovie");

            migrationBuilder.DropColumn(
                name: "DirectorId",
                table: "AppMovie");

            migrationBuilder.AlterColumn<DateTime>(
                name: "YearOfRelease",
                table: "AppMovie",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
