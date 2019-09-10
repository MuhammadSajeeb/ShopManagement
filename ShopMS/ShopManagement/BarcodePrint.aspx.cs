using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.Data;
using Shop.Persistancis.Repositories;

namespace ShopManagement
{
    public partial class BarcodePrint : System.Web.UI.Page
    {
        ReportDocument crystal = new ReportDocument();

        StocksInRepository _StocksInRepository = new StocksInRepository();
         
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Convert.ToInt32(Request.QueryString["id"]) != 0)
                {
                    //GetQtyByCode();
                    string id = (Request.QueryString["id"]);
                    int qty = Convert.ToInt32(Request.QueryString["qty"]);

                    crystal.Load(@"F:\Web Form Project\ShopManagement\ShopMS\ShopManagement\Barcode.rpt");

                    SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=ShopMsDb;Integrated Security=True");

                    string sql = "Select *from StockIn where Id='" + id + "'";

                    for (int i = 1; i < qty; i++)
                    {
                        sql = sql + "Union All Select *from StockIn  where Id='" + id + "'";
                    }

                    SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                    DataSet ds = new DataSet();
                    sda.Fill(ds,"StockIn");
                    crystal.SetDataSource(ds);
                    CrystalReportViewer1.ReportSource = crystal;
                }
                
            }
            
        }
        //public void GetQtyByCode()
        //{
        //    code = (Request.QueryString["code"]);

        //    var Data = _StocksInRepository.GetQtyByCode(code);
        //    if (Data != null)
        //    {
        //        Qty = Data.Quantity;
 

        //    }
        //    else
        //    {
        //    }

        //}
    }
}