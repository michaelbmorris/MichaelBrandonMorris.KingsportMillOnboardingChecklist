using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MichaelBrandonMorris.KingsportMillOnboardingChecklist.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnboarding",
                table: "ActionItemAssignments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ChecklistId",
                table: "ActionItems",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Checklists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checklists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checklists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionItems_ChecklistId",
                table: "ActionItems",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_UserId",
                table: "Checklists",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ActionItems_Checklists_ChecklistId",
                table: "ActionItems",
                column: "ChecklistId",
                principalTable: "Checklists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionItems_Checklists_ChecklistId",
                table: "ActionItems");

            migrationBuilder.DropTable(
                name: "Checklists");

            migrationBuilder.DropIndex(
                name: "IX_ActionItems_ChecklistId",
                table: "ActionItems");

            migrationBuilder.DropColumn(
                name: "IsOnboarding",
                table: "ActionItemAssignments");

            migrationBuilder.DropColumn(
                name: "ChecklistId",
                table: "ActionItems");
        }
    }
}
