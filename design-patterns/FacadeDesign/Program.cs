using System;

// Subsystem 1
class Engine
{
    public void Start()
    {
        Console.WriteLine("Motor çalıştırılıyor.");
    }

    public void Stop()
    {
        Console.WriteLine("Motor durduruluyor.");
    }
}

// Subsystem 2
class AirConditioner
{
    public void TurnOn()
    {
        Console.WriteLine("Klima açılıyor.");
    }

    public void TurnOff()
    {
        Console.WriteLine("Klima kapatılıyor.");
    }
}

// Facade
class CarFacade
{
    private Engine _engine;
    private AirConditioner _airConditioner;

    public CarFacade()
    {
        _engine = new Engine();
        _airConditioner = new AirConditioner();
    }

    public void StartCar()
    {
        _engine.Start();
        _airConditioner.TurnOn();
        Console.WriteLine("Araba kullanıma hazır.");
    }

    public void StopCar()
    {
        _engine.Stop();
        _airConditioner.TurnOff();
        Console.WriteLine("Araba park edildi.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Facade kullanarak arabayı başlatma
        CarFacade carFacade = new CarFacade();
        carFacade.StartCar();

        Console.WriteLine("------------------------");

        // Facade kullanarak arabayı durdurma
        carFacade.StopCar();
    }
}
