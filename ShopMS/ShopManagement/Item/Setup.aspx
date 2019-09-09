<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Setup.aspx.cs" Inherits="ShopManagement.Item.Setup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="ItemUpdatePanel" runat="server">
        <ContentTemplate>
            <br />
            <h4>Category Setup</h4>
            <br />
            <div class="form-horizontal">
                <div class="form-group">
                    <asp:Label runat="server" ID="lblMessage" AssociatedControlID="lblMessage" CssClass="col-md-1 control-label"></asp:Label>
                    <div class="col-md-10">
                        <div class="messagealert" id="alert_container">
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-offset-1 col-md-5" style="width: 500px">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">Item Add</h3>
                            </div>
                            <br />
                            <div class="panel-body">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblCode" AssociatedControlID="txtCode" CssClass="col-md-2 control-label">Code</asp:Label>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server" ID="txtCode" CssClass="form-control" Font-Bold="true" Font-Size="Medium" Style="text-align: center" ReadOnly="true" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblCategories" AssociatedControlID="CategoriesDropDownList" CssClass="col-md-2 control-label">Categories</asp:Label>
                                    <div class="col-md-10">
                                        <asp:DropDownList ID="CategoriesDropDownList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="CategoriesDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblSize" AssociatedControlID="SizeDropDownList" CssClass="col-md-2 control-label">Size</asp:Label>
                                    <div class="col-md-10">
                                        <asp:DropDownList ID="SizeDropDownList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="SizeDropDownList_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblName" AssociatedControlID="txtName" CssClass="col-md-2 control-label">Name</asp:Label>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server" ID="txtName" CssClass="form-control" Font-Bold="true" Font-Size="Medium" Style="text-align: center" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName"
                                            CssClass="text-danger" ErrorMessage="This field is required." />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-offset-7 col-md-12">
                                        <asp:Button ID="AddButton" runat="server" Text="Add" CssClass="btn btn-info" Width="85px" OnClick="AddButton_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5" style="width: 600px">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">Item Details</h3>
                            </div>
                            <div class="panel-body">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div style="width: 100%; height: 200px; overflow: scroll">
                                            <asp:GridView ID="ItemsGridView" runat="server" EmptyDataText="No Items Available Now" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="10" ForeColor="Black" GridLines="Horizontal" AllowPaging="false" CellSpacing="10" OnRowCommand="ItemsGridView_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnDelete" CommandArgument='<%# Eval("Code") %>' CommandName="DeleteRow" ForeColor="#8C4510" runat="server" CausesValidation="false">Delete</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Code" HeaderText="Code" />
                                                    <asp:BoundField DataField="Name" HeaderText="Item" />
                                                    <asp:BoundField DataField="ReorderLevel" HeaderText="Reorder"/>
                                                    <asp:BoundField DataField="Size" HeaderText="Size"/>
                                                    <asp:CommandField HeaderText="Action" SelectText="Edit" ShowSelectButton="True">
                                                        <ItemStyle ForeColor="#CC0000" />
                                                    </asp:CommandField>
                                                </Columns>
                                                <PagerStyle Font-Bold="true" Font-Size="Small" ForeColor="#3399FF" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
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
            </script>
            <link href="../Content/Gridviewstylesheet.css" rel="stylesheet" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
