using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Infra.Migrations
{
    public partial class Insert_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("Insert Into user (Id, FullName, Email, Password) Values(1, \"Admin User\", \"admin@user.com\", \"123\")");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("Delete From user where Id=1");
        }
    }
}
