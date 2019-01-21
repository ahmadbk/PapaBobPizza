using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaBobMegaChallenge.DTO
{
    public enum Crust : int
    {
        Regular = 0,
        Thin = 1,
        Thick = 2
    }

    public enum Size : int
    {
        Small = 0,
        Medium = 1,
        Large = 2
    }

    public enum Payment : int
    {
        Cash = 0,
        Credit = 1
    }

    [Serializable]
    public class Customer
    {
        public System.Guid customer_id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string zip_code { get; set; }
        public string phone_number { get; set; }
        public double amount_owing { get; set; }
    }

    [Serializable]
    public class Order
    {
        public System.Guid order_id { get; set; }
        public Size size { get; set; }
        public Crust crust { get; set; }
        public bool sausage { get; set; }
        public double cost { get; set; }
        public Payment payment_type { get; set; }
        public System.Guid customer_id { get; set; }
        public bool pepperoni { get; set; }
        public bool onions { get; set; }
        public bool green_peppers { get; set; }
        public bool completed { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
