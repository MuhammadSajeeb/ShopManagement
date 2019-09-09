using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistancis.Repositories
{
    public class ItemRepository
    {
        MainRepository _MainRepository = new MainRepository();
        public decimal AlreadyExistData()
        {
            string query = "Select Count(*)from Items ";
            return _MainRepository.ExecuteScalar(query, _MainRepository.ConnectionString());
        }
        public Items GetLastCode()
        {
            Items _Items = null;

            string query = "Select top 1 Code from Items order by Code desc";
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

        public decimal AlreadyExistName(Items _Items)
        {
            string query = "Select Count(*)from Items Where Name='" + _Items.Name + "' And CategoriesId='"+_Items.CategoriesId+"' And Size='"+_Items.Size+"' ";
            return _MainRepository.ExecuteScalar(query, _MainRepository.ConnectionString());
        }
        public int Add(Items _Items)
        {
            string query = "Insert Into Items(Code,Name,ReorderLevel,Size,CategoriesId) Values ('" + _Items.Code + "','" + _Items.Name + "','"+_Items.ReorderLevel+"','"+_Items.Size+"','" + _Items.CategoriesId + "')";
            return _MainRepository.ExecuteNonQuery(query, _MainRepository.ConnectionString());
        }
        public int Update(Categories _Categories)
        {
            string query = "Update Categories SET Name='" + _Categories.Name + "' where Code='" + _Categories.Code + "' ";
            return _MainRepository.ExecuteNonQuery(query, _MainRepository.ConnectionString());
        }

        public int Delete(Items _Items)
        {
            string query = ("Delete From Items Where Code='" + _Items.Code + "' ");
            return _MainRepository.ExecuteNonQuery(query, _MainRepository.ConnectionString());
        }
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
            string query = ("Select *From Size Where CategoriesId='"+Id+"'");
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
        public List<Items> GetAllItems(int CategoriesId,string Size)
        {
            var _ItemsList = new List<Items>();
            string query = ("Select *From Items Where Size='"+ Size + "' And CategoriesId='"+ CategoriesId + "' ");
            var reader = _MainRepository.Reader(query, _MainRepository.ConnectionString());
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var _Items = new Items();
                    _Items.Code = (reader["Code"].ToString());
                    _Items.Name = reader["Name"].ToString();
                    _Items.ReorderLevel = Convert.ToInt32(reader["ReorderLevel"].ToString());
                    _Items.Size = reader["Size"].ToString();

                    _ItemsList.Add(_Items);
                }
            }
            reader.Close();

            return _ItemsList;
        }
    }
}
