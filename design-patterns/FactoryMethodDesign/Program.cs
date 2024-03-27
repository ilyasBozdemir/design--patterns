using System;

// Ürün arayüzü
interface IProduct
{
    void Operate();
}

// Concrete Product sınıfları
class ConcreteProductA : IProduct
{
    public void Operate()
    {
        Console.WriteLine("Concrete Product A işlem yapıyor.");
    }
}

class ConcreteProductB : IProduct
{
    public void Operate()
    {
        Console.WriteLine("Concrete Product B işlem yapıyor.");
    }
}

// Creator (Factory) sınıfı
class Creator
{
    public IProduct FactoryMethod(string type)
    {
        switch (type)
        {
            case "A":
                return new ConcreteProductA();
            case "B":
                return new ConcreteProductB();
            default:
                throw new ArgumentException("Geçersiz ürün tipi.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Creator nesnesi oluştur
        Creator creator = new Creator();

        // Product A oluştur ve çalıştır
        IProduct productA = creator.FactoryMethod("A");
        productA.Operate();

        // Product B oluştur ve çalıştır
        IProduct productB = creator.FactoryMethod("B");
        productB.Operate();
    }
}
