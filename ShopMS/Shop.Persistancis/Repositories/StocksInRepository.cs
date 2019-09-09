using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistancis.Repositories
{
    public class StocksInRepository
    {
        MainRepository _MainRepository = new MainRepository();
        public List<Categories> GetAllCategories()
        {

            var _CategoryList = new List<Categories>();
            string query = ("Select *From Categories");
            var reader = _MainRepository.Reader(query, _MainRepository.ConnectionString());
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var _Category = new Categories();
                    _Category.Id = Convert.ToInt32(reader["Id"].ToString());
                    _Category.Name = reader["Name"].ToString();

                    _CategoryList.Add(_Category);
                }
            }
            reader.Close();

            return _CategoryList;
        }
        public List<Sizes> GetSizeByCategories(int Id)
        {
            var _SizeList = new List<Sizes>();
            string query = ("Select *From Size Where CategoriesId='" + Id + "'");
            var reader = _MainRepository.Reader(query, _MainRepository.ConnectionString());
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var _Sizes = new Sizes();
                    _Sizes.Id = Convert.ToInt32(reader["Id"].ToString());
                    _Sizes.Size = reader["Size"].ToString();

                    _SizeList.Add(_Sizes);
                }
            }
            reader.Close();

            return _SizeList;
        }
        public List<Items> GetItemsByCtgndSze(int Id,string Size)
        {
            var _ItemsList = new List<Items>();
            string query = ("Select *From Items Where Size='"+Size+"' And CategoriesId='" + Id + "' ");
            var reader = _MainRepository.Reader(query, _MainRepository.ConnectionString());
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var _Items = new Items();
                    _Items.Id = Convert.ToInt32(reader["Id"].ToString());
                    _Items.Name = reader["Name"].ToString();
                    
                    _ItemsList.Add(_Items);
                }
            }
            reader.Close();

            return _ItemsList;
        }
        public Items GetItemData(int Id)
        {
            Items _Items = null;

            string query = ("Select *From Items where Id='" + Id + "' ");
            var reader = _MainRepository.Reader(query, _MainRepository.ConnectionString());
            if (reader.HasRows)
            {
                reader.Read();
                _Items = new Items();

                _Items.Code = (reader["Code"].ToString());
            }
            reader.Close();

            return _Items;
        }
        public StocksIn GetQtyByCode(string code)
        {
            StocksIn _StocksIn = null;

            string query = ("Select *From StockIn where ItemCode='" + code + "' ");
            var reader = _MainRepository.Reader(query, _MainRepository.ConnectionString());
            if (reader.HasRows)
            {
                reader.Read();
                _StocksIn = new StocksIn();

                _StocksIn.Quantity = Convert.ToInt32(reader["Quantity"].ToString());
            }
            reader.Close();

            return _StocksIn;
        }
        public int Add(StocksIn _StocksIn)
        {
            string query = "Insert Into StockIn(Quantity,Size,UnitCost,Mrp,ItemName,ItemCode,CategoriesId,Date) Values ('" + _StocksIn.Quantity + "','" + _StocksIn.Size + "','" + _StocksIn.UnitCost + "','" + _StocksIn.Mrp + "','" + _StocksIn.ItemName + "','" + _StocksIn.ItemCode + "','" + _StocksIn.CategoriesId + "','"+DateTime.Now.ToShortDateString()+"')";
            return _MainRepository.ExecuteNonQuery(query, _MainRepository.ConnectionString());
        }
        public List<StocksIn> GetAllStocks(string size,string code,int Id)
        {
            var _StocksInList = new List<StocksIn>();
            string query = ("Select *From StockIn Where Size='" + size + "' And ItemCode='" + code + "' And CategoriesId='" + Id + "' And Date='" + DateTime.Now.ToShortDateString() + "'");
            var reader = _MainRepository.Reader(query, _MainRepository.ConnectionString());
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var _StocksIn = new StocksIn();
                    _StocksIn.ItemCode = (reader["ItemCode"].ToString());
                    _StocksIn.Quantity = Convert.ToInt32(reader["Quantity"].ToString());
                    _StocksIn.UnitCost = Convert.ToDecimal(reader["UnitCost"].ToString());
                    _StocksIn.Mrp = Convert.ToDecimal(reader["Mrp"].ToString());
                    _StocksIn.Date= (reader["Date"].ToString());

                    _StocksInList.Add(_StocksIn);
                }
            }
            reader.Close();

            return _StocksInList;
        }
    }
}
