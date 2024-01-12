using p05_btvn_b02.DataAccess;
using p05_btvn_b02.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace p05_btvn_b02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductService productService = new ProductService();
            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1: Add or Update Product");
                Console.WriteLine("2: Delete Product");
                Console.WriteLine("3: View All Products");
                Console.WriteLine("4: Exit");
                Console.Write("\r\nSelect an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddOrUpdateProduct(productService);
                        break;
                    case "2":
                        DeleteProduct(productService);
                        break;
                    case "3":
                        ViewAllProducts(productService);
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Please select a valid option!");
                        break;
                }
                DisplayAllProducts(productService);
            }
        }

        private static void AddOrUpdateProduct(ProductService productService)
        {
            Console.WriteLine("Enter product details:");
            Console.Write("MaH: ");
            int maH = int.Parse(Console.ReadLine());
            Console.Write("TenHang: ");
            string tenHang = Console.ReadLine();
            Console.Write("SoLuong: ");
            int soLuong = int.Parse(Console.ReadLine());
            Console.Write("DonGia: ");
            decimal donGia = decimal.Parse(Console.ReadLine());
            Console.Write("MaLH: ");
            int maLH = int.Parse(Console.ReadLine());

            var product = new Product
            {
                MaH = maH,
                TenHang = tenHang,
                SoLuong = soLuong,
                DonGia = donGia,
                MaLH = maLH
            };

            bool success = productService.AddOrUpdateProduct(product);
            Console.WriteLine(success ? "Operation successful." : "Operation failed.");
        }

        private static void DeleteProduct(ProductService productService)
        {
            Console.Write("Enter MaH of the product to delete: ");
            int maH = int.Parse(Console.ReadLine());
            bool success = productService.DeleteProduct(maH);
            Console.WriteLine(success ? "Product deleted successfully." : "Product deletion failed.");
        }
        //private static void ViewAllProducts(ProductService productService)
        //{
        //    List<Product> products = productService.GetAllProducts();
        //    foreach (var product in products)
        //    {
        //        Console.WriteLine($"MaH: {product.MaH}, TenHang: {product.TenHang}, SoLuong: {product.SoLuong}, DonGia: {product.DonGia}, MaLH: {product.MaLH}");
        //    }
        //}
        private static void ViewAllProducts(ProductService productService)
        {
            List<Product> products = productService.GetAllProducts();

            // Set table headers
            string[] headers = { "MaH", "TenHang", "SoLuong", "DonGia", "MaLH" };
            int[] columnWidths = { 10, 30, 10, 10, 10 };

            // Calculate the table width
            int tableWidth = columnWidths.Sum() + (headers.Length - 1);

            // Write the header with colors
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(new string(' ', tableWidth)); // Top padding for header
            for (int i = 0; i < headers.Length; i++)
            {
                Console.Write(headers[i].PadRight(columnWidths[i]));
            }
            Console.WriteLine();
            Console.WriteLine(new string(' ', tableWidth)); // Bottom padding for header
            Console.ResetColor();

            // Write the border after the header
            Console.WriteLine(new string('-', tableWidth));

            // Write the product rows
            foreach (var product in products)
            {
                Console.Write(product.MaH.ToString().PadRight(columnWidths[0]));
                Console.Write(product.TenHang.PadRight(columnWidths[1]));
                Console.Write(product.SoLuong.ToString().PadRight(columnWidths[2]));
                Console.Write(product.DonGia.ToString("C", CultureInfo.CreateSpecificCulture("en-US")).PadRight(columnWidths[3]));
                Console.Write(product.MaLH.ToString().PadRight(columnWidths[4]));
                Console.WriteLine();
            }

            // Write the border at the bottom of the table
            Console.WriteLine(new string('-', tableWidth));
        }

        private static void DisplayAllProducts(ProductService productService)
        {
            Console.WriteLine("Current list of products:");
            ViewAllProducts(productService);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
