using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Anovase.Sunnyside.Data.Migrations
{
    /// <inheritdoc />
    public partial class PrimitiveType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TaskTypes",
                columns: new[] { "Id", "DerivativeId", "Description", "IsCounted", "Name" },
                values: new object[,]
                {
                    { new Guid("6f90d276-4cdc-4790-a5ba-34a58cca88c2"), null, "", false, "Void" },
                    { new Guid("ccb39899-3229-4d42-b28b-1f655adbb84c"), null, "", true, "Chore" },
                    { new Guid("e567611b-ba13-49e9-b668-02c964a90391"), null, "", true, "Work" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("6f90d276-4cdc-4790-a5ba-34a58cca88c2"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("ccb39899-3229-4d42-b28b-1f655adbb84c"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("e567611b-ba13-49e9-b668-02c964a90391"));
        }
    }
}
