using System;

namespace DeliveryService
{
    public class DeliveryService
    {
        private List<Transport> _transports;
        private List<Order> _activeOrders;
        private List<Order> _history;

        public DeliveryService()
        {
            _transports = new List<Transport>();
            _activeOrders = new List<Order>();
            _history = new List<Order>();
        }

        public void AddTransport(Transport transport)
        {
            _transports.Add(transport);
        }

        public void CreateOrder(Order order)
        {
            _activeOrders.Add(order);
            Console.WriteLine($"[Замовлення №{order.Id}] Створено: {order.Description} ({order.Weight} кг, {order.Distance} км)");
        }

        public void ShowCalculatedCosts(Order order)
        {
            Console.WriteLine($"\nРозрахунок вартості для замовлення №{order.Id} ({order.Description}):");
            foreach (Transport transport in _transports)
            {
                if (transport.CanTake(order))
                {
                    double cost = transport.CalculateCost(order);
                    Console.WriteLine($" - {transport.Name}: {cost} грн");
                }
            }
        }

        public bool AssignOrder(Order order, Transport transport)
        {
            if (transport.IsBusy)
            {
                Console.WriteLine($"[Помилка] {transport.Name} зараз зайнятий!");
                return false;
            }
            if (!transport.CanTake(order))
            {
                Console.WriteLine($"[Помилка] {transport.Name} не може взяти замовлення №{order.Id} (перевищено ліміт відстані).");
                return false;
            }

            transport.IsBusy = true;
            Console.WriteLine($"[Призначено] Замовлення №{order.Id} передано на: {transport.Name}.");
            return true;
        }

        public void DeliverOrder(Order order, Transport transport, IPaymentStrategy paymentMethod)
        {
            if (!transport.IsBusy) return;

            double finalCost = transport.CalculateCost(order);
            Console.WriteLine($"[Доставка] {transport.Name} доставив замовлення №{order.Id}.");
            paymentMethod.Pay(finalCost);

            transport.IsBusy = false;
            _activeOrders.Remove(order);
            _history.Add(order);
        }

        public void ShowActiveOrders()
        {
            Console.WriteLine("\n--- АКТИВНІ ЗАМОВЛЕННЯ ---");
            if (_activeOrders.Count == 0)
            {
                Console.WriteLine("Немає активних замовлень.");
                return;
            }

            foreach (Order order in _activeOrders)
            {
                Console.WriteLine($" - Замовлення №{order.Id}: {order.Description} ({order.Weight} кг, {order.Distance} км)");
            }
        }

        public void ShowHistory()
        {
            Console.WriteLine("\n--- ІСТОРІЯ ДОСТАВОК ---");
            if (_history.Count == 0)
            {
                Console.WriteLine("Історія порожня.");
                return;
            }

            foreach (Order order in _history)
            {
                Console.WriteLine($" - [Завершено] Замовлення №{order.Id}: {order.Description}");
            }
        }
    }
}
