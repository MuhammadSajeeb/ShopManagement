﻿using Shop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistancis.Repositories
{
    public class CategoriesRepository
    {
        MainRepository _MainRepository = new MainRepository();
        public decimal AlreadyExistData()
        {
            string query = "Select Count(*)from Categories ";
            return _MainRepository.ExecuteScalar(query, _MainRepository.ConnectionString());
        }
        public Categories GetLastCode()
        {
            Categories _Categories = null;

            string query = "Select top 1 Code from Categories order by Code desc";
            var reader = _MainRepository.Reader(query, _MainRepository.ConnectionString());
            if (reader.HasRows)
            {
                reader.Read();
                _Categories = new Categories();
                _Categories.Code = (reader["Code"].ToString());
            }
            reader.Close();

            return _Categories;
        }

        public decimal AlreadyExistName(Categories _Categories)
        {
            string query = "Select Count(*)from Categories Where Name='" + _Categories.Name + "' ";
            return _MainRepository.ExecuteScalar(query, _MainRepository.ConnectionString());
        }
        public int Add(Categories _Categories)
        {
            string query = "Insert Into Categories(Code,Name) Values ('"+_Categories.Code+"','" + _Categories.Name + "')";
            return _MainRepository.ExecuteNonQuery(query, _MainRepository.ConnectionString());
        }
        public int Update(Categories _Categories)
        {
            string query = "Update Categories SET Name='" + _Categories.Name + "' where Code='" + _Categories.Code + "' ";
            return _MainRepository.ExecuteNonQuery(query, _MainRepository.ConnectionString());
        }

        public int Delete(Categories _Categories)
        {
            string query = ("Delete From Categories Where Code='" + _Categories.Code + "' ");
            return _MainRepository.ExecuteNonQuery(query, _MainRepository.ConnectionString());
        }
        public List<Categories> GetAll()
        {
            var _CategoryList = new List<Categories>();
            string query = ("Select *From Categories");
            var reader = _MainRepository.Reader(query, _MainRepository.ConnectionString());
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var _Category = new Categories();
                    _Category.Code = (reader["Code"].ToString());
                    _Category.Name = reader["Name"].ToString();

                    _CategoryList.Add(_Category);
                }
            }
            reader.Close();

            return _CategoryList;
        }
    }
}
