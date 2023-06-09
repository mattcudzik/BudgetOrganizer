using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BudgetOrganizer.Migrations
{
    /// <inheritdoc />
    public partial class deafultValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Categories",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Color", "Name" },
                values: new object[,]
                {
                    { new Guid("05cbb654-bd1b-4cec-a3d7-297d4510a05d"), "#177EE6", "Inne" },
                    { new Guid("161aefc1-d19e-4074-a820-4446faa7118b"), "#E617E6", "Sprzedaż" },
                    { new Guid("3c5a97de-1bd0-4355-8d8e-065339e5990f"), "#17E67E", "Edukacja" },
                    { new Guid("6358e213-e476-454c-be68-5d22553c81bb"), "#1717E6", "Kieszonkowe" },
                    { new Guid("78767c75-ad4e-45e0-9f90-ff607b6e61cf"), "#E67E17", "Rachunki" },
                    { new Guid("7ecbdcf9-b803-4dcc-9d5e-b5cda8f9c9d5"), "#E6177E", "Wynagrodzenie" },
                    { new Guid("8cacaea7-277b-47fd-bf94-2182ca97c98e"), "#E6E617", "Transport" },
                    { new Guid("92b54e06-46d9-4bc0-b10c-8d7b525acff8"), "#7EE617", "Rozrywka i wypoczynek" },
                    { new Guid("9a98d560-4526-4151-9c79-cb30c4425825"), "#7E17E6", "Emerytura" },
                    { new Guid("b2e24e36-7d45-416a-9084-8b1b551aac36"), "#17E6E6", "Dzieci" },
                    { new Guid("e2b9db46-9f7e-4f3d-97bc-4bbb0d22501f"), "#17E617", "Zdrowie" },
                    { new Guid("fe0c4dd2-72a0-47cc-b5f6-abb287a203d6"), "#E61717", "Zakupy" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0499792b-be19-4f0b-9fd7-8fafcf6daf41"), "child" },
                    { new Guid("334bfc88-5485-4902-acf1-e0142a161a2e"), "adult" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("05cbb654-bd1b-4cec-a3d7-297d4510a05d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("161aefc1-d19e-4074-a820-4446faa7118b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3c5a97de-1bd0-4355-8d8e-065339e5990f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6358e213-e476-454c-be68-5d22553c81bb"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("78767c75-ad4e-45e0-9f90-ff607b6e61cf"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7ecbdcf9-b803-4dcc-9d5e-b5cda8f9c9d5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8cacaea7-277b-47fd-bf94-2182ca97c98e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("92b54e06-46d9-4bc0-b10c-8d7b525acff8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9a98d560-4526-4151-9c79-cb30c4425825"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b2e24e36-7d45-416a-9084-8b1b551aac36"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e2b9db46-9f7e-4f3d-97bc-4bbb0d22501f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("fe0c4dd2-72a0-47cc-b5f6-abb287a203d6"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0499792b-be19-4f0b-9fd7-8fafcf6daf41"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("334bfc88-5485-4902-acf1-e0142a161a2e"));

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Categories",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(7)",
                oldMaxLength: 7);
        }
    }
}
