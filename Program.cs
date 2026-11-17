using System;

namespace DeliveryService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            DeliveryService service = new DeliveryService();

            Bike bike = new Bike();
            Car car = new Car();
            Truck truck = new Truck();

            service.AddTransport(bike);
            service.AddTransport(car);
            service.AddTransport(truck);

            Order order1 = new Order(1, "піца та напої", 2.0, 5.0);
            Order order2 = new Order(2, "Офісне крісло", 15.0, 25.0);

            service.CreateOrder(order1);
            service.CreateOrder(order2);

            service.ShowCalculatedCosts(order1);
            service.ShowCalculatedCosts(order2);

            Console.WriteLine("\n--- СЦЕНАРІЙ 1: Помилка призначення (велика відстань) ---");
            service.AssignOrder(order2, bike);

            Console.WriteLine("\n--- СЦЕНАРІЙ 2: Доставка велосипедом (оплата карткою) ---");
            if (service.AssignOrder(order1, bike))
            {
                service.AssignOrder(order1, bike);
                service.DeliverOrder(order1, bike, new CardPayment());
            }

            Console.WriteLine("\n--- СЦЕНАРІЙ 3: Доставка авто (оплата готівкою) ---");
            if (service.AssignOrder(order2, car))
            {
                service.DeliverOrder(order2, car, new CashPayment());
            }

            service.ShowActiveOrders();
            service.ShowHistory();

            Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }
    }
}