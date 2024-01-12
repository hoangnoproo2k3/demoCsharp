using p05_btvn_b02.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p05_btvn_b02.Services
{
    internal class ProductService
    {
        private readonly DatabaseAccess _dbAccess;

        public ProductService()
        {
            _dbAccess = new DatabaseAccess();
        }

        public bool AddOrUpdateProduct(Product product)
        {
            return _dbAccess.InsertOrUpdateProduct(product);
        }
        public bool DeleteProduct(int MaH)
        {
            return _dbAccess.DeleteProduct(MaH);
        }
        public List<Product> GetAllProducts()
        {
            return _dbAccess.GetAllProducts();
        }
    }
}
