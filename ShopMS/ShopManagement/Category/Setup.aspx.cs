using Shop.Core.Models;
using Shop.Persistancis.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShopManagement.Category
{
    public partial class Setup : System.Web.UI.Page
    {
        CategoriesRepository _CategoriesRepository = new CategoriesRepository();
        public enum MessageType { Success, Failed, Error, Info, Warning };
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                AutoCodeGenerate();
                LoadCategories();
            }
        }
        public void AutoCodeGenerate()
        {
            decimal AlreadyExistData = _CategoriesRepository.AlreadyExistData();
            int code = 1;
            if (AlreadyExistData >= 1)
            {
                var GetLastCode = _CategoriesRepository.GetLastCode();
                if (GetLastCode != null)
                {
                    code = Convert.ToInt32(GetLastCode.Code);
                    code++;
                }
                txtCode.Text = code.ToString("000");
            }
            else
            {
                txtCode.Text = "001";
            }
        }
        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        public void LoadCategories()
        {
            CategoriesGridView.DataSource = _CategoriesRepository.GetAll();
            CategoriesGridView.DataBind();
        }
        protected void AddCategoriesButton_Click(object sender, EventArgs e)
        {
            try
            {
                Categories _Categories = new Categories();
                _Categories.Code = txtCode.Text;
                _Categories.Name = txtName.Text;

                decimal AlreadyExistCaegory = _CategoriesRepository.AlreadyExistName(_Categories);
                if (AlreadyExistCaegory >= 1)
                {
                    ShowMessage("This Category Already Here!!!...", MessageType.Warning);
                }
                else
                {
                    int Savesuccess = _CategoriesRepository.Add(_Categories);
                    if (Savesuccess > 0)
                    {

                        ShowMessage("Successfully Saved Category....", MessageType.Success);
                        LoadCategories();
                        AutoCodeGenerate();
                        txtName.Text = "";
                        //Response.Redirect(Request.Url.AbsoluteUri);


                    }
                    else
                    {
                        ShowMessage("Failed Saving Category", MessageType.Warning);
                    }

                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, MessageType.Error);
            }
        }

        protected void CategoriesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Categories _Categories = new Categories();
                _Categories.Code = (e.CommandArgument).ToString();

                int deletesuccess = _CategoriesRepository.Delete(_Categories);
                if (deletesuccess > 0)
                {
                    ShowMessage("Successfully Delete Category....", MessageType.Success);

                    LoadCategories();
                    AutoCodeGenerate();
                    
                }

            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, MessageType.Error);
            }
        }

        protected void CategoriesGridView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}