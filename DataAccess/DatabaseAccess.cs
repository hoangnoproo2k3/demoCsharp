using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace p05_btvn_b02.DataAccess
{

    internal class DatabaseAccess
    {
        private readonly string _connectionString;

        public DatabaseAccess()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["QLNSConnectionString"].ConnectionString;
        }

        public bool InsertOrUpdateProduct(Product product)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("UpsertMatHang", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@iMaH", product.MaH);
                command.Parameters.AddWithValue("@sTenhang", product.TenHang);
                command.Parameters.AddWithValue("@iSoluong", product.SoLuong);
                command.Parameters.AddWithValue("@fDongia", product.DonGia);
                command.Parameters.AddWithValue("@iMaLH", product.MaLH);

                connection.Open();
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }
        public bool DeleteProduct(int maH)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DeleteMatHang", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@iMaH", maH);

                connection.Open();
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }
        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("SELECT iMaH, sTenhang, iSoluong, fDongia, iMaLH FROM tblMatHang", connection);
                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = new Product
                        {
                            MaH = reader.GetInt32(reader.GetOrdinal("iMaH")),
                            TenHang = reader.GetString(reader.GetOrdinal("sTenhang")),
                            SoLuong = reader.GetInt32(reader.GetOrdinal("iSoluong")),
                            DonGia = reader.GetDecimal(reader.GetOrdinal("fDongia")),
                            MaLH = reader.GetInt32(reader.GetOrdinal("iMaLH"))
                        };
                        products.Add(product);
                    }
                }
            }

            return products;
        }
    }
}
