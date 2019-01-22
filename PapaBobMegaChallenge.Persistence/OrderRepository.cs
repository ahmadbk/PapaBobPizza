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
            bool check_if_customer_exits = CustomerRepository.CustomerExists(dbCustomersList, current_customer);

            if (!check_if_customer_exits)
            {
                var new_customer = new Customer();
                CustomerRepository.CreateNewCustomer(current_customer, out new_customer);
                new_order.customer_id = new_customer.customer_id;
                new_customer.amount_owing = current_order.payment_type != DTO.Payment.Cash ? current_order.cost : 0;
                new_order.Customer = new_customer;
                dbCustomers.Add(new_customer);
            }
            else
            {
                var existing_customer = dbCustomersList?.Find(p => p.phone_number == current_customer.phone_number);
                new_order.customer_id = existing_customer.customer_id;
                existing_customer.amount_owing += current_order.payment_type != DTO.Payment.Cash ? current_order.cost : 0;
            }

            dbOrders.Add(new_order);
            db.SaveChanges();
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
            CustomerRepository.CustomerMapper(current_order.Customer, out customer);
            new_order.Customer = customer;
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
    }
}