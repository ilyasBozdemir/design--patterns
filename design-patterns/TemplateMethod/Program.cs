using System;

// Kargo teslimatı soyut sınıfı
abstract class ShipmentDelivery
{
    // Template method: Kargo teslimat sürecini tanımlar
    public void Deliver()
    {
        PrepareShipment();
        TransportShipment();
        DeliverShipment();
        GetFeedback();
    }

    // Kargo hazırlama adımı (Abstract method)
    protected abstract void PrepareShipment();

    // Kargo taşıma adımı (Concrete method)
    protected virtual void TransportShipment()
    {
        Console.WriteLine("Kargo taşınıyor...");
    }

    // Kargo teslim adımı (Abstract method)
    protected abstract void DeliverShipment();

    // Geri bildirim alma adımı (Concrete method)
    protected virtual void GetFeedback()
    {
        Console.WriteLine("Müşteri geri bildirimi alınıyor...");
    }
}

// Karayolu kargo teslimatı sınıfı
class RoadShipmentDelivery : ShipmentDelivery
{
    protected override void PrepareShipment()
    {
        Console.WriteLine("Karayolu kargo hazırlanıyor...");
    }

    protected override void DeliverShipment()
    {
        Console.WriteLine("Karayolu kargo teslim ediliyor...");
    }
}

// Hava yolu kargo teslimatı sınıfı
class AirShipmentDelivery : ShipmentDelivery
{
    protected override void PrepareShipment()
    {
        Console.WriteLine("Hava yolu kargo hazırlanıyor...");
    }

    protected override void DeliverShipment()
    {
        Console.WriteLine("Hava yolu kargo teslim ediliyor...");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Karayolu kargo teslimatı
        Console.WriteLine("Karayolu kargo teslimatı:");
        ShipmentDelivery roadDelivery = new RoadShipmentDelivery();
        roadDelivery.Deliver();

        Console.WriteLine();

        // Hava yolu kargo teslimatı
        Console.WriteLine("Hava yolu kargo teslimatı:");
        ShipmentDelivery airDelivery = new AirShipmentDelivery();
        airDelivery.Deliver();
    }
}
