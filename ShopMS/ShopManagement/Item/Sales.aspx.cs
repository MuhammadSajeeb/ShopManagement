using Shop.Core.Models;
using Shop.Persistancis.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShopManagement.Item
{
    public partial class Sales : System.Web.UI.Page
    {
        ItemSalesRepository _ItemSalesRepository = new ItemSalesRepository();

        string Item;
        decimal Qty=1,Unit;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                     
            }
        
        }

        public void SaleOrder()
        {
            try
            {
                string code = txtItemcode.Text;
                var getData = _ItemSalesRepository.GetDataByCode(code);
                if (getData != null)
                {
                 
                    Item = getData.ItemName.ToString();
                    Unit = Convert.ToDecimal(getData.Mrp);

                    DataTable dataTable = new DataTable();

                    dataTable.Columns.Add("Item", typeof(string));
                    dataTable.Columns.Add("Qty", typeof(decimal));
                    dataTable.Columns.Add("Unit", typeof(decimal));
                    dataTable.Columns.Add("Total", typeof(decimal));

                    DataRow dr = null;
                    var data = (DataTable)ViewState["Details"];

                    if (data != null)
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            dataTable = (DataTable)ViewState["Details"];
                            if (dataTable.Rows.Count > 0)
                            {
                                dr = dataTable.NewRow();
                                dr["Item"] = Item;
                                dr["Qty"] = Qty;
                                dr["Unit"] = Unit;
                                dr["Total"] = Convert.ToDecimal(dr["Qty"]) * Convert.ToDecimal(dr["Unit"]);
                                dataTable.Rows.Add(dr);

                                ItemSalesGridView.DataSource = dataTable;
                                ItemSalesGridView.DataBind();
                            }
                            else
                            {
                                dr = dataTable.NewRow();
                                dr["Item"] = Item;
                                dr["Qty"] = Qty;
                                dr["Unit"] = Unit;
                                dr["Total"] = Convert.ToDecimal(dr["Qty"]) * Convert.ToDecimal(dr["Unit"]);
                                dataTable.Rows.Add(dr);

                                ItemSalesGridView.DataSource = dataTable;
                                ItemSalesGridView.DataBind();

                            }
                        }
                    }
                    else
                    {
                        dr = dataTable.NewRow();
                        dr["Item"] = Item;
                        dr["Qty"] = Qty;
                        dr["Unit"] = Unit;
                        dr["Total"] = Convert.ToDecimal(dr["Qty"]) * Convert.ToDecimal(dr["Unit"]);
                        dataTable.Rows.Add(dr);

                        ItemSalesGridView.DataSource = dataTable;
                        ItemSalesGridView.DataBind();
                    }
                    ViewState["Details"] = dataTable;

                }
                else
                {}

            }
            catch
            {
            }
        }
        public void AddSalesOrder()
        {


        }
        protected void ItemSalesGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void ItemSalesGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void ItemSalesGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void ItemSalesGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void txtItemcode_TextChanged(object sender, EventArgs e)
        {
            SaleOrder();
        }
    }
}