using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Demo.Migrations
{
    public partial class UpdateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InternName = table.Column<string>(type: "text", nullable: true),
                    InternAddress = table.Column<string>(type: "text", nullable: true),
                    ImageData = table.Column<byte[]>(type: "bytea", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    InternMail = table.Column<string>(type: "text", nullable: true),
                    InternMailReplace = table.Column<string>(type: "text", nullable: true),
                    University = table.Column<string>(type: "text", nullable: true),
                    CitizenIdentification = table.Column<string>(type: "text", nullable: true),
                    CitizenIdentificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Major = table.Column<string>(type: "text", nullable: true),
                    Internable = table.Column<bool>(type: "boolean", nullable: true),
                    FullTime = table.Column<bool>(type: "boolean", nullable: true),
                    Cvfile = table.Column<string>(type: "text", nullable: true),
                    InternSpecialized = table.Column<int>(type: "integer", nullable: true),
                    TelephoneNum = table.Column<string>(type: "text", nullable: true),
                    InternStatus = table.Column<string>(type: "text", nullable: true),
                    RegisteredDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    HowToKnowAlta = table.Column<string>(type: "text", nullable: true),
                    InternPassword = table.Column<string>(type: "text", nullable: true),
                    ForeignLanguage = table.Column<string>(type: "text", nullable: true),
                    YearOfExperiences = table.Column<short>(type: "smallint", nullable: true),
                    PasswordStatus = table.Column<bool>(type: "boolean", nullable: true),
                    ReadyToWork = table.Column<bool>(type: "boolean", nullable: true),
                    InternEnabled = table.Column<bool>(type: "boolean", nullable: true),
                    EntranceTest = table.Column<float>(type: "real", nullable: true),
                    Introduction = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    LinkProduct = table.Column<string>(type: "text", nullable: true),
                    JobFields = table.Column<string>(type: "text", nullable: true),
                    HiddenToEnterprise = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "AllowAccesses",
                columns: table => new
                {
                    AllowAccessId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    TableName = table.Column<string>(type: "text", nullable: false),
                    AccessProperties = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowAccesses", x => x.AllowAccessId);
                    table.ForeignKey(
                        name: "FK_AllowAccesses_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllowAccesses_RoleId",
                table: "AllowAccesses",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId",
                unique: true);
            
            for (int i = 1; i <= 10; i++)
{
   migrationBuilder.InsertData(
    "Interns", 
    columns: new[] 
    {
        "Id", "InternName", "InternAddress", "ImageData", "DateOfBirth", "InternMail", 
        "InternMailReplace", "University", "CitizenIdentification", "CitizenIdentificationDate", 
        "Major", "Internable", "FullTime", "Cvfile", "InternSpecialized", "TelephoneNum", 
        "InternStatus", "RegisteredDate", "HowToKnowAlta", "InternPassword", "ForeignLanguage", 
        "YearOfExperiences", "PasswordStatus", "ReadyToWork", "InternEnabled", "EntranceTest", 
        "Introduction", "Note", "LinkProduct", "JobFields", "HiddenToEnterprise"
    },
    values: new object[] 
    {
        i, // Id
        "Intern-" + i.ToString("D2"), // InternName
        $"Address-{i}", // InternAddress
        null, // ImageData (Set null or your byte array)
        new DateTime(1995, 6, 15).AddDays(i).ToUniversalTime(), // DateOfBirth (Converted to UTC)
        $"intern{i.ToString("D2")}@example.com", // InternMail
        $"replace{i.ToString("D2")}@example.com", // InternMailReplace
        "University of Example", // University
        "ID" + i.ToString("D5"), // CitizenIdentification
        new DateTime(2020, 1, 1).AddDays(i).ToUniversalTime(), // CitizenIdentificationDate (Converted to UTC)
        "Software Engineering", // Major
        i % 2 == 0, // Internable (True for even i)
        i % 2 != 0, // FullTime (True for odd i)
        null, // Cvfile (Set null or file path)
        i % 2 == 0 ? 1 : 2, // InternSpecialized (Alternating values)
        $"123-456-78{i}", // TelephoneNum
        "Active", // InternStatus
        DateTime.UtcNow, // RegisteredDate (UTC)
        "Referral", // HowToKnowAlta
        "password123", // InternPassword
        "English", // ForeignLanguage
        (short)(i % 5), // YearOfExperiences
        true, // PasswordStatus
        true, // ReadyToWork
        i % 2 == 0, // InternEnabled (True for even i)
        i * 10.5f, // EntranceTest (Random values)
        "Introduction text here", // Introduction
        "Note text", // Note
        "http://linkproduct.com", // LinkProduct
        "Development", // JobFields
        i % 2 == 0 // HiddenToEnterprise (True for even i)
    }
);
}

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllowAccesses");

            migrationBuilder.DropTable(
                name: "Interns");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
