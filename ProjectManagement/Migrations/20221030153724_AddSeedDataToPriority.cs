using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagement.Migrations
{
    public partial class AddSeedDataToPriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2bb80575-a02f-45e3-9504-1f225cbf237e",
                column: "ConcurrencyStamp",
                value: "65261979-f5ee-4e2d-bfa6-bec1ff95c639");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "989c13c2-05a1-471b-abe4-1aecf8485887",
                column: "ConcurrencyStamp",
                value: "fddeb99c-5428-4ec5-bb85-74a78ced67bd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5393722f-d39e-4bb1-8a5e-9641e3ce4a25",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4508485d-1778-4812-8082-08f08217582f", "AQAAAAEAACcQAAAAEIjq+ONSmqXck39RkxUo3IhuVyAxeZgsyfakvL5P7Uv8IjevNcC+f6UeMqg+Xp0L0w==", "514deaaa-0982-408e-89d6-f8efa28cead1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d91481c-2946-4b3d-9117-072c4953475e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a0d19f4e-2828-4219-a2b9-ff457ba079c0", "AQAAAAEAACcQAAAAEFheTMjRDo+bhBCDiWI/fB1xXDOnIqfrY1Uupw9Txyz7xdhVljz+1VsGS2M5SR0rnQ==", "1b341c4d-83c9-41b1-8862-48557af14558" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "89c4435a-6986-4995-b514-2aec9af0ac3f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "76a0f7e4-fcec-4fec-a28a-850343c7c687", "AQAAAAEAACcQAAAAECeaF9//g/okO3oakc6znuRiJFiXUzV/8Rq1ZRQ70ynugrqbOEMWHhpUD3giR23R/A==", "7e98ee4c-6ec6-466b-be2e-85ca10084f00" });

            migrationBuilder.InsertData(
                table: "Priority",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "02d32d4a-9f98-4474-bf9f-15feee52445f\r\n", "High" },
                    { "356e0f04-b5c1-48d1-9d6c-1bcb044d695b\r\n", "Medium" },
                    { "61867ccb-f44a-4c89-a025-5fc0140894d0\r\n", "Urgent" },
                    { "c646bac9-3b96-4635-b7a6-68e1da239c51\r\n", "Low" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Priority",
                keyColumn: "Id",
                keyValue: "02d32d4a-9f98-4474-bf9f-15feee52445f\r\n");

            migrationBuilder.DeleteData(
                table: "Priority",
                keyColumn: "Id",
                keyValue: "356e0f04-b5c1-48d1-9d6c-1bcb044d695b\r\n");

            migrationBuilder.DeleteData(
                table: "Priority",
                keyColumn: "Id",
                keyValue: "61867ccb-f44a-4c89-a025-5fc0140894d0\r\n");

            migrationBuilder.DeleteData(
                table: "Priority",
                keyColumn: "Id",
                keyValue: "c646bac9-3b96-4635-b7a6-68e1da239c51\r\n");

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
        }
    }
}
