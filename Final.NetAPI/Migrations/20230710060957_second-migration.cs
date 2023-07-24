using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Final.NetAPI.Migrations
{
    public partial class secondmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Address_AddressId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Gender_GenderId1",
                table: "Student");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropIndex(
                name: "IX_Student_AddressId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_GenderId1",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "GenderId1",
                table: "Student");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "Student");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Student",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GenderId",
                table: "Student",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "GenderId1",
                table: "Student",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhysicalAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gen = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_AddressId",
                table: "Student",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_GenderId1",
                table: "Student",
                column: "GenderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Address_AddressId",
                table: "Student",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Gender_GenderId1",
                table: "Student",
                column: "GenderId1",
                principalTable: "Gender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
