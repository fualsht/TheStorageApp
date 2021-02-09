﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheStorageApp.API.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Password = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Email = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    FirstName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    LastName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Address = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    GPSLocation = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Website = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shops_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shops_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Color = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tags_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "char(36)", nullable: false),
                    ReceiptHolderId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ShopId = table.Column<Guid>(type: "char(36)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipts_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipts_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipts_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipts_Users_ReceiptHolderId",
                        column: x => x.ReceiptHolderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Color = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "char(36)", nullable: false),
                    ReceiptId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Receipts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Categories_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "char(36)", nullable: false),
                    Image = table.Column<byte[]>(type: "longblob", nullable: true),
                    ReceiptId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptImages_Receipts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptImages_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptImages_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptTag",
                columns: table => new
                {
                    ReceiptsId = table.Column<Guid>(type: "char(36)", nullable: false),
                    TagsId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptTag", x => new { x.ReceiptsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ReceiptTag_Receipts_ReceiptsId",
                        column: x => x.ReceiptsId,
                        principalTable: "Receipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOn", "Email", "FirstName", "LastName", "ModifiedOn", "Name", "Password" },
                values: new object[] { new Guid("e0a38d3f-190e-4995-852f-32f483a975d8"), new DateTime(2021, 2, 9, 15, 21, 11, 460, DateTimeKind.Local).AddTicks(3713), "system@email.com", "system", "user", new DateTime(2021, 2, 9, 15, 21, 11, 460, DateTimeKind.Local).AddTicks(4224), "<SYSTEM>", "password" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Color", "CreatedById", "CreatedOn", "ModifiedById", "ModifiedOn", "Name", "ReceiptId" },
                values: new object[] { new Guid("a558aad5-6e46-48bd-b5b5-e9c0b2279527"), -1, new Guid("e0a38d3f-190e-4995-852f-32f483a975d8"), new DateTime(2021, 2, 9, 15, 21, 11, 464, DateTimeKind.Local).AddTicks(3544), new Guid("e0a38d3f-190e-4995-852f-32f483a975d8"), new DateTime(2021, 2, 9, 15, 21, 11, 464, DateTimeKind.Local).AddTicks(4057), "<DEFAULT>", null });

            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "Address", "CreatedById", "CreatedOn", "GPSLocation", "ModifiedById", "ModifiedOn", "Name", "Website" },
                values: new object[] { new Guid("4c4a50ac-dbf4-436f-aeef-7202e116e73d"), "", new Guid("e0a38d3f-190e-4995-852f-32f483a975d8"), new DateTime(2021, 2, 9, 15, 21, 11, 468, DateTimeKind.Local).AddTicks(6901), "", new Guid("e0a38d3f-190e-4995-852f-32f483a975d8"), new DateTime(2021, 2, 9, 15, 21, 11, 468, DateTimeKind.Local).AddTicks(7378), "<DEFAULT>", "" });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Color", "CreatedById", "CreatedOn", "ModifiedById", "ModifiedOn", "Name" },
                values: new object[] { new Guid("786759ea-6f4a-4b41-b9ec-276ac980cab2"), 0, new Guid("e0a38d3f-190e-4995-852f-32f483a975d8"), new DateTime(2021, 2, 9, 15, 21, 11, 470, DateTimeKind.Local).AddTicks(855), new Guid("e0a38d3f-190e-4995-852f-32f483a975d8"), new DateTime(2021, 2, 9, 15, 21, 11, 470, DateTimeKind.Local).AddTicks(1357), "<DEFAULT>" });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatedById",
                table: "Categories",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ModifiedById",
                table: "Categories",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ReceiptId",
                table: "Categories",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptImages_CreatedById",
                table: "ReceiptImages",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptImages_ModifiedById",
                table: "ReceiptImages",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptImages_ReceiptId",
                table: "ReceiptImages",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_CategoryId",
                table: "Receipts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_CreatedById",
                table: "Receipts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_ModifiedById",
                table: "Receipts",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_ReceiptHolderId",
                table: "Receipts",
                column: "ReceiptHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_ShopId",
                table: "Receipts",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTag_TagsId",
                table: "ReceiptTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_CreatedById",
                table: "Shops",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_ModifiedById",
                table: "Shops",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CreatedById",
                table: "Tags",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ModifiedById",
                table: "Tags",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Categories_CategoryId",
                table: "Receipts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Receipts_ReceiptId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "ReceiptImages");

            migrationBuilder.DropTable(
                name: "ReceiptTag");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
