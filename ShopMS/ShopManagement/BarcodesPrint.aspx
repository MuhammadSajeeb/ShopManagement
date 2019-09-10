<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BarcodesPrint.aspx.cs" Inherits="ShopManagement.BarcodesPrint" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h4>Item Barcode Print</h4>

    <div class="form-horizontal">

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <div style="width: 100%; height: 650px; overflow: scroll">

                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ReportSourceID="CrystalReportSource1" ToolPanelView="None" />
                </div>
            </div>
        </div>

        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="Barcode.rpt">
            </Report>
        </CR:CrystalReportSource>

    </div>
</asp:Content>
