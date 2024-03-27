using System;

// Araba arayüzü
interface ICar
{
    string GetBrand();
    string GetType();
}

// BMW arabası
class BMW : ICar
{
    public string GetBrand()
    {
        return "BMW";
    }

    public string GetType()
    {
        return "Sedan";
    }
}

// Audi arabası
class Audi : ICar
{
    public string GetBrand()
    {
        return "Audi";
    }

    public string GetType()
    {
        return "SUV";
    }
}

// Araba Fabrikası arayüzü
interface ICarFactory
{
    ICar ProduceCar();
}

// BMW Fabrikası
class BMWFactory : ICarFactory
{
    public ICar ProduceCar()
    {
        return new BMW();
    }
}

// Audi Fabrikası
class AudiFactory : ICarFactory
{
    public ICar ProduceCar()
    {
        return new Audi();
    }
}

// Kullanıcı sınıfı
class Client
{
    private ICarFactory _carFactory;

    public Client(ICarFactory carFactory)
    {
        _carFactory = carFactory;
    }

    public void BuildCar()
    {
        ICar car = _carFactory.ProduceCar();
        Console.WriteLine("Üretilen Araba: " + car.GetBrand() + " - " + car.GetType());
    }
}

class Program
{
    static void Main(string[] args)
    {
        // BMW fabrikası
        ICarFactory bmwFactory = new BMWFactory();
        // Audi fabrikası
        ICarFactory audiFactory = new AudiFactory();

        // BMW arabası üret
        Client bmwClient = new Client(bmwFactory);
        bmwClient.BuildCar();

        // Audi arabası üret
        Client audiClient = new Client(audiFactory);
        audiClient.BuildCar();
    }
}
