using Shop.Core.Models;
using Shop.Persistancis.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShopManagement.Item
{
    public partial class Setup : System.Web.UI.Page
    {
        ItemRepository _ItemRepository = new ItemRepository();
        public enum MessageType { Success, Failed, Error, Info, Warning };
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                AutoCodeGenerate();
                GetAllCategories();
                SizeDropDownList.Items.Insert(0, new ListItem("Chose Size", "0"));
            }
        }
        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        public void AutoCodeGenerate()
        {
            decimal AlreadyExistData = _ItemRepository.AlreadyExistData();
            int code = 1;
            if (AlreadyExistData >= 1)
            {
                var GetLastCode = _ItemRepository.GetLastCode();
                if (GetLastCode != null)
                {
                    code = Convert.ToInt32(GetLastCode.Code);
                    code++;
                }
                txtCode.Text = code.ToString("0000");
            }
            else
            {
                txtCode.Text = "1001";
            }
        }
        public void GetAllCategories()
        {
            CategoriesDropDownList.DataSource = _ItemRepository.GetAllCategories();
            CategoriesDropDownList.DataTextField = "Name";
            CategoriesDropDownList.DataValueField = "Id";
            CategoriesDropDownList.DataBind();

            CategoriesDropDownList.Items.Insert(0, new ListItem("Chose Category", "0"));
        }
        public void GetSizeByCategories()
        {
            int Id = Convert.ToInt32(CategoriesDropDownList.SelectedValue);
            SizeDropDownList.DataSource = _ItemRepository.GetSizeByCategories(Id);
            SizeDropDownList.DataTextField = "Size";
            SizeDropDownList.DataValueField = "Id";
            SizeDropDownList.DataBind();

            SizeDropDownList.Items.Insert(0, new ListItem("Chose Size", "0"));
        }
        public void LoadItem()
        {
            try
            {
                int Id = Convert.ToInt32(CategoriesDropDownList.SelectedValue);
                string size = SizeDropDownList.SelectedItem.ToString();
                ItemsGridView.DataSource = _ItemRepository.GetAllItems(Id,size);
                ItemsGridView.DataBind();
            }
            catch { }
        }
        protected void CategoriesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSizeByCategories();
            
        }
        protected void SizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItem();
        }
        protected void AddButton_Click(object sender, EventArgs e)
        {
            Items _Items = new Items();
            _Items.Code = txtCode.Text;
            _Items.Name = txtName.Text;
            _Items.ReorderLevel = 5;
            _Items.Size = SizeDropDownList.SelectedItem.ToString();
            _Items.CategoriesId = Convert.ToInt32(CategoriesDropDownList.SelectedValue);

            if(CategoriesDropDownList.SelectedIndex>0)
            {
                if(SizeDropDownList.SelectedIndex>0)
                {
                    decimal AlreadyExistCaegory = _ItemRepository.AlreadyExistName(_Items);
                    if (AlreadyExistCaegory >= 1)
                    {
                        ShowMessage("This Item Already Here!!!...", MessageType.Warning);
                    }
                    else
                    {
                        int Savesuccess = _ItemRepository.Add(_Items);
                        if (Savesuccess > 0)
                        {

                            ShowMessage("Successfully Saved Item....", MessageType.Success);
                            LoadItem();
                            AutoCodeGenerate();
                            GetAllCategories();
                            txtName.Text = "";
                            CategoriesDropDownList.ClearSelection();
                            SizeDropDownList.ClearSelection();
                            //Response.Redirect(Request.Url.AbsoluteUri);


                        }
                        else
                        {
                            ShowMessage("Failed Saving Item", MessageType.Warning);
                        }
                    }
                }
                else
                {
                    ShowMessage("Select Size", MessageType.Warning);
                    SizeDropDownList.Focus();
                }

            }
            else
            {
                ShowMessage("At First Select Catgeory", MessageType.Warning);
                CategoriesDropDownList.Focus();
            }
        }

        protected void ItemsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Items _Items = new Items();
                _Items.Code = (e.CommandArgument).ToString();

                int deletesuccess = _ItemRepository.Delete(_Items);
                if (deletesuccess > 0)
                {
                    ShowMessage("Successfully Delete Item....", MessageType.Success);

                    LoadItem();
                    AutoCodeGenerate();
                    GetAllCategories();

                }

            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, MessageType.Error);
            }
        }


    }
}