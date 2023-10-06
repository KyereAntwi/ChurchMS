using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COPDistrictMS.WebApi.Migrations.COPDistrictMS
{
    /// <inheritdoc />
    public partial class AddedOfferingsBindings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DistrictId",
                table: "MinistryOfferings",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MinistryOfferings_DistrictId",
                table: "MinistryOfferings",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_MinistryOfferings_Districts_DistrictId",
                table: "MinistryOfferings",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinistryOfferings_Districts_DistrictId",
                table: "MinistryOfferings");

            migrationBuilder.DropIndex(
                name: "IX_MinistryOfferings_DistrictId",
                table: "MinistryOfferings");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "MinistryOfferings");
        }
    }
}
