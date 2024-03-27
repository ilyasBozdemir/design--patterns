using System;

// Kargo durumu arayüzü
interface IShipmentState
{
    void ProcessShipment(Shipment shipment);
}

// Kargo sınıfı
class Shipment
{
    public string TrackingNumber { get; private set; }
    public IShipmentState CurrentState { get; set; }

    public Shipment(string trackingNumber, IShipmentState initialState)
    {
        TrackingNumber = trackingNumber;
        CurrentState = initialState;
    }

    public void Process()
    {
        CurrentState.ProcessShipment(this);
    }
}

// Kargo durumu - Hazırlanıyor
class ShipmentPreparingState : IShipmentState
{
    public void ProcessShipment(Shipment shipment)
    {
        Console.WriteLine($"Kargo {shipment.TrackingNumber} hazırlanıyor...");
        shipment.CurrentState = new ShipmentReadyToShipState();
    }
}

// Kargo durumu - Gönderilmeye Hazır
class ShipmentReadyToShipState : IShipmentState
{
    public void ProcessShipment(Shipment shipment)
    {
        Console.WriteLine($"Kargo {shipment.TrackingNumber} gönderilmeye hazır.");
        shipment.CurrentState = new ShipmentShippedState();
    }
}

// Kargo durumu - Gönderildi
class ShipmentShippedState : IShipmentState
{
    public void ProcessShipment(Shipment shipment)
    {
        Console.WriteLine($"Kargo {shipment.TrackingNumber} gönderildi.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Shipment shipment = new Shipment("ABC123", new ShipmentPreparingState());

        shipment.Process(); // Kargo hazırlanıyor...
        shipment.Process(); // Kargo gönderilmeye hazır.
        shipment.Process(); // Kargo gönderildi.
    }
}
