<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Setup.aspx.cs" Inherits="ShopManagement.Category.Setup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="CategoryUpdatePanel" runat="server">
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
                                <h3 class="panel-title">Category Add</h3>
                            </div>
                            <br />
                            <br />
                            <div class="panel-body">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblCode" AssociatedControlID="txtCode" CssClass="col-md-2 control-label">Code</asp:Label>
                                    <div class="col-md-10">
                                        <asp:TextBox runat="server" ID="txtCode" CssClass="form-control" Font-Bold="true" Font-Size="Medium" Style="text-align: center" ReadOnly="true" />
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
                                        <asp:Button ID="AddCategoriesButton" runat="server" Text="Add" CssClass="btn btn-info" Width="85px" OnClick="AddCategoriesButton_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5" style="width: 600px">
                        <div class="panel panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">Category Details</h3>
                            </div>
                            <div class="panel-body">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div style="width: 100%; height: 200px; overflow: scroll">
                                            <asp:GridView ID="CategoriesGridView" runat="server" EmptyDataText="No Categories Available Now" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="10" ForeColor="Black" GridLines="Horizontal" AllowPaging="false" CellSpacing="10" OnRowCommand="CategoriesGridView_RowCommand" OnSelectedIndexChanged="CategoriesGridView_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnDelete" CommandArgument='<%# Eval("Code") %>' CommandName="DeleteRow" ForeColor="#8C4510" runat="server" CausesValidation="false">Delete</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Code" HeaderText="Code" />
                                                    <asp:BoundField DataField="Name" HeaderText="Name" />
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
            <link href="../Content/Gridviewstylesheet.css" rel="stylesheet" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
