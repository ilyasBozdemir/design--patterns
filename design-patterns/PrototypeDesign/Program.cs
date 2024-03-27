using System;

// Prototype arayüzü
interface IPrototype
{
    IPrototype Clone();
    void Display();
}

// ConcretePrototype sınıfı
class ConcretePrototype : IPrototype
{
    private string _name;
    private int _age;

    public ConcretePrototype(string name, int age)
    {
        _name = name;
        _age = age;
    }

    public IPrototype Clone()
    {
        // Mevcut nesnenin kopyasını oluştur
        return new ConcretePrototype(_name, _age);
    }

    public void Display()
    {
        Console.WriteLine($"Name: {_name}, Age: {_age}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Prototip nesne oluştur
        ConcretePrototype prototype = new ConcretePrototype("John", 30);

        // Prototipin kopyasını oluştur
        ConcretePrototype clone = (ConcretePrototype)prototype.Clone();

        // Kopyayı göster
        clone.Display();
    }
}
