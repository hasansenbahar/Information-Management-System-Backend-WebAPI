using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebService.API.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allocation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExitTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllocationCount = table.Column<int>(type: "int", nullable: false),
                    AllocationMission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allocation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SicilNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLdap = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c5as74e-sdfr-446f-86af-483d56fhh210", null, null, "Admin", "ADMIN" },
                    { "2c5e174e-3b0e-446f-86af-483d56fduu10", null, null, "SuperAdmin", "SUPERADMIN" },
                    { "2c5e1dde-3b0e-45d3-86af-483d56fll210", null, null, "Basic", "BASIC" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedBy", "CreatedOn", "DeletedOn", "Email", "EmailConfirmed", "FirstName", "ImageUrl", "IsActive", "IsDeleted", "IsLdap", "LastModifiedBy", "LastModifiedOn", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "SicilNo", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "123458as-a24d-3456-a6c6-123fhj48cdb9", 0, "33faa1b2-04ae-4cfa-a155-d44623cb97cd", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "basic@mail.com", true, null, null, null, null, null, null, null, null, true, null, "BASIC@MAIL.COM", "BASIC", "AQAAAAIAAYagAAAAEIk36oWGww6OWweWPk4j6Uup/hvhyx46j1kUKhw7S3/zOOPe49RnTh/r150BD6H5Mw==", null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e2c977dc-11fe-48b4-b98c-a70ed5761444", null, false, "basic" },
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "eff4c6e7-2fa3-4705-863e-49c3fae209a7", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@mail.com", true, null, null, null, null, null, null, null, null, true, null, "ADMIN@MAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEAqEuWf51/wlSBFnNQ+TpYLRoC/OpnkGf+slTReouZHXiEUjmb0n5vrjPZ5mro2vAA==", null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "6f312c06-7b73-4d75-8340-180347bae8b4", null, false, "admin" },
                    { "8e4458as-a24d-3456-a6c6-944fhj48cdb9", 0, "b3806fff-dd50-43c3-bc91-6ef91f9c3e59", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "superadmin@mail.com", true, null, null, null, null, null, null, null, null, true, null, "SUPERADMIN@MAIL.COM", "SUPERADMIN", "AQAAAAIAAYagAAAAEO0VqJqfbjKtCMt0VwOJgHHUbLTQQ/Uw4O6VGB2Cyw1TCvOFye2HUgFDWhXlldfD9g==", null, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "d91194b1-d074-44a8-ac04-7f0d65b5889a", null, false, "superadmin" }
                });

            migrationBuilder.InsertData(
                table: "RoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "Permission", "Permissions.Todo.View", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 2, "Permission", "Permissions.Todo.Create", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 3, "Permission", "Permissions.Todo.Edit", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 4, "Permission", "Permissions.Todo.Delete", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 5, "Permission", "Permissions.Todo.ViewById", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 6, "Permission", "Permissions.Todo.Exists", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 7, "Permission", "Permissions.Allocation.View", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 8, "Permission", "Permissions.Allocation.Create", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 9, "Permission", "Permissions.Allocation.Edit", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 10, "Permission", "Permissions.Allocation.Delete", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 11, "Permission", "Permissions.Allocation.ViewById", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 12, "Permission", "Permissions.Allocation.Exists", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 13, "Permission", "Permissions.Person.View", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 14, "Permission", "Permissions.Person.Create", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 15, "Permission", "Permissions.Person.Edit", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 16, "Permission", "Permissions.Person.Delete", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 17, "Permission", "Permissions.Person.ViewById", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 18, "Permission", "Permissions.Person.Exists", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 19, "Permission", "Permissions.Users.View", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 20, "Permission", "Permissions.Users.Create", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 21, "Permission", "Permissions.Users.Edit", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 22, "Permission", "Permissions.Users.Delete", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 23, "Permission", "Permissions.Users.ViewById", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 24, "Permission", "Permissions.Users.CreateRole", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 25, "Permission", "Permissions.Users.DeleteRole", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 26, "Permission", "Permissions.Users.Exists", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 27, "Permission", "Permissions.Roles.View", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 28, "Permission", "Permissions.Roles.Create", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 29, "Permission", "Permissions.Roles.Edit", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 30, "Permission", "Permissions.Roles.Delete", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 31, "Permission", "Permissions.Roles.ViewById", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 32, "Permission", "Permissions.Permissions.View", "2c5e174e-3b0e-446f-86af-483d56fduu10" },
                    { 33, "Permission", "Permissions.Permissions.Edit", "2c5e174e-3b0e-446f-86af-483d56fduu10" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2c5e1dde-3b0e-45d3-86af-483d56fll210", "123458as-a24d-3456-a6c6-123fhj48cdb9" },
                    { "2c5as74e-sdfr-446f-86af-483d56fhh210", "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                    { "2c5e174e-3b0e-446f-86af-483d56fduu10", "8e4458as-a24d-3456-a6c6-944fhj48cdb9" }
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allocation");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
