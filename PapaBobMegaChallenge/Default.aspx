<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PapaBobMegaChallenge.Default" %>

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
                <h1>Papa Bob's Pizza</h1>
                <p class="lead">Pizza to Code By
                </p>
            </div>

            <div class="form-group">
                <label>Size:</label>
              <asp:DropDownList ID="sizeDropDownList" runat="server" CssClass="form-control" OnSelectedIndexChanged="Input_Parameters_Changed" AutoPostBack="True">
                    <asp:ListItem Text="Small (12inch-$12)" Value="Small" />
                    <asp:ListItem Text="Medium (14inch-$14)" Value="Medium" />
                    <asp:ListItem Text="Large (16inch-$16)" Value="Large" />
              </asp:DropDownList>
            </div>

            <div class="form-group">
                <label>Crust:</label>
              <asp:DropDownList ID="crustDropDownList" runat="server" CssClass="form-control" OnSelectedIndexChanged="Input_Parameters_Changed" AutoPostBack="True">
                    <asp:ListItem Text="Regular" Value="Regular" />
                    <asp:ListItem Text="Thin" Value="Thin" />
                    <asp:ListItem Text="Thick (+$2)" Value="Thick" />
              </asp:DropDownList>
            </div>


            <div class="checkbox"><label><asp:CheckBox ID="sausageCheckBox" runat="server" OnCheckedChanged="Input_Parameters_Changed" AutoPostBack="True"></asp:CheckBox> Sausage(+$2)</label></div>
            <div class="checkbox"><label><asp:CheckBox ID="pepperoniCheckBox" runat="server" OnCheckedChanged="Input_Parameters_Changed" AutoPostBack="True"></asp:CheckBox> Pepperoni(+$1.50)</label></div>
            <div class="checkbox"><label><asp:CheckBox ID="onionsCheckBox" runat="server" OnCheckedChanged="Input_Parameters_Changed" AutoPostBack="True"></asp:CheckBox> Onions(+$1)</label></div>
            <div class="checkbox"><label><asp:CheckBox ID="greenPeppersCheckBox" runat="server" OnCheckedChanged="Input_Parameters_Changed" AutoPostBack="True"></asp:CheckBox> Green Peppers(+$1)</label></div>

            <p><br /></p>


          <h3>Deliver To:</h3>

          <div class="form-group">
            <label>Name:</label>
            <asp:TextBox CssClass="form-control" id="nameTextBox" runat="server"></asp:TextBox>
          </div>

          <div class="form-group">
            <label>Address:</label>
            <asp:TextBox CssClass="form-control" id="addressTextBox" runat="server"></asp:TextBox>
          </div>

          <div class="form-group">
            <label>Zip:</label>
            <asp:TextBox CssClass="form-control" id="zipTextBox" runat="server"></asp:TextBox>
          </div>

          <div class="form-group">
            <label>Phone:</label>
            <asp:TextBox CssClass="form-control" id="phoneTextBox" runat="server"></asp:TextBox>
          </div>

            <p><br /></p>


          <h3>Payment:</h3>
            <div class="radio"><label><asp:RadioButton ID="cashRadioButton" runat="server" GroupName="PaymentOptions" Checked="True"></asp:RadioButton> Cash</label></div>
            <div class="radio"><label><asp:RadioButton ID="creditRadioButton" runat="server" GroupName="PaymentOptions"></asp:RadioButton> Credit</label></div>

            <p><br /></p>

            <asp:Button ID="orderButton" runat="server" Text="Order" CssClass="btn btn-group-lg btn-primary" OnClick="OrderButton_Click" />

            <p>
                <asp:Label ID="errorLabel" runat="server"></asp:Label>
                <br /></p>
          <h3>Total Cost:<asp:Label ID="resultLabel" runat="server"></asp:Label>
            </h3>

            <asp:Button ID="mgtButtom" runat="server" Text="Management" CssClass="btn btn-group-lg btn-primary" OnClick="mgtButtom_Click" />


        </div>
    </form>
    <script src="Scripts/jquery-3.0.0.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

</body>
</html>
