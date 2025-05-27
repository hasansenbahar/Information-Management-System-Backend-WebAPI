using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebService.API.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExitTime",
                table: "Allocation",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "123458as-a24d-3456-a6c6-123fhj48cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5560e685-48e8-4068-978d-ae49920df8df", "AQAAAAIAAYagAAAAEF6TzBOLmvLbrabacwIQ6F+h/T8mW4NEOwwQ7jy5DcVIzExoNW9fa3EKTGAza6vRxA==", "03abae72-145b-45fd-8945-0de10f26bc3f" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f7b544c8-8ede-4288-a21f-5920ca5d247b", "AQAAAAIAAYagAAAAENllCafbOwsDpci5hqEasBeA8oyHyJ4OD0CzXyWebAu6l889kuF+4N7iycGsVGUMSQ==", "7a767c31-18a2-4fcc-b9da-c0a05a5b16c5" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "8e4458as-a24d-3456-a6c6-944fhj48cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d20f56e0-44dc-4a32-be7c-240bc4eca4f8", "AQAAAAIAAYagAAAAEMbpxZCtx0wyFAZKbi1C6IkG3pO+QPS9gE/LLncwp+D7rPMAUSs0qcKwzCDz4riRsA==", "dd739f3f-c60d-46fa-ad85-107e2efe3a19" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExitTime",
                table: "Allocation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "123458as-a24d-3456-a6c6-123fhj48cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "33faa1b2-04ae-4cfa-a155-d44623cb97cd", "AQAAAAIAAYagAAAAEIk36oWGww6OWweWPk4j6Uup/hvhyx46j1kUKhw7S3/zOOPe49RnTh/r150BD6H5Mw==", "e2c977dc-11fe-48b4-b98c-a70ed5761444" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eff4c6e7-2fa3-4705-863e-49c3fae209a7", "AQAAAAIAAYagAAAAEAqEuWf51/wlSBFnNQ+TpYLRoC/OpnkGf+slTReouZHXiEUjmb0n5vrjPZ5mro2vAA==", "6f312c06-7b73-4d75-8340-180347bae8b4" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "8e4458as-a24d-3456-a6c6-944fhj48cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b3806fff-dd50-43c3-bc91-6ef91f9c3e59", "AQAAAAIAAYagAAAAEO0VqJqfbjKtCMt0VwOJgHHUbLTQQ/Uw4O6VGB2Cyw1TCvOFye2HUgFDWhXlldfD9g==", "d91194b1-d074-44a8-ac04-7f0d65b5889a" });
        }
    }
}
