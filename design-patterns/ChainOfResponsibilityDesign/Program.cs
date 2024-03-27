using System;

// Temsilci (Handler) arayüzü
interface IShippingHandler
{
    void HandlePackage(Package package);
    void SetNextHandler(IShippingHandler nextHandler);
}

// Soyut temsilci (Handler) sınıfı
abstract class ShippingHandler : IShippingHandler
{
    private IShippingHandler _nextHandler;

    public void SetNextHandler(IShippingHandler nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public virtual void HandlePackage(Package package)
    {
        if (_nextHandler != null)
        {
            _nextHandler.HandlePackage(package);
        }
        else
        {
            Console.WriteLine("Hiçbir taşıma hizmeti mevcut değil.");
        }
    }
}

// Concrete Handler 1
class RoadShipping : ShippingHandler
{
    public override void HandlePackage(Package package)
    {
        if (package.Weight <= 10)
        {
            Console.WriteLine("Paket karayolu ile gönderildi.");
        }
        else
        {
            base.HandlePackage(package);
        }
    }
}

// Concrete Handler 2
class AirShipping : ShippingHandler
{
    public override void HandlePackage(Package package)
    {
        if (package.Weight > 10 && package.Weight <= 50)
        {
            Console.WriteLine("Paket hava yolu ile gönderildi.");
        }
        else
        {
            base.HandlePackage(package);
        }
    }
}

// Concrete Handler 3
class SeaShipping : ShippingHandler
{
    public override void HandlePackage(Package package)
    {
        if (package.Weight > 50)
        {
            Console.WriteLine("Paket deniz yolu ile gönderildi.");
        }
        else
        {
            base.HandlePackage(package);
        }
    }
}

// İstek (Request) sınıfı
class Package
{
    public int Weight { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // Zincir oluşturma
        IShippingHandler roadShipping = new RoadShipping();
        IShippingHandler airShipping = new AirShipping();
        IShippingHandler seaShipping = new SeaShipping();

        roadShipping.SetNextHandler(airShipping);
        airShipping.SetNextHandler(seaShipping);

        // Paket işleme
        Package package1 = new Package { Weight = 5 };
        Package package2 = new Package { Weight = 20 };
        Package package3 = new Package { Weight = 60 };

        roadShipping.HandlePackage(package1);
        roadShipping.HandlePackage(package2);
        roadShipping.HandlePackage(package3);
    }
}
