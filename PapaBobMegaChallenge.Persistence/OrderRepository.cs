using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaBobMegaChallenge.Persistence
{
    public class OrderRepository
    {
        public static void AddOrder(DTO.Order current_order, DTO.Customer current_customer)
        {
            PapaBobEntities db = new PapaBobEntities();
            System.Data.Entity.DbSet<Order> dbOrders = db.Orders;
            var dbCustomers = db.Customers;

            var new_order = new Persistence.Order();
            CreateNewOrder(current_order, out new_order);

            var dbCustomersList = db.Customers.ToList();
            bool check_if_customer_exits = CustomerExists(dbCustomersList, current_customer);

            if (!check_if_customer_exits)
            {
                var new_customer = new Customer();
                CreateNewCustomer(current_customer, out new_customer);
                new_order.customer_id = new_customer.customer_id;
                new_customer.amount_owing = current_order.payment_type != DTO.Payment.Cash ? current_order.cost : 0;
                new_order.Customer = new_customer;
                try
                {
                    dbCustomers.Add(new_customer);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var existing_customer = dbCustomersList?.Find(p => p.phone_number == current_customer.phone_number);
                new_order.customer_id = existing_customer.customer_id;
                existing_customer.amount_owing += current_order.payment_type != DTO.Payment.Cash ? current_order.cost : 0;
            }

            try
            {
                dbOrders.Add(new_order);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private static void OrderMapper(DTO.Order current_order, out Persistence.Order new_order)
        {
            new_order = new Persistence.Order();
            new_order.completed = false;
            new_order.cost = current_order.cost;
            new_order.crust = (Persistence.Crust)current_order.crust;
            new_order.payment_type = (Persistence.Payment)current_order.payment_type;
            new_order.size = (Persistence.Size)current_order.size;
            new_order.onions = current_order.onions;
            new_order.green_peppers = current_order.green_peppers;
            new_order.sausage = current_order.sausage;
            new_order.pepperoni = current_order.pepperoni;
        }

        private static void CustomerMapper(DTO.Customer current_customer, out Persistence.Customer new_customer)
        {
            new_customer = new Persistence.Customer();
            new_customer.name = current_customer.name;
            new_customer.address = current_customer.address;
            new_customer.zip_code = current_customer.zip_code;
            new_customer.phone_number = current_customer.phone_number;
        }

        private static bool CustomerExists(List<Persistence.Customer> dbCustomersList, DTO.Customer current_customer)
        {
            var count = dbCustomersList?.Where(p => p.phone_number == current_customer.phone_number).Count();

            if (count > 0)
                return true;
            else
                return false;
        }

        private static void CreateNewCustomer(DTO.Customer current_customer, out Persistence.Customer new_customer)
        {
            new_customer = new Customer();
            CustomerMapper(current_customer, out new_customer);
            System.Guid customer_id = Guid.NewGuid();
            new_customer.customer_id = customer_id;
        }

        private static void CreateNewOrder(DTO.Order current_order, out Persistence.Order new_order)
        {
            new_order = new Persistence.Order();
            OrderMapper(current_order, out new_order);
            System.Guid order_id = Guid.NewGuid();
            new_order.order_id = order_id;
        }

        private static void OrderMapper(Persistence.Order current_order, out DTO.Order new_order)
        {
            new_order = new DTO.Order();
            new_order.completed = false;
            new_order.cost = current_order.cost;
            new_order.size = (DTO.Size)current_order.size;
            new_order.crust = (DTO.Crust)current_order.crust;
            new_order.payment_type = (DTO.Payment)current_order.payment_type;
            new_order.onions = current_order.onions;
            new_order.green_peppers = current_order.green_peppers;
            new_order.sausage = current_order.sausage;
            new_order.pepperoni = current_order.pepperoni;
            new_order.customer_id = current_order.customer_id;
            new_order.order_id = current_order.order_id;

            DTO.Customer customer = new DTO.Customer();
            CustomerMapper(current_order.Customer, out customer);
            new_order.Customer = customer;
        }

        private static void CustomerMapper(Persistence.Customer current_customer, out DTO.Customer new_customer)
        {
            new_customer = new DTO.Customer();
            new_customer.name = current_customer.name;
            new_customer.address = current_customer.address;
            new_customer.zip_code = current_customer.zip_code;
            new_customer.phone_number = current_customer.phone_number;
            new_customer.customer_id = current_customer.customer_id;
            new_customer.amount_owing = current_customer.amount_owing;
        }

        public static List<DTO.Customer> GetCustomerList()
        {
            PapaBobEntities db = new PapaBobEntities();
            var dbCustomers = db.Customers.ToList();
            List<DTO.Customer> dto_customers_list = new List<DTO.Customer>();

            foreach (var customer in dbCustomers)
            {
                DTO.Customer new_customer = new DTO.Customer();
                CustomerMapper(customer, out new_customer);
                dto_customers_list.Add(new_customer);
            }
            return dto_customers_list;
        }

        public static List<DTO.Order> GetOrdersList()
        {
            PapaBobEntities db = new PapaBobEntities();
            var dbOrders = db.Orders.ToList();
            List<DTO.Order> dto_orders_list = new List<DTO.Order>();

            foreach (var order in dbOrders)
            {
                if(!order.completed)
                {
                    DTO.Order new_order = new DTO.Order();
                    OrderMapper(order, out new_order);
                    dto_orders_list.Add(new_order);
                }
            }
            return dto_orders_list;
        }

        public static void UpdateOrder(string current_order_id)
        {
            PapaBobEntities db = new PapaBobEntities();
            var dbOrders = db.Orders.ToList();
            var existing_order = dbOrders?.Find(p => p.order_id == new Guid(current_order_id));
            existing_order.completed = true;
            db.SaveChanges();
        }

        public static DTO.PizzaPriceTable GetPrices()
        {
            PapaBobEntities db = new PapaBobEntities();
            var dbPrices = db.PizzaPriceTables.ToList();
            dbPrices = dbPrices.OrderBy(p => p.Date).ToList();
            var latestPrice = dbPrices[dbPrices.Count - 1];
            
            return PriceMapper(latestPrice);
        }

        private static DTO.PizzaPriceTable PriceMapper(Persistence.PizzaPriceTable current_prices)
        {
            DTO.PizzaPriceTable prices_current = new DTO.PizzaPriceTable();

            prices_current.Id = current_prices.Id;
            prices_current.Date = current_prices.Date;
            prices_current.SmallSizeCost = current_prices.SmallSizeCost;
            prices_current.MediumSizeCost = current_prices.MediumSizeCost;
            prices_current.LargeSizeCost = current_prices.LargeSizeCost;
            prices_current.ThickCrustCost = current_prices.ThickCrustCost;
            prices_current.ThinCrustCost = current_prices.ThinCrustCost;
            prices_current.SausageCost = current_prices.SausageCost;
            prices_current.GreenPeppersCost = current_prices.GreenPeppersCost;
            prices_current.OnionsCost = current_prices.OnionsCost;
            prices_current.PepperoniCost = current_prices.PepperoniCost;

            return prices_current;
        }
    }
}