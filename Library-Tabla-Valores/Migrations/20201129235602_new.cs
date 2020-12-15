using Microsoft.EntityFrameworkCore.Migrations;

namespace Library_Tabla_Valores.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Valores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marca = table.Column<string>(nullable: true),
                    Modelo = table.Column<string>(nullable: true),
                    Version = table.Column<string>(nullable: true),
                    Moneda = table.Column<string>(nullable: true),
                    Okm = table.Column<string>(nullable: true),
                    Año2019 = table.Column<string>(nullable: true),
                    Año2018 = table.Column<string>(nullable: true),
                    Año2017 = table.Column<string>(nullable: true),
                    Año2016 = table.Column<string>(nullable: true),
                    Año2015 = table.Column<string>(nullable: true),
                    Año2014 = table.Column<string>(nullable: true),
                    Año2013 = table.Column<string>(nullable: true),
                    Año2012 = table.Column<string>(nullable: true),
                    Año2011 = table.Column<string>(nullable: true),
                    Año2010 = table.Column<string>(nullable: true),
                    Año2009 = table.Column<string>(nullable: true),
                    Año2008 = table.Column<string>(nullable: true),
                    Año2007 = table.Column<string>(nullable: true),
                    Año2006 = table.Column<string>(nullable: true),
                    Año2005 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valores", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Valores");
        }
    }
}
