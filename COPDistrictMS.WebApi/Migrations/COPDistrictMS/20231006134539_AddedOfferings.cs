using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COPDistrictMS.WebApi.Migrations.COPDistrictMS
{
    /// <inheritdoc />
    public partial class AddedOfferings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssemblyOfferings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssemblyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OfferingType = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssemblyOfferings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssemblyOfferings_Assemblies_AssemblyId",
                        column: x => x.AssemblyId,
                        principalTable: "Assemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssemblyPresiding",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssemblyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MemberId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssemblyPresiding", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssemblyPresiding_Assemblies_AssemblyId",
                        column: x => x.AssemblyId,
                        principalTable: "Assemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssemblyPresiding_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    AssemblyId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.Username);
                    table.ForeignKey(
                        name: "FK_Manager_Assemblies_AssemblyId",
                        column: x => x.AssemblyId,
                        principalTable: "Assemblies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MinistryOfferings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Ministry = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinistryOfferings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyOfferings_AssemblyId",
                table: "AssemblyOfferings",
                column: "AssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyPresiding_AssemblyId",
                table: "AssemblyPresiding",
                column: "AssemblyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyPresiding_MemberId",
                table: "AssemblyPresiding",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Manager_AssemblyId",
                table: "Manager",
                column: "AssemblyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssemblyOfferings");

            migrationBuilder.DropTable(
                name: "AssemblyPresiding");

            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "MinistryOfferings");
        }
    }
}
