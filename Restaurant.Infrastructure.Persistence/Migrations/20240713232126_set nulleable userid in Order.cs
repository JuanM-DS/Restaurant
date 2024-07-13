using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class setnulleableuseridinOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 7, 13, 23, 21, 25, 523, DateTimeKind.Utc).AddTicks(3996));

            migrationBuilder.UpdateData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2024, 7, 13, 23, 21, 25, 523, DateTimeKind.Utc).AddTicks(4007));

            migrationBuilder.UpdateData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedTime",
                value: new DateTime(2024, 7, 13, 23, 21, 25, 523, DateTimeKind.Utc).AddTicks(4008));

            migrationBuilder.UpdateData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedTime",
                value: new DateTime(2024, 7, 13, 23, 21, 25, 523, DateTimeKind.Utc).AddTicks(4010));

            migrationBuilder.UpdateData(
                table: "OrderStates",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 7, 13, 23, 21, 25, 525, DateTimeKind.Utc).AddTicks(4085));

            migrationBuilder.UpdateData(
                table: "OrderStates",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2024, 7, 13, 23, 21, 25, 525, DateTimeKind.Utc).AddTicks(4089));

            migrationBuilder.UpdateData(
                table: "TableStates",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 7, 13, 23, 21, 25, 526, DateTimeKind.Utc).AddTicks(2563));

            migrationBuilder.UpdateData(
                table: "TableStates",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2024, 7, 13, 23, 21, 25, 526, DateTimeKind.Utc).AddTicks(2567));

            migrationBuilder.UpdateData(
                table: "TableStates",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedTime",
                value: new DateTime(2024, 7, 13, 23, 21, 25, 526, DateTimeKind.Utc).AddTicks(2568));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 7, 11, 19, 34, 42, 592, DateTimeKind.Utc).AddTicks(4338));

            migrationBuilder.UpdateData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2024, 7, 11, 19, 34, 42, 592, DateTimeKind.Utc).AddTicks(4341));

            migrationBuilder.UpdateData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedTime",
                value: new DateTime(2024, 7, 11, 19, 34, 42, 592, DateTimeKind.Utc).AddTicks(4342));

            migrationBuilder.UpdateData(
                table: "DishCategories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedTime",
                value: new DateTime(2024, 7, 11, 19, 34, 42, 592, DateTimeKind.Utc).AddTicks(4343));

            migrationBuilder.UpdateData(
                table: "OrderStates",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 7, 11, 19, 34, 42, 593, DateTimeKind.Utc).AddTicks(6422));

            migrationBuilder.UpdateData(
                table: "OrderStates",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2024, 7, 11, 19, 34, 42, 593, DateTimeKind.Utc).AddTicks(6425));

            migrationBuilder.UpdateData(
                table: "TableStates",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2024, 7, 11, 19, 34, 42, 594, DateTimeKind.Utc).AddTicks(5826));

            migrationBuilder.UpdateData(
                table: "TableStates",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedTime",
                value: new DateTime(2024, 7, 11, 19, 34, 42, 594, DateTimeKind.Utc).AddTicks(5834));

            migrationBuilder.UpdateData(
                table: "TableStates",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedTime",
                value: new DateTime(2024, 7, 11, 19, 34, 42, 594, DateTimeKind.Utc).AddTicks(5835));
        }
    }
}
