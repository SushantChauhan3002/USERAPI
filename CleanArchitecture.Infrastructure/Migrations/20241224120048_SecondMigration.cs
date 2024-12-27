using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminUserRoles_AdminRoles_RoleId",
                table: "AdminUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AdminUserRoles_AdminUsers_UserId",
                table: "AdminUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AdminUserUtilities_AdminUsers_UserId",
                table: "AdminUserUtilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdminUsers",
                table: "AdminUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdminRoles",
                table: "AdminRoles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AdminUserUtilities");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "AdminRoleID",
                table: "AdminRoles");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "AdminRoles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdminUsers",
                table: "AdminUsers",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdminRoles",
                table: "AdminRoles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminUserRoles_AdminRoles_RoleId",
                table: "AdminUserRoles",
                column: "RoleId",
                principalTable: "AdminRoles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdminUserRoles_AdminUsers_UserId",
                table: "AdminUserRoles",
                column: "UserId",
                principalTable: "AdminUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdminUserUtilities_AdminUsers_UserId",
                table: "AdminUserUtilities",
                column: "UserId",
                principalTable: "AdminUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminUserRoles_AdminRoles_RoleId",
                table: "AdminUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AdminUserRoles_AdminUsers_UserId",
                table: "AdminUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AdminUserUtilities_AdminUsers_UserId",
                table: "AdminUserUtilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdminUsers",
                table: "AdminUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdminRoles",
                table: "AdminRoles");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "AdminUserUtilities",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "AdminUsers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "AdminRoles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "AdminRoleID",
                table: "AdminRoles",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdminUsers",
                table: "AdminUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdminRoles",
                table: "AdminRoles",
                column: "AdminRoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminUserRoles_AdminRoles_RoleId",
                table: "AdminUserRoles",
                column: "RoleId",
                principalTable: "AdminRoles",
                principalColumn: "AdminRoleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdminUserRoles_AdminUsers_UserId",
                table: "AdminUserRoles",
                column: "UserId",
                principalTable: "AdminUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdminUserUtilities_AdminUsers_UserId",
                table: "AdminUserUtilities",
                column: "UserId",
                principalTable: "AdminUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
