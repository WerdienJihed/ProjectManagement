using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagement.Migrations
{
    public partial class AddPriorityToTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PriorityId",
                table: "Ticket",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Priority",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priority", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2bb80575-a02f-45e3-9504-1f225cbf237e",
                column: "ConcurrencyStamp",
                value: "2e030bc7-d2dd-452b-9f2b-464d785ebaf9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "989c13c2-05a1-471b-abe4-1aecf8485887",
                column: "ConcurrencyStamp",
                value: "16d71f76-ab95-4245-ab5f-82beabc77a6b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5393722f-d39e-4bb1-8a5e-9641e3ce4a25",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "34bd4cad-73b0-44c9-a331-8c59b04b0460", "AQAAAAEAACcQAAAAEIJSphn01Argm7a1l4pW2L9RRA7UVqjXQgOD2++/uH6TU4GpzZIcVe/rz5NohU5AdQ==", "91025adc-c848-4cae-aa2c-f1924030c4c8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d91481c-2946-4b3d-9117-072c4953475e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c2d9b570-b5ef-45ab-b71e-f798eefb2b0a", "AQAAAAEAACcQAAAAEJuPGDeV3DjLti11eVJHMgHV84qnjZg3285S2i91rWM54cxETCU8BeCSa7P1C8QQow==", "1b899b8d-b3cc-4646-9c06-a4e1793e2b54" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "89c4435a-6986-4995-b514-2aec9af0ac3f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dd81a4b9-a52c-4417-9fa6-44cb4316ed76", "AQAAAAEAACcQAAAAEA4ZntjXdw6c7qlO9bGwEGR1eGTKk5GHqwvEK5JzQwRraCaYYDzuHhX8hBaJGz6vQg==", "43929fbe-915f-4c71-99c6-594b84ddf1ac" });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_PriorityId",
                table: "Ticket",
                column: "PriorityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Priority_PriorityId",
                table: "Ticket",
                column: "PriorityId",
                principalTable: "Priority",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Priority_PriorityId",
                table: "Ticket");

            migrationBuilder.DropTable(
                name: "Priority");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_PriorityId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "PriorityId",
                table: "Ticket");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2bb80575-a02f-45e3-9504-1f225cbf237e",
                column: "ConcurrencyStamp",
                value: "8ad4fe42-2729-49b3-96c7-f4aa88427ca7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "989c13c2-05a1-471b-abe4-1aecf8485887",
                column: "ConcurrencyStamp",
                value: "e9011710-8b18-41de-9ab1-ff91b44eaedb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5393722f-d39e-4bb1-8a5e-9641e3ce4a25",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "824560d8-a95f-48ad-ab8c-91f26d0b3841", "AQAAAAEAACcQAAAAEHgJG0pNvcWiJZ+CBwcXpyPv2cv1HFQ7a80TM5RIrYDXsQWQj+NOGhn1t+LWLbHjIw==", "2c0ac15d-360d-4b95-8c44-52cfa003e0c7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d91481c-2946-4b3d-9117-072c4953475e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a7a69ba7-469e-4b90-9678-cf7a8ad01571", "AQAAAAEAACcQAAAAELxhQcqv2igG/MTC2d2ReQttqGGowznZ46qcDS1eRt3hFPWprvjH2k8Co04tIhxIDA==", "a46f3eb2-907c-411f-a984-0e40666b24d4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "89c4435a-6986-4995-b514-2aec9af0ac3f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0dac7437-bd1a-4581-bc23-467d098f039a", "AQAAAAEAACcQAAAAEJttFFKwUmJEyhDL6IbjQUJY0szxKbXqBqxVsqa2s4cWw8/cCJC2p0d7jzlr0X1VGg==", "d34d8c2e-2ada-49fb-8876-c2b1c416da87" });
        }
    }
}
