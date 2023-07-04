using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FamilyBudget.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RepeatDay = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "transactions",
                columns: new[] { "Id", "Category", "CreatedTime", "Description", "ModifiedTime", "RepeatDay", "Value" },
                values: new object[,]
                {
                    { 1, "Salary", new DateTime(2022, 12, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2343), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since", new DateTime(2023, 1, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2381), 0, 572m },
                    { 2, "General", new DateTime(2022, 12, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2393), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since", new DateTime(2023, 1, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2395), 0, -852m },
                    { 3, "MonthlyRepetition", new DateTime(2022, 12, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2399), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since", new DateTime(2023, 1, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2402), 0, -55m },
                    { 4, "Salary", new DateTime(2022, 11, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2405), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since", new DateTime(2023, 1, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2407), 0, 606m },
                    { 5, "General", new DateTime(2022, 11, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2410), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since", new DateTime(2023, 1, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2412), 0, -495m },
                    { 6, "MonthlyRepetition", new DateTime(2022, 11, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2416), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since", new DateTime(2023, 1, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2418), 0, -55m },
                    { 7, "Salary", new DateTime(2022, 10, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2421), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since", new DateTime(2023, 1, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2423), 0, 476m },
                    { 8, "General", new DateTime(2022, 10, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2426), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since", new DateTime(2023, 1, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2428), 0, -169m },
                    { 9, "MonthlyRepetition", new DateTime(2022, 10, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2430), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since", new DateTime(2023, 1, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2432), 0, -55m },
                    { 10, "Salary", new DateTime(2022, 9, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2436), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since", new DateTime(2023, 1, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2438), 0, 612m },
                    { 11, "General", new DateTime(2022, 9, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2440), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since", new DateTime(2023, 1, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2442), 0, -499m },
                    { 12, "MonthlyRepetition", new DateTime(2022, 9, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2445), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since", new DateTime(2023, 1, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2447), 0, -55m },
                    { 13, "Salary", new DateTime(2022, 8, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2449), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since", new DateTime(2023, 1, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2451), 0, 839m },
                    { 14, "General", new DateTime(2022, 8, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2454), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since", new DateTime(2023, 1, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2456), 0, -298m },
                    { 15, "MonthlyRepetition", new DateTime(2022, 8, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2458), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since", new DateTime(2023, 1, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2460), 0, -55m },
                    { 16, "Salary", new DateTime(2022, 7, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2506), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since", new DateTime(2023, 1, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2508), 0, 636m },
                    { 17, "General", new DateTime(2022, 7, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2511), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since", new DateTime(2023, 1, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2513), 0, -528m },
                    { 18, "MonthlyRepetition", new DateTime(2022, 7, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2517), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since", new DateTime(2023, 1, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2519), 0, -55m },
                    { 19, "Health", new DateTime(2023, 5, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2523), "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since", new DateTime(2023, 1, 9, 3, 59, 45, 698, DateTimeKind.Local).AddTicks(2525), 14, -55m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transactions");
        }
    }
}
