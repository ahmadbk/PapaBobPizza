using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapaBobMegaChallenge.Domain
{
    public class OrderManager
    {
        //calculate total cost given DTO order object return a double
        public static double CalculateAmountOwing(DTO.Order current_order)
        {
            double cost = 0.0;

            switch (current_order.size)
            {
                case DTO.Size.Small:
                    cost += 12;
                    break;
                case DTO.Size.Medium:
                    cost += 14;
                    break;
                case DTO.Size.Large:
                    cost += 16;
                    break;
                default:
                    break;
            }

            switch (current_order.crust)
            {
                case DTO.Crust.Regular:
                    break;
                case DTO.Crust.Thin:
                    break;
                case DTO.Crust.Thick:
                    cost += 2;
                    break;
                default:
                    break;
            }

            cost += current_order.green_peppers ? 1 : 0;
            cost += current_order.onions ? 1 : 0;
            cost += current_order.pepperoni ? 1.5 : 0;
            cost += current_order.sausage ? 2 : 0;

            return cost;
        }

        //recieve order and customer object from presentation and pass onto persistence
        public static void AddOrder(DTO.Order current_order, DTO.Customer current_customer)
        {
            Persistence.OrderRepository.AddOrder(current_order, current_customer);
        }


        public static List<DTO.Order> ObtainOrdersList()
        {
            return Persistence.OrderRepository.GetOrdersList();
        }

        public static List<DTO.Customer> ObtainCustomersList()
        {
            return Persistence.OrderRepository.GetCustomerList();
        }

        public static void ChangeOrderStatus(string current_order_id)
        {
            Persistence.OrderRepository.UpdateOrder(current_order_id);
        }
    }
}
