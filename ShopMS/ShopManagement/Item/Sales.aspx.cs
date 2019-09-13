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
        decimal Qty=1,Unit,total=0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDiscount.Text = "0";
                AutoGenerateInvoice();
            }
        
        }
        public void AutoGenerateInvoice()
        {
            decimal AlreadyExistData = _ItemSalesRepository.AlreadyExistData();
            int Invoice = 1;
            if (AlreadyExistData >= 1)
            {
                var GetLastInvoice = _ItemSalesRepository.GetLastInvoice();
                if (GetLastInvoice != null)
                {
                    Invoice = Convert.ToInt32(GetLastInvoice.Invoice);
                    Invoice++;
                }
                txtInvoice.Text = Invoice.ToString();
            }
            else
            {
                txtInvoice.Text = "1";
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
                    txtItemcode.Focus();
                }
                else
                {}

            }
            catch
            {
            }
        }
        protected void ItemSalesGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lbltotal = e.Row.FindControl("lblTotal") as Label;
                if (lbltotal != null)
                {
                    total += Convert.ToDecimal(lbltotal.Text);
                }
            }
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

            txtGrandTotal.Text = total.ToString();
            txtPayableAmount.Text = total.ToString();
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

                txtGrandTotal.Text = total.ToString();
                txtPayableAmount.Text = total.ToString();

            }
        }

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal grandtotal = Convert.ToDecimal(txtGrandTotal.Text);
                decimal discount = Convert.ToDecimal(txtDiscount.Text);

                decimal cal = discount / 100;
                decimal discountAmount = cal * grandtotal;
                decimal payableamount = grandtotal - discountAmount;

                txtPayableAmount.Text =(payableamount).ToString("00.00");
                txtPaidAmount.Focus();
            }
            catch
            {

            }
        }

        protected void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal PayableAount = Convert.ToDecimal(txtPayableAmount.Text);
                decimal PaidAmount = Convert.ToDecimal(txtPaidAmount.Text);

                decimal changes = PaidAmount - PayableAount;
                txtChanges.Text = (changes).ToString("00.00");
            }
            catch
            {

            }
        }

        protected void txtItemcode_TextChanged(object sender, EventArgs e)
        {
            SaleOrder();

            txtGrandTotal.Text = total.ToString();
            txtPayableAmount.Text = total.ToString();
        }
    }
}