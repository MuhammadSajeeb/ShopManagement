<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockIn.aspx.cs" Inherits="ShopManagement.Item.StockIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h4>Stock In</h4>
    <div class="form-horizontal">
        <div class="form-group">
            <asp:Label runat="server" ID="lblMessage" AssociatedControlID="lblMessage" CssClass="col-md-1 control-label"></asp:Label>
            <div class="col-md-10">
                <div class="messagealert" id="alert_container">
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-offset-1 col-md-5">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">Item Information</h3>
                    </div>
                    <br />
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="col-md-offset-1 col-md-5">
                                <asp:Label runat="server" ID="lblCategories" AssociatedControlID="CategoriesDropDownList" CssClass="control-label">Categories</asp:Label>
                                <asp:DropDownList ID="CategoriesDropDownList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="CategoriesDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="CategoriesDropDownList"
                                    CssClass="text-danger" ErrorMessage="This field is required." />
                            </div>
                            <div class="col-md-5">
                                <asp:Label runat="server" ID="lblSize" AssociatedControlID="SizeDropDownList" CssClass="control-label">Size</asp:Label>
                                <asp:DropDownList ID="SizeDropDownList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="SizeDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="SizeDropDownList"
                                    CssClass="text-danger" ErrorMessage="This field is required." />
                            </div>
                            <div class="col-md-offset-1 col-md-5">
                                <asp:Label runat="server" ID="lblItems" AssociatedControlID="ItemsDropDownList" CssClass="control-label">Items</asp:Label>
                                <asp:DropDownList ID="ItemsDropDownList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ItemsDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ItemsDropDownList"
                                    CssClass="text-danger" ErrorMessage="This field is required." />
                            </div>
                            <div class="col-md-5">
                                <asp:Label runat="server" ID="lblCode" AssociatedControlID="txtCode" CssClass="control-label">Code</asp:Label>
                                <asp:TextBox runat="server" ID="txtCode" CssClass="form-control" Font-Bold="true" Font-Size="Medium" Style="text-align: center" ReadOnly="true" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCode"
                                    CssClass="text-danger" ErrorMessage="This field is required." />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-5" style="width: 500px">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">Stock Input</h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="col-md-offset-1 col-md-5">
                                <asp:Label runat="server" ID="lblQty" AssociatedControlID="txtQty" CssClass="control-label">Quantity</asp:Label>
                                <asp:TextBox runat="server" ID="txtQty" CssClass="form-control" Font-Bold="true" Font-Size="Medium" Style="text-align: center" TextMode="Number" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtQty"
                                    CssClass="text-danger" ErrorMessage="This field is required." />
                            </div>
                            <div class="col-md-5">
                                <asp:Label runat="server" ID="lblCost" AssociatedControlID="txtCost" CssClass="control-label">Unit Cost</asp:Label>
                                <asp:TextBox runat="server" ID="txtCost" CssClass="form-control" Font-Bold="true" Font-Size="Medium" Style="text-align: center" TextMode="Number" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCost"
                                    CssClass="text-danger" ErrorMessage="This field is required." />
                            </div>
                            <div class="col-md-offset-1 col-md-5">
                                <asp:Label runat="server" ID="lblMerp" AssociatedControlID="txtMrp" CssClass="control-label">MRP(Tk)</asp:Label>
                                <asp:TextBox runat="server" ID="txtMrp" CssClass="form-control" Font-Bold="true" Font-Size="Medium" Style="text-align: center" TextMode="Number" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMrp"
                                    CssClass="text-danger" ErrorMessage="This field is required." />
                            </div>
                            <div class="col-md-5"">
                                <br />
                                <asp:Button ID="AddButton" runat="server" Text="Add" CssClass="btn btn-info" Width="85px" OnClick="AddButton_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="form-group">
            <div class="col-md-offset-1 col-md-10">
                <div style="width: 100%; height: 200px; overflow: scroll">
                    <asp:GridView ID="StockGridView" runat="server" EmptyDataText="No Stocks Available Now" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="10" ForeColor="Black" GridLines="Horizontal" AllowPaging="false" CellSpacing="10" OnRowCommand="StockGridView_RowCommand" OnSelectedIndexChanged="StockGridView_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnDelete" CommandArgument='<%# Eval("ItemCode") %>' CommandName="DeleteRow" ForeColor="#8C4510" runat="server" CausesValidation="false">Delete</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id" HeaderText="Id"/>
                            <asp:BoundField DataField="ItemCode" HeaderText="Code" />
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                            <asp:BoundField DataField="UnitCost" HeaderText="Unit Cost" />
                            <asp:BoundField DataField="Mrp" HeaderText="Mrp(Tk)" />
                            <asp:BoundField DataField="Date" HeaderText="Date" />
                            <asp:CommandField HeaderText="Barcode" SelectText="Print" ShowSelectButton="True">
                                <ItemStyle ForeColor="#CC0000" />
                            </asp:CommandField>
                        </Columns>
                        <PagerStyle Font-Bold="true" Font-Size="Small" ForeColor="#3399FF" />
                    </asp:GridView>
                </div>
            </div>
        </div>
                    </div>
    <style type="text/css">
        .messagealert {
            width: 470px;
        }
    </style>
    <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Failed':
                    cssclass = 'alert-danger'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');
        }
    </script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.jquery.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/chosen/1.8.7/chosen.min.css" rel="stylesheet" />
    <script>
        $('#<%=CategoriesDropDownList.ClientID%>').chosen()
        $('#<%=SizeDropDownList.ClientID%>').chosen()
        $('#<%=ItemsDropDownList.ClientID%>').chosen()
    </script>
    <link href="../Content/Gridviewstylesheet.css" rel="stylesheet" />
</asp:Content>
