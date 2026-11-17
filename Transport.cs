namespace DeliveryService
{
    public abstract class Transport
    {
        public string Name { get; protected set; }
        public bool IsBusy { get; set; }

        public Transport(string name)
        {
            Name = name;
            IsBusy = false;
        }

        public abstract double CalculateCost(Order order);

        public virtual bool CanTake(Order order)
        {
            return true;
        }
    }

    public class Bike : Transport
    {
        public Bike() : base("Велосипед") { }

        public override double CalculateCost(Order order)
        {
            return 20 + (5 * order.Distance);
        }

        public override bool CanTake(Order order)
        {
            return order.Distance <= 10;
        }
    }

    public class Car : Transport
    {
        public Car() : base("Автомобіль") { }

        public override double CalculateCost(Order order)
        {
            return 50 + (8 * order.Distance) + (1 * order.Weight);
        }
    }

    public class Truck : Transport
    {
        public Truck() : base("Вантажівка") { }

        public override double CalculateCost(Order order)
        {
            return 200 + (15 * order.Distance) + (0.5 * order.Weight);
        }
    }

}
