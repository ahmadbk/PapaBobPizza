using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PapaBobMegaChallenge
{
    public partial class OrderManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateView();
        }

        protected void orderGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            var row = orderGridView.Rows[index];
            var order_id = row.Cells[1].Text;
            Domain.OrderManager.ChangeOrderStatus(order_id);
            UpdateView();
        }

        public void UpdateView()
        {
            List<DTO.Order> orders = Domain.OrderManager.ObtainOrdersList();
            orderGridView.DataSource = orders;
            orderGridView.DataBind();
        }

        protected void backButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}