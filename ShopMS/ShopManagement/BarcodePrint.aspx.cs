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
        string code;
        int Qty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Convert.ToInt32(Request.QueryString["code"]) != 0)
                {
                    GetQtyByCode();

                    crystal.Load(@"F:\Web Form Project\ShopManagement\ShopMS\ShopManagement\Barcode.rpt");

                    SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=ShopMsDb;Integrated Security=True");

                    string sql = "Select *from StockIn where ItemCode='" + code + "'";

                    for (int i = 1; i < Qty; i++)
                    {
                        sql = sql + "Union All Select *from StockIn  where ItemCode='" + code + "'";
                    }

                    SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    crystal.SetDataSource(dt);
                    CrystalReportViewer1.ReportSource = crystal;
                }
                
            }
            
        }
        public void GetQtyByCode()
        {
            code = (Request.QueryString["code"]);

            var Data = _StocksInRepository.GetQtyByCode(code);
            if (Data != null)
            {
                Qty = Data.Quantity;
 

            }
            else
            {
            }

        }
    }
}