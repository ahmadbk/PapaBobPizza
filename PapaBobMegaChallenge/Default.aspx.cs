using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PapaBobMegaChallenge
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DTO.Order current_order = new DTO.Order();
                ViewState.Add("current_order", current_order);
            }
        }

        protected void Input_Parameters_Changed(object sender, EventArgs e)
        {
            DTO.Order current_order = (DTO.Order)ViewState["current_order"];

            current_order.size = GetSize();
            current_order.crust = GetCrust();
            CheckForToppings(current_order);

            resultLabel.Text = string.Format("R{0}", Domain.OrderManager.CalculateAmountOwing(current_order).ToString());

            ViewState.Add("current_order", current_order);
        }

        private DTO.Size GetSize()
        {
            int value = sizeDropDownList.SelectedIndex;
            return (DTO.Size)Enum.Parse(typeof(DTO.Size), value.ToString());
        }

        private DTO.Crust GetCrust()
        {
            int value = crustDropDownList.SelectedIndex;
            return (DTO.Crust)Enum.Parse(typeof(DTO.Crust), value.ToString());
        }

        private DTO.Payment GetPaymentType()
        {
            DTO.Payment paymentType;
            if(cashRadioButton.Checked)
            {
                paymentType = DTO.Payment.Cash;
            }
            else
            {
                paymentType = DTO.Payment.Credit;
            }
            return paymentType;
        }

        private void CheckForToppings(DTO.Order current_order)
        {
            current_order.onions = onionsCheckBox.Checked;
            current_order.sausage = sausageCheckBox.Checked;
            current_order.pepperoni = pepperoniCheckBox.Checked;
            current_order.green_peppers = greenPeppersCheckBox.Checked;
        }

        protected void OrderButton_Click(object sender, EventArgs e)
        {
            //obtain personal details
            DTO.Customer current_customer = ObtainCustomer();

            if (current_customer == null)
                return;

            DTO.Order current_order = (DTO.Order)ViewState["current_order"];
            current_order.cost = Domain.OrderManager.CalculateAmountOwing(current_order);
            current_order.payment_type = GetPaymentType();

            try
            {
                Domain.OrderManager.AddOrder(current_order, current_customer);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Response.Redirect("Success.aspx");

        }

        public int ValidateCustomerInfo()
        {
            if (nameTextBox.Text.Trim().Length == 0)
                return 1;
            if (addressTextBox.Text.Trim().Length == 0)
                return 2;
            if (zipTextBox.Text.Trim().Length == 0 || !zipTextBox.Text.Any(char.IsDigit))
                return 3;
            if (phoneTextBox.Text.Trim().Length == 0 || !phoneTextBox.Text.Any(char.IsDigit))
                return 4;

            return 0;
        }

        public bool InputValidation()
        {
            if (ValidateCustomerInfo() == 1)
            {
                errorLabel.Text = "Please enter a valid Name";
                return false;
            }
            if (ValidateCustomerInfo() == 2)
            {
                errorLabel.Text = "Please enter a valid Address";
                return false;
            }
            if (ValidateCustomerInfo() == 3)
            {
                errorLabel.Text = "Please enter a valid Zip Code";
                return false;
            }
            if (ValidateCustomerInfo() == 4)
            {
                errorLabel.Text = "Please enter a valid Phone Number";
                return false;
            }

            return true;
        }

        private DTO.Customer ObtainCustomer()
        {
            DTO.Customer current_customer = new DTO.Customer();

            if(InputValidation())
            {
                current_customer.name = nameTextBox.Text;
                current_customer.address = addressTextBox.Text;
                current_customer.zip_code = zipTextBox.Text;
                current_customer.phone_number = phoneTextBox.Text;
            }
            else
            {
                current_customer = null;
            }

            return current_customer;
        }

        protected void mgtButtom_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrderManagement.aspx");
        }
    }
}