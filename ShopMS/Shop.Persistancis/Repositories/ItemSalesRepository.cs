using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistancis.Repositories
{
    public class ItemSalesRepository
    {
        MainRepository _MainRepository = new MainRepository();

        public StocksIn GetDataByCode(string code)
        {
            StocksIn _StocksIn = null;

            string query = ("Select top 1 * from StockIn Where ItemCode = '" + code + "' order by Id desc");
            var reader = _MainRepository.Reader(query, _MainRepository.ConnectionString());
            if (reader.HasRows)
            {
                reader.Read();
                _StocksIn = new StocksIn();
                _StocksIn.ItemName = (reader["ItemName"].ToString());
                _StocksIn.Mrp= Convert.ToDecimal(reader["Mrp"].ToString());
            }
            reader.Close();

            return _StocksIn;
        }
    }
}
