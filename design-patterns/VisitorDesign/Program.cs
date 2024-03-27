using System;
using System.Collections.Generic;

// Visitor arayüzü
interface IVisitor
{
    void Visit(Product product);
    void Visit(Service service);
}

// Element arayüzü
interface IElement
{
    void Accept(IVisitor visitor);
}

// Ürün sınıfı (Concrete Element)
class Product : IElement
{
    public string Name { get; }

    public Product(string name)
    {
        Name = name;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}

// Hizmet sınıfı (Concrete Element)
class Service : IElement
{
    public string Name { get; }

    public Service(string name)
    {
        Name = name;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}

// Ziyaretçi sınıfı (Concrete Visitor)
class TaxVisitor : IVisitor
{
    public void Visit(Product product)
    {
        Console.WriteLine($"Ürün vergisi hesaplandı: {product.Name}");
    }

    public void Visit(Service service)
    {
        Console.WriteLine($"Hizmet vergisi hesaplandı: {service.Name}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<IElement> elements = new List<IElement>
        {
            new Product("Bilgisayar"),
            new Service("Tamir Hizmeti"),
            new Product("Yazıcı")
        };

        // Ziyaretçi oluştur
        IVisitor visitor = new TaxVisitor();

        // Tüm öğeleri ziyaret et
        foreach (var element in elements)
        {
            element.Accept(visitor);
        }
    }
}
