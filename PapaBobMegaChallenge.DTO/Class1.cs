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

    [Serializable]
    public class PizzaPriceTable
    {
        public System.Guid Id { get; set; }
        public System.DateTime Date { get; set; }
        public double SmallSizeCost { get; set; }
        public double MediumSizeCost { get; set; }
        public double LargeSizeCost { get; set; }
        public double ThickCrustCost { get; set; }
        public double ThinCrustCost { get; set; }
        public double PepperoniCost { get; set; }
        public double SausageCost { get; set; }
        public double GreenPeppersCost { get; set; }
        public double OnionsCost { get; set; }
    }
}
