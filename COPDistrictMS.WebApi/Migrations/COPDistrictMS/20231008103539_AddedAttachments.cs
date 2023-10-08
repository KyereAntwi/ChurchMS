using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COPDistrictMS.WebApi.Migrations.COPDistrictMS
{
    /// <inheritdoc />
    public partial class AddedAttachments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OfficersMeetingId",
                table: "Members",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OfferingAttachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OfferingId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssemblyOfferingId = table.Column<Guid>(type: "TEXT", nullable: true),
                    MinistryOfferingId = table.Column<Guid>(type: "TEXT", nullable: true),
                    FileType = table.Column<string>(type: "TEXT", nullable: true),
                    FileUri = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferingAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferingAttachment_AssemblyOfferings_AssemblyOfferingId",
                        column: x => x.AssemblyOfferingId,
                        principalTable: "AssemblyOfferings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OfferingAttachment_MinistryOfferings_MinistryOfferingId",
                        column: x => x.MinistryOfferingId,
                        principalTable: "MinistryOfferings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OfficersMeetings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DistrictId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PastorInCharge = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficersMeetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfficersMeetings_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfficersMeetingAttachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MeetingId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OfficersMeetingId = table.Column<Guid>(type: "TEXT", nullable: true),
                    FileType = table.Column<string>(type: "TEXT", nullable: true),
                    FileUri = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficersMeetingAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfficersMeetingAttachment_OfficersMeetings_OfficersMeetingId",
                        column: x => x.OfficersMeetingId,
                        principalTable: "OfficersMeetings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_OfficersMeetingId",
                table: "Members",
                column: "OfficersMeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferingAttachment_AssemblyOfferingId",
                table: "OfferingAttachment",
                column: "AssemblyOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferingAttachment_MinistryOfferingId",
                table: "OfferingAttachment",
                column: "MinistryOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficersMeetingAttachment_OfficersMeetingId",
                table: "OfficersMeetingAttachment",
                column: "OfficersMeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficersMeetings_DistrictId",
                table: "OfficersMeetings",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_OfficersMeetings_OfficersMeetingId",
                table: "Members",
                column: "OfficersMeetingId",
                principalTable: "OfficersMeetings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_OfficersMeetings_OfficersMeetingId",
                table: "Members");

            migrationBuilder.DropTable(
                name: "OfferingAttachment");

            migrationBuilder.DropTable(
                name: "OfficersMeetingAttachment");

            migrationBuilder.DropTable(
                name: "OfficersMeetings");

            migrationBuilder.DropIndex(
                name: "IX_Members_OfficersMeetingId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "OfficersMeetingId",
                table: "Members");
        }
    }
}
