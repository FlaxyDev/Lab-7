using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    class Order
    {
        public string Customer { get; set; }
        public int OrderSize { get; set; }
        public string ShippingMethod { get; set; }
        public string PaymentMethod { get; set; }
    }

    abstract class OrderHandler
    {
        protected OrderHandler successor;

        public void SetSuccessor(OrderHandler successor)
        {
            this.successor = successor;
        }

        public abstract void HandleOrder(Order order);
    }

    class SizeHandler : OrderHandler
    {
        public override void HandleOrder(Order order)
        {
            if (order.OrderSize <= 10)
            {
                Console.WriteLine($"Order for {order.Customer} is a small order.");
            }
            else if (order.OrderSize <= 30)
            {
                Console.WriteLine($"Order for {order.Customer} is an average-sized order.");
            }
            else
            {
                Console.WriteLine($"Order for {order.Customer} is a large order.");
            }

            if (successor != null)
            {
                successor.HandleOrder(order);
            }
        }
    }

    class ShippingHandler : OrderHandler
    {
        public override void HandleOrder(Order order)
        {
            if (order.ShippingMethod == "Courier")
            {
                Console.WriteLine($"Order for {order.Customer} will be delivered by courier.");
            }
            else if (order.ShippingMethod == "Pickup")
            {
                Console.WriteLine($"Order for {order.Customer} will be available for pickup.");
            }

            if (successor != null)
            {
                successor.HandleOrder(order);
            }
        }
    }

    class PaymentHandler : OrderHandler
    {
        public override void HandleOrder(Order order)
        {
            if (order.PaymentMethod == "Card")
            {
                Console.WriteLine($"Order for {order.Customer} will be paid by card.");
            }
            else if (order.PaymentMethod == "Cash")
            {
                Console.WriteLine($"Order for {order.Customer} will be paid by cash.");
            }
            else if (order.PaymentMethod == "Online")
            {
                Console.WriteLine($"Order for {order.Customer} will be paid online.");
            }

            if (successor != null)
            {
                successor.HandleOrder(order);
            }
        }
    }



    internal class Program
    {
        static void Main(string[] args)
        {
            OrderHandler sizeHandler = new SizeHandler();
            OrderHandler shippingHandler = new ShippingHandler();
            OrderHandler paymentHandler = new PaymentHandler();

            sizeHandler.SetSuccessor(shippingHandler);
            shippingHandler.SetSuccessor(paymentHandler);

            Order testOrder = new Order
            {
                Customer = "Max",
                OrderSize = 15,
                ShippingMethod = "Courier",
                PaymentMethod = "Online"
            };

            sizeHandler.HandleOrder(testOrder);
        }
    }
}
