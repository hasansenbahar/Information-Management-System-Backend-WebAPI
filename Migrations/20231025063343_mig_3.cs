using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebService.API.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Allocation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "123458as-a24d-3456-a6c6-123fhj48cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "971d1c37-a010-4e6e-98cc-d413636ef485", "AQAAAAIAAYagAAAAEJrPE2RBX34LJbK+M8t7ud3HSmjgcrlykSqW0uP+McWfpW0AtDKfrG5tejlqN0aWng==", "1972cd6c-3f13-4049-93ea-ecdd507f7c19" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bd142f4a-80c9-4a18-b3c1-4ffc264fa5b2", "AQAAAAIAAYagAAAAEAjjaszeYI/I3NQSONxyGHX5IYACWxL5blEcseuhQF7omVyfkQkA6A4nVtVzvCv0pA==", "551df57e-5858-41fd-b5cc-011ae988a32b" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: "8e4458as-a24d-3456-a6c6-944fhj48cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea70b961-1ecc-4829-853f-cd12f80014de", "AQAAAAIAAYagAAAAEAKNGLSiBZov3RILQ4yEYIbz53BYJ4h40KO3GtqHX2HAtPI1NFuaunEz1unzi0yaWA==", "3c94394c-0b46-41a3-8e78-6d439acbb30d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Allocation");

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
    }
}
