using System;

// İlk olarak mevcut olan arayüz
interface ITarget
{
    void Request();
}

// Mevcut olan sınıf
class Adaptee
{
    public void SpecificRequest()
    {
        Console.WriteLine("Specific Request");
    }
}

// Adapter sınıfı, hedef arayüzünü uygulayarak adapte edilecek olan sınıfı içerir
class Adapter : ITarget
{
    private Adaptee _adaptee;

    public Adapter(Adaptee adaptee)
    {
        _adaptee = adaptee;
    }

    public void Request()
    {
        // Mevcut sınıfın özelliklerini hedef arayüzle uyumlu hale getirir
        _adaptee.SpecificRequest();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Mevcut sınıf örneği oluştur
        Adaptee adaptee = new Adaptee();

        // Adapter örneği oluştur ve mevcut sınıfı parametre olarak ver
        ITarget adapter = new Adapter(adaptee);

        // Hedef arayüzü kullanarak talepte bulun
        adapter.Request();
    }
}
