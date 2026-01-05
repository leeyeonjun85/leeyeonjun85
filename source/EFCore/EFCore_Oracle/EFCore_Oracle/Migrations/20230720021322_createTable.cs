using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore_Oracle.Migrations
{
    /// <inheritdoc />
    public partial class createTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SCHOOL",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    name = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCHOOL", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ROOM",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    name = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    school_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROOM", x => x.id);
                    table.ForeignKey(
                        name: "FK_ROOM_SCHOOL_school_id",
                        column: x => x.school_id,
                        principalTable: "SCHOOL",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STUDENT",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    name = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    birthday = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    room_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDENT", x => x.id);
                    table.ForeignKey(
                        name: "FK_STUDENT_ROOM_room_id",
                        column: x => x.room_id,
                        principalTable: "ROOM",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ROOM_school_id",
                table: "ROOM",
                column: "school_id");

            migrationBuilder.CreateIndex(
                name: "IX_STUDENT_room_id",
                table: "STUDENT",
                column: "room_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STUDENT");

            migrationBuilder.DropTable(
                name: "ROOM");

            migrationBuilder.DropTable(
                name: "SCHOOL");
        }
    }
}
