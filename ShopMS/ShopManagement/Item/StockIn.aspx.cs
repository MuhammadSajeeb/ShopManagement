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
    public partial class StockIn : System.Web.UI.Page
    {
        StocksInRepository _StocksInRepository = new StocksInRepository();
        public enum MessageType { Success, Failed, Error, Info, Warning };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAllCategories();
                SizeDropDownList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Available No Size", "0"));
                ItemsDropDownList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Available No Items", "0"));
                StockGridView.Columns[1].Visible = true;

            }
        }
        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }
        public void GetAllCategories()
        {
            CategoriesDropDownList.DataSource = _StocksInRepository.GetAllCategories();
            CategoriesDropDownList.DataTextField = "Name";
            CategoriesDropDownList.DataValueField = "Id";
            CategoriesDropDownList.DataBind();

            CategoriesDropDownList.Items.Insert(0, new ListItem("Chose Category", "0"));
        }
        public void LoadStocks()
        {
            
            string size = SizeDropDownList.SelectedItem.ToString();
            string code = txtCode.Text;
            int id = Convert.ToInt32(CategoriesDropDownList.SelectedValue);

            StockGridView.DataSource = _StocksInRepository.GetAllStocks(size,code,id);
            StockGridView.DataBind();
        }
        public void Refresh()
        {
            txtCode.Text = "";
            txtQty.Text = "";
            txtCost.Text = "";
            txtMrp.Text = "";
            CategoriesDropDownList.ClearSelection();
            SizeDropDownList.ClearSelection();
            ItemsDropDownList.ClearSelection();
        }
        protected void CategoriesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int Id = Convert.ToInt32(CategoriesDropDownList.SelectedValue);
                 
                SizeDropDownList.DataSource = _StocksInRepository.GetSizeByCategories(Id);
                SizeDropDownList.DataTextField = "Size";
                SizeDropDownList.DataValueField = "Id";
                SizeDropDownList.DataBind();

                SizeDropDownList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Size", "0"));

                txtCode.Text = "";
            }
            catch
            {

            }
        }
        protected void SizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int Id = Convert.ToInt32(CategoriesDropDownList.SelectedValue);
                string Size = SizeDropDownList.SelectedItem.ToString();
                ItemsDropDownList.DataSource = _StocksInRepository.GetItemsByCtgndSze(Id, Size);
                ItemsDropDownList.DataTextField = "Name";
                ItemsDropDownList.DataValueField = "Id";
                ItemsDropDownList.DataBind();

                ItemsDropDownList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Items", "0"));

                txtCode.Text = "";
            }
            catch
            {

            }
        }

        protected void ItemsDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int Id = Convert.ToInt32(ItemsDropDownList.SelectedValue);
                var getData = _StocksInRepository.GetItemData(Id);
                if (getData != null)
                {
                    txtCode.Text = (getData.Code).ToString();
                    LoadStocks();
                }
                
            }
            catch
            {
            }
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(CategoriesDropDownList.SelectedIndex>0)
                {
                    StocksIn _StocksIn = new StocksIn();
                    _StocksIn.Quantity = Convert.ToInt32(txtQty.Text);
                    _StocksIn.Size = SizeDropDownList.SelectedItem.ToString();
                    _StocksIn.UnitCost = Convert.ToDecimal(txtCost.Text);
                    _StocksIn.Mrp = Convert.ToDecimal(txtMrp.Text);
                    _StocksIn.ItemName = ItemsDropDownList.SelectedItem.ToString();
                    _StocksIn.ItemCode = (txtCode.Text);
                    _StocksIn.CategoriesId = Convert.ToInt32(CategoriesDropDownList.SelectedValue);

                    int SaveSuccess = _StocksInRepository.Add(_StocksIn);
                    if (SaveSuccess > 0)
                    {
                        ShowMessage("Successfully Add Stock....", MessageType.Success);
                        LoadStocks();
                        GetAllCategories();
                        Refresh();
                        //Response.Redirect(Request.Url.AbsoluteUri);
                    }
                    else
                    {
                        ShowMessage("Failed Stock Input....", MessageType.Success);
                    }
                }


            }
            catch(Exception ex)
            {
                ShowMessage(ex.Message, MessageType.Success);
            }
        }

        protected void StockGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void StockGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            StockGridView.Columns[1].Visible = true;

            string id = (StockGridView.SelectedRow.Cells[1].Text);
            string qty= (StockGridView.SelectedRow.Cells[3].Text);

            Response.Redirect("~/BarcodesPrint.aspx?id=" + id+"&qty="+qty);
             
        }
    }
}