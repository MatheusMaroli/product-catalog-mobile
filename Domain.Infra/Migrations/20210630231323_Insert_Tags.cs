using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Infra.Migrations
{
    public partial class Insert_Tags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert Into tag (Id, Name) Values(1, \"Roupas\")");
            migrationBuilder.Sql("Insert Into tag (Id, Name) Values(2, \"Utensílios de cozinha\")");
            migrationBuilder.Sql("Insert Into tag (Id, Name) Values(3, \"Decoração de Casa\")");
            migrationBuilder.Sql("Insert Into tag (Id, Name) Values(4, \"Livros\")");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From tag where Id=1");
            migrationBuilder.Sql("Delete From tag where Id=2");
            migrationBuilder.Sql("Delete From tag where Id=3");
            migrationBuilder.Sql("Delete From tag where Id=4");
        }
    }
}
