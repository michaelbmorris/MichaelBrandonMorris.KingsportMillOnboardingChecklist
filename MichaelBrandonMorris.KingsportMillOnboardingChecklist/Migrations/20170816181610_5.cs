using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MichaelBrandonMorris.KingsportMillOnboardingChecklist.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionItemAssignments_AspNetUsers_AssigneeId",
                table: "ActionItemAssignments");

            migrationBuilder.RenameColumn(
                name: "AssigneeId",
                table: "ActionItemAssignments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ActionItemAssignments_AssigneeId",
                table: "ActionItemAssignments",
                newName: "IX_ActionItemAssignments_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionItemAssignments_AspNetUsers_UserId",
                table: "ActionItemAssignments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionItemAssignments_AspNetUsers_UserId",
                table: "ActionItemAssignments");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ActionItemAssignments",
                newName: "AssigneeId");

            migrationBuilder.RenameIndex(
                name: "IX_ActionItemAssignments_UserId",
                table: "ActionItemAssignments",
                newName: "IX_ActionItemAssignments_AssigneeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionItemAssignments_AspNetUsers_AssigneeId",
                table: "ActionItemAssignments",
                column: "AssigneeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
