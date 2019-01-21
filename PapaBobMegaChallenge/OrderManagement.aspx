<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderManagement.aspx.cs" Inherits="PapaBobMegaChallenge.OrderManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="Content/bootstrap.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">

            <div class ="page-header">
                <h1>Order Management</h1>
                <p>
                    <asp:GridView ID="orderGridView" runat="server" AutoGenerateColumns="False" OnRowCommand="orderGridView_RowCommand">
                        <Columns>
                            <asp:ButtonField Text="Complete" />
                            <asp:BoundField DataField="order_id" HeaderText="Order ID" />
                            <asp:BoundField DataField="size" HeaderText="Size" />
                            <asp:BoundField DataField="crust" HeaderText="Crust" />
                            <asp:CheckBoxField DataField="sausage" HeaderText="Sausage" />
                            <asp:CheckBoxField DataField="onions" HeaderText="Onions" />
                            <asp:CheckBoxField DataField="pepperoni" HeaderText="Pepperoni" />
                            <asp:CheckBoxField DataField="green_peppers" HeaderText="Green Peppers" />
                            <asp:BoundField DataField="cost" HeaderText="Total Cost" />
                            <asp:BoundField DataField="Customer.Name" HeaderText="Name" />
                        </Columns>
                    </asp:GridView>
                </p>
            </div>
            <asp:Button ID="backButton" runat="server" Text="Back" CssClass="btn btn-group-lg btn-primary" OnClick="backButton_Click"  />

        </div>
    </form>
    <script src="Scripts/jquery-3.0.0.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</body>
</html>