using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaBobMegaChallenge.Persistence
{
    public class CustomerRepository
    {

        public static void CustomerMapper(DTO.Customer current_customer, out Persistence.Customer new_customer)
        {
            new_customer = new Persistence.Customer();
            new_customer.name = current_customer.name;
            new_customer.address = current_customer.address;
            new_customer.zip_code = current_customer.zip_code;
            new_customer.phone_number = current_customer.phone_number;
        }

        public static bool CustomerExists(List<Persistence.Customer> dbCustomersList, DTO.Customer current_customer)
        {
            var count = dbCustomersList?.Where(p => p.phone_number == current_customer.phone_number).Count();

            if (count > 0)
                return true;
            else
                return false;
        }

        public static void CreateNewCustomer(DTO.Customer current_customer, out Persistence.Customer new_customer)
        {
            new_customer = new Customer();
            CustomerMapper(current_customer, out new_customer);
            System.Guid customer_id = Guid.NewGuid();
            new_customer.customer_id = customer_id;
        }

        public static void CustomerMapper(Persistence.Customer current_customer, out DTO.Customer new_customer)
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

    }
}
