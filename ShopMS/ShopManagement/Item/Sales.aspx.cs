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
                    txtItemcode.Text = "";
                }
                else
                {}

            }
            catch
            {
            }
        }
        private void BindGridView()
        {
            //Declare a datatable for the gridview
            DataTable dt = new DataTable();

            //Add Columns to the datatable
            dt.Columns.Add("Item", typeof(string));
            dt.Columns.Add("Qty", typeof(decimal));
            dt.Columns.Add("Unit", typeof(decimal));
            dt.Columns.Add("Total", typeof(decimal));



            //Define a datarow for the datatable dt
            DataRow dr = dt.NewRow();


            //Now add the datarow to the datatable
            dt.Rows.Add(dr);

            //Now bind the datatable to gridview
            ItemSalesGridView.DataSource = dt;
            ItemSalesGridView.DataBind();

            //Now hide the extra row of the grid view
            ItemSalesGridView.Rows[0].Visible = false;

            //Delete row 0 from the datatable
            dt.Rows[0].Delete();
            dt.AcceptChanges();

            //View the datatable to the viewstate
            ViewState["Details"] = dt;

        }
        protected void ItemSalesGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Findout the controls inside the gridview
            TextBox txtQty = (TextBox)ItemSalesGridView.Rows[e.RowIndex].Cells[2].FindControl("txtEQty");
            TextBox txtUnit = (TextBox)ItemSalesGridView.Rows[e.RowIndex].Cells[3].FindControl("txtEUnit");
            TextBox txtTotal = (TextBox)ItemSalesGridView.Rows[e.RowIndex].Cells[4].FindControl("txtETotal");
            //TextBox txtUnit = (TextBox)ItemSalesGridView.Rows[e.RowIndex].Cells[3].FindControl("txtEDOB");

            //Assign the ViewState to the datatable
            DataRow dr = ((DataTable)ViewState["Details"]).Rows[e.RowIndex];

            dr.BeginEdit();

            dr["Qty"] = txtQty.Text;
            dr["Unit"] = txtUnit.Text;
            dr["Total"] = Convert.ToDecimal(dr["Qty"]) * Convert.ToDecimal(dr["Unit"]);

            dr.EndEdit();

            dr.AcceptChanges();

            ItemSalesGridView.EditIndex = -1;

            //Now bind the datatable to the gridview
            ItemSalesGridView.DataSource = (DataTable)ViewState["Details"];
            ItemSalesGridView.DataBind();
        }

        protected void ItemSalesGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Change the gridview to edit mode
            ItemSalesGridView.EditIndex = e.NewEditIndex;

  
            //Now bind the gridview
            ItemSalesGridView.DataSource = (DataTable)ViewState["Details"];
            ItemSalesGridView.DataBind();
        }

        protected void ItemSalesGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ItemSalesGridView.EditIndex = -1;

            ItemSalesGridView.DataSource = (DataTable)ViewState["Details"];
            ItemSalesGridView.DataBind();
        }

        protected void ItemSalesGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dataTable = new DataTable();

            dataTable = (DataTable)ViewState["Details"];

            if (dataTable.Rows.Count > 0)
            {
                dataTable.Rows[e.RowIndex].Delete();

                ViewState["Details"] = dataTable;


                ItemSalesGridView.DataSource = dataTable;
                ItemSalesGridView.DataBind();

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ViewState["Details"] = null;
            ItemSalesGridView.DataSource = null;
            ItemSalesGridView.DataBind();
        }

        protected void txtItemcode_TextChanged(object sender, EventArgs e)
        {
            SaleOrder();
        }
    }
}