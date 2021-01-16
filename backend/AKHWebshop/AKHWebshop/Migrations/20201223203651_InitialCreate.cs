using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AKHWebshop.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "billing_info",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    country = table.Column<string>(type: "varchar(256)", nullable: false),
                    zip_code = table.Column<string>(type: "varchar(4)", nullable: false),
                    city = table.Column<string>(type: "varchar(256)", nullable: false),
                    public_space_type = table.Column<string>(type: "varchar(20)", nullable: false),
                    public_space_name = table.Column<string>(type: "varchar(256)", nullable: false),
                    state = table.Column<string>(type: "varchar(256)", nullable: true),
                    house_number = table.Column<ushort>(nullable: false),
                    floor = table.Column<byte>(nullable: true),
                    door = table.Column<ushort>(nullable: true),
                    first_name = table.Column<string>(type: "varchar(256)", nullable: false),
                    last_name = table.Column<string>(type: "varchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_billing_info", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    name = table.Column<string>(type: "varchar(256)", nullable: true),
                    display_name = table.Column<string>(type: "varchar(256)", nullable: false),
                    image_name = table.Column<string>(type: "varchar(256)", nullable: true),
                    status = table.Column<string>(type: "varchar(256)", nullable: false, defaultValue: "Hidden"),
                    price = table.Column<uint>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    normalized_username = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    normalized_email = table.Column<string>(nullable: true),
                    email_confirmed = table.Column<bool>(nullable: false),
                    password_hash = table.Column<string>(nullable: true),
                    security_stamp = table.Column<string>(nullable: true),
                    concurrency_stamp = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    phone_number_confirmed = table.Column<bool>(nullable: false),
                    two_factor_enabled = table.Column<bool>(nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(nullable: true),
                    lockout_enabled = table.Column<bool>(nullable: false),
                    access_failed_count = table.Column<int>(nullable: false),
                    username = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", nullable: false),
                    created_at = table.Column<DateTime>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false),
                    country = table.Column<string>(type: "varchar(256)", nullable: false),
                    zip_code = table.Column<string>(type: "varchar(4)", nullable: false),
                    city = table.Column<string>(type: "varchar(256)", nullable: false),
                    public_space_type = table.Column<string>(type: "varchar(20)", nullable: false, defaultValue: "Utca"),
                    public_space_name = table.Column<string>(type: "varchar(256)", nullable: false),
                    state = table.Column<string>(type: "varchar(256)", nullable: true),
                    house_number = table.Column<ushort>(nullable: false),
                    floor = table.Column<byte>(nullable: true),
                    door = table.Column<ushort>(nullable: true),
                    shipped = table.Column<bool>(nullable: false, defaultValue: false),
                    payed = table.Column<bool>(nullable: false, defaultValue: false),
                    total_price = table.Column<uint>(nullable: false),
                    first_name = table.Column<string>(type: "varchar(256)", nullable: false),
                    last_name = table.Column<string>(type: "varchar(256)", nullable: false),
                    comment = table.Column<string>(type: "varchar(256)", nullable: true),
                    email = table.Column<string>(type: "varchar(256)", nullable: false),
                    billing_info_id = table.Column<string>(nullable: false),
                    same_billing_info = table.Column<bool>(type: "bool", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_billing_info_billing_info_id",
                        column: x => x.billing_info_id,
                        principalTable: "billing_info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "size_record",
                columns: table => new
                {
                    product_id = table.Column<string>(type: "varchar(36)", nullable: false),
                    size = table.Column<string>(type: "varchar(36)", nullable: false, defaultValue: "UNDEFINED"),
                    quantity = table.Column<ushort>(type: "smallint unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_size_record", x => new { x.product_id, x.size });
                    table.ForeignKey(
                        name: "FK_size_record_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    user_id = table.Column<string>(nullable: false),
                    role_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role", x => new { x.role_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_user_role_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_role_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_item",
                columns: table => new
                {
                    order_id = table.Column<string>(type: "varchar(36)", nullable: false),
                    product_id = table.Column<string>(type: "varchar(36)", nullable: false),
                    size = table.Column<string>(type: "varchar(36)", nullable: false),
                    amount = table.Column<ushort>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_item", x => new { x.order_id, x.product_id, x.size });
                    table.ForeignKey(
                        name: "FK_order_item_order_order_id",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "dafcb95a-d2ea-4d9d-bfbf-c58afea13c17", "15e3b432-ba6a-45b9-9b9c-b95ca8038bce", "AppRole", "admin", "Admin" });

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "62564147-c6a8-4bd4-a037-ae0a09818745", "4de4b21f-b066-4571-9f75-b497067b2aaf", "AppRole", "user", "User" });

            migrationBuilder.CreateIndex(
                name: "IX_order_billing_info_id",
                table: "order",
                column: "billing_info_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_display_name",
                table: "product",
                column: "display_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_image_name",
                table: "product",
                column: "image_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_name",
                table: "product",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_role_user_id",
                table: "user_role",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_item");

            migrationBuilder.DropTable(
                name: "size_record");

            migrationBuilder.DropTable(
                name: "user_role");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "billing_info");
        }
    }
}
