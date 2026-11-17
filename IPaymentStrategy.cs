using System;

namespace DeliveryService
{
    public interface IPaymentStrategy
    {
        void Pay(double amount);
    }

    public class CardPayment : IPaymentStrategy
    {
        public void Pay(double amount)
        {
            Console.WriteLine($"[Оплата] Оплачено {amount} грн банківською карткою.");
        }
    }

    public class CashPayment : IPaymentStrategy
    {
        public void Pay(double amount)
        {
            Console.WriteLine($"[Оплата] Оплачено {amount} грн готівкою.");
        }
    }
}
