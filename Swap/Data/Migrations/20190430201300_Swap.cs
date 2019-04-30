using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Swap.Data.Migrations
{
    public partial class Swap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Category = table.Column<string>(nullable: true),
                    Img = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "FirstName", "LastName" },
                values: new object[] { "78707d2f-d7f8-48d6-aba5-9ea0c4584ee7", 0, "8ffe9242-7001-4aaf-ac95-fdde992d3895", "ApplicationUser", "admin@admin.com", true, false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEC0POi7bbeuWJ2cH4YRzB62IGrhSmRc3pReVgOMiwzUml0wX3DBi/6XqJI4LqYjd7A==", null, false, "10c2b3d8-0ebc-4278-a2f4-ba460b15853f", false, "admin@admin.com", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Id", "Category", "Description", "Img", "UserId" },
                values: new object[,]
                {
                    { 1, "Clothing", "Cool hat in good condition", "https://www.bootbarn.com/dw/image/v2/BCCF_PRD/on/demandware.static/-/Sites-master-product-catalog-shp/default/dw7eaef6c3/images/648/2000232648_700_P1.JPG", "78707d2f-d7f8-48d6-aba5-9ea0c4584ee7" },
                    { 2, "Home Appliances", "Mildly good condition", "https://images.crateandbarrel.com/is/image/Crate/EllaWhiteTableLampOffSHF15", "78707d2f-d7f8-48d6-aba5-9ea0c4584ee7" },
                    { 3, "Clothing", "Awesome shirt! good condition.. it just doesnt fit", "https://cdn.shopify.com/s/files/1/0051/4802/products/i-octocat-code_600x600.png?v=1520399372", "78707d2f-d7f8-48d6-aba5-9ea0c4584ee7" },
                    { 4, "Home Appliances", "Super awesome bowl set", "https://www.westelm.com/weimgs/ab/images/wcm/products/201849/0247/folk-pad-printed-bowls-c.jpg", "78707d2f-d7f8-48d6-aba5-9ea0c4584ee7" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_UserId",
                table: "Item",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "78707d2f-d7f8-48d6-aba5-9ea0c4584ee7");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
