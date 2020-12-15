using HtmlAgilityPack;
using Library_Tabla_Valores;
using Microsoft.EntityFrameworkCore;
using ScrapySharp.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Acara_Web
{
    class Program
    {
        static void Main()
        {
            Console.Title = "/Scraping-Web";
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;

            Inicio();

            Menu();
        }

        static public void Inicio()
        {
            Console.Clear();
            Console.WriteLine("BIENVENIDO. SCRAPING ACARA VALORES AUTOMOTOR ACTUALIZADOS");
            Console.WriteLine("\n\r1 => Filtrar por Marca");
            Console.WriteLine("2 => Filtrar por Marca y Modelo");
            Console.WriteLine("3 => Filtrar por Marca, Modelo y versión");
            Console.WriteLine("4 => Actualizar valores Db");
            Console.WriteLine("\n\rCualquier otra tecla => Salir");
        }

        static void Menu()
        {
            string texto;
            texto = Console.ReadLine();
            if (texto == "1")
            {
                Console.Clear();
                ListaMarcas();
                Console.WriteLine("\r\nEscriba Marca que desea filtrar por scrapping:");
                string marca = Console.ReadLine();

                MuestraValoresScrap("https://www.acara.org.ar/guia-oficial-de-precios.php?tipo=AUTOS&marca=" + marca + "&modelo=todos&version=todas");

                Console.WriteLine("\r\nPresione cualquier tecla para volver atrás");
                Console.ReadKey();
                Main();
            }
            if (texto == "2")
            {
                Console.Clear();
                ListaMarcas();
                Console.WriteLine("\r\nEscriba Marca que desea filtrar por scraping:");
                string marca = Console.ReadLine();
                ListaModelos(marca);
                Console.WriteLine("Escriba Modelo que desea filtrar por scraping:");
                string modelo = Console.ReadLine();

                MuestraValoresScrap("https://www.acara.org.ar/guia-oficial-de-precios.php?tipo=AUTOS&marca=" + marca + "&modelo=" + modelo + "&version=todas");
                Console.WriteLine("\r\nPresione cualquier tecla para volver atrás");
                Console.ReadKey();
                Main();
            }
            if (texto == "3")
            {
                Console.Clear();
                ListaMarcas();
                Console.WriteLine("\r\nEscriba Marca que desea filtrar por scraping:");
                string marca = Console.ReadLine();
                ListaModelos(marca);
                Console.WriteLine("Escriba Modelo que desea filtrar por scraping:");
                string modelo = Console.ReadLine();
                ListaVersiones(marca, modelo);
                Console.WriteLine("Escriba Versión que desea filtrar por scraping:");
                string version = Console.ReadLine();

                MuestraValoresScrap("https://www.acara.org.ar/guia-oficial-de-precios.php?tipo=AUTOS&marca=" + marca + "&modelo=" + modelo + "&version=" + version);
                Console.WriteLine("\r\nPresione cualquier tecla para volver atrás");
                Console.ReadKey();
                Main();

            }
            if (texto == "4")
            {
                Console.Clear();

                PreparaCargaDb();

                Console.WriteLine("\r\nPresione cualquier tecla para volver atrás");
                Console.ReadKey();
                Main();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        static public void ListaMarcas()
        {
            HtmlWeb oWeb = new HtmlWeb();

            HtmlDocument doc = oWeb.Load("https://www.acara.org.ar/guia-oficial-de-precios.php?tipo=AUTOS");

            foreach (HtmlNode marca in doc.DocumentNode.CssSelect(".col-sm-3"))
            {
                Console.WriteLine(Convert.ToString(marca.InnerText.Trim()));
            }
        }

        static public void ListaModelos(string marca)
        {
            HtmlWeb oWeb = new HtmlWeb();

            HtmlDocument doc = oWeb.Load("https://www.acara.org.ar/guia-oficial-de-precios.php?tipo=AUTOS&marca=" + marca);

            Console.WriteLine("");
            foreach (HtmlNode modelo in doc.DocumentNode.CssSelect(".selector .opt-select"))
            {
                Console.WriteLine(Convert.ToString(modelo.InnerText.Trim()));
            }
            Console.WriteLine("");
        }

        static public void ListaVersiones(string marca, string modelo)
        {
            HtmlWeb oWeb = new HtmlWeb();

            HtmlDocument doc = oWeb.Load("https://www.acara.org.ar/guia-oficial-de-precios.php?tipo=AUTOS&marca=" + marca + "&modelo=" + modelo);

            Console.WriteLine("");
            foreach (HtmlNode version in doc.DocumentNode.CssSelect(".selector .opt-select"))
            {
                Console.WriteLine(Convert.ToString(version.InnerText.Trim()));
            }
            Console.WriteLine("");
        }

        static public void MuestraValoresScrap(string ruta)
        {

            HtmlWeb oWeb = new HtmlWeb();

            HtmlDocument doc = oWeb.Load(ruta);

            if (doc.DocumentNode.SelectNodes("//tr/th") != null && doc.DocumentNode.SelectNodes("//tr[td]") != null)
            {
                foreach (HtmlNode row in doc.DocumentNode.SelectNodes("//tr/th"))
                {
                    Console.WriteLine(row.InnerText);
                }
                foreach (var cells in doc.DocumentNode.SelectNodes("//tr[td]"))
                {
                    Console.WriteLine(cells.InnerText);
                }
            }
            else
            {
                Console.WriteLine("\r\nError. Datos ingresados incorrectos.");
            }

        }

        static void PreparaCargaDb()
        {
            var _context = new ValoresContext();
            _context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Valores]"); //borran antiguos datos
            _context.SaveChanges();

            HtmlWeb oWeb = new HtmlWeb();

            HtmlDocument doc = oWeb.Load("https://www.acara.org.ar/guia-oficial-de-precios.php?tipo=AUTOS");

            List<string> Marcas = new List<string>();
            foreach (HtmlNode marcas in doc.DocumentNode.CssSelect(".col-sm-3"))
            {
                Marcas.Add(marcas.InnerText.Trim());
            }

            for (int i = 0; i < Marcas.Count; i++)
            {
                Console.Clear();

                Console.WriteLine("\n\rCargando " + Marcas[i] + " a la DB..");

                CargaValoresDb("https://www.acara.org.ar/guia-oficial-de-precios.php?tipo=AUTOS&marca=" + Marcas[i] + "&modelo=todos&version=todas", Marcas[i]);
                
                Console.WriteLine("\n\rMarca cargada con éxito!");
            }

        }

        static void CargaValoresDb(string ruta, string marca)
        {
            var _context = new ValoresContext();

            HtmlWeb oWeb = new HtmlWeb();

            HtmlDocument doc = oWeb.Load(ruta);


            if (doc.DocumentNode.SelectNodes("//tr/th") != null && doc.DocumentNode.SelectNodes("//tr[td]") != null)
            {
                DataTable table = new DataTable();
                foreach (HtmlNode header in doc.DocumentNode.SelectNodes("//tr/th"))
                    table.Columns.Add(header.InnerText);
                foreach (var row in doc.DocumentNode.SelectNodes("//tr[td]"))
                    table.Rows.Add(row.SelectNodes("td").Select(td => td.InnerText).ToArray());

                foreach (DataRow fila in table.Rows)
                {
                    Valores val = new Valores();
                    val.Marca = marca;
                    val.Modelo = fila.ItemArray.GetValue(0).ToString();
                    val.Version = fila.ItemArray.GetValue(1).ToString();
                    val.Moneda = fila.ItemArray.GetValue(2).ToString();
                    val.Okm = fila.ItemArray.GetValue(3).ToString();
                    val.Año2019 = fila.ItemArray.GetValue(4).ToString();
                    val.Año2018 = fila.ItemArray.GetValue(5).ToString();
                    val.Año2017 = fila.ItemArray.GetValue(6).ToString();
                    val.Año2016 = fila.ItemArray.GetValue(7).ToString();
                    val.Año2015 = fila.ItemArray.GetValue(8).ToString();
                    val.Año2014 = fila.ItemArray.GetValue(9).ToString();
                    val.Año2013 = fila.ItemArray.GetValue(10).ToString();
                    val.Año2012 = fila.ItemArray.GetValue(11).ToString();
                    val.Año2011 = fila.ItemArray.GetValue(12).ToString();
                    val.Año2010 = fila.ItemArray.GetValue(13).ToString();
                    val.Año2009 = fila.ItemArray.GetValue(14).ToString();
                    val.Año2008 = fila.ItemArray.GetValue(15).ToString();
                    val.Año2007 = fila.ItemArray.GetValue(16).ToString();
                    val.Año2006 = fila.ItemArray.GetValue(17).ToString();
                    val.Año2005 = fila.ItemArray.GetValue(18).ToString();

                    _context.Add(val);

                    _context.SaveChanges();
                }
            }
            else
            {
                Console.WriteLine("\r\nError. Datos ingresados incorrectos.");
                Console.ReadKey();
                Main();
            }
        }
    }
}