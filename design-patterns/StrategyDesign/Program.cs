using System;

// Kargo stratejisi arabirimi
interface IShippingStrategy
{
    double CalculateShippingCost(double weight);
    int EstimateDeliveryDays();
}

// Karayolu kargo stratejisi
class RoadShippingStrategy : IShippingStrategy
{
    public double CalculateShippingCost(double weight)
    {
        return weight * 0.5; // Karayolu kargo maliyeti: 0.5 TL/kg
    }

    public int EstimateDeliveryDays()
    {
        return 3; // Karayolu kargo teslim süresi: 3 gün
    }
}

// Hava yolu kargo stratejisi
class AirShippingStrategy : IShippingStrategy
{
    public double CalculateShippingCost(double weight)
    {
        return weight * 1.5; // Hava yolu kargo maliyeti: 1.5 TL/kg
    }

    public int EstimateDeliveryDays()
    {
        return 1; // Hava yolu kargo teslim süresi: 1 gün
    }
}

// Deniz yolu kargo stratejisi
class SeaShippingStrategy : IShippingStrategy
{
    public double CalculateShippingCost(double weight)
    {
        return weight * 0.3; // Deniz yolu kargo maliyeti: 0.3 TL/kg
    }

    public int EstimateDeliveryDays()
    {
        return 7; // Deniz yolu kargo teslim süresi: 7 gün
    }
}

// Kargo sipariş sınıfı
class ShipmentOrder
{
    private IShippingStrategy _shippingStrategy;

    public ShipmentOrder(IShippingStrategy shippingStrategy)
    {
        _shippingStrategy = shippingStrategy;
    }

    public double CalculateShippingCost(double weight)
    {
        return _shippingStrategy.CalculateShippingCost(weight);
    }

    public int EstimateDeliveryDays()
    {
        return _shippingStrategy.EstimateDeliveryDays();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Karayolu kargo siparişi
        ShipmentOrder roadOrder = new ShipmentOrder(new RoadShippingStrategy());
        double roadCost = roadOrder.CalculateShippingCost(10);
        int roadDays = roadOrder.EstimateDeliveryDays();
        Console.WriteLine($"Karayolu kargo maliyeti: {roadCost} TL, Teslim süresi: {roadDays} gün");

        // Hava yolu kargo siparişi
        ShipmentOrder airOrder = new ShipmentOrder(new AirShippingStrategy());
        double airCost = airOrder.CalculateShippingCost(10);
        int airDays = airOrder.EstimateDeliveryDays();
        Console.WriteLine($"Hava yolu kargo maliyeti: {airCost} TL, Teslim süresi: {airDays} gün");

        // Deniz yolu kargo siparişi
        ShipmentOrder seaOrder = new ShipmentOrder(new SeaShippingStrategy());
        double seaCost = seaOrder.CalculateShippingCost(10);
        int seaDays = seaOrder.EstimateDeliveryDays();
        Console.WriteLine($"Deniz yolu kargo maliyeti: {seaCost} TL, Teslim süresi: {seaDays} gün");
    }
}
