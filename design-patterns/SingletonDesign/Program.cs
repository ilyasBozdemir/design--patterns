using System;

public class Singleton
{
    private static Singleton _instance;
    private static readonly object _lock = new object();

    // Constructor özel olarak tanımlanır.
    private Singleton()
    {
    }

    public static Singleton GetInstance()
    {
        // Çoklu thread ortamlarında güvenli bir şekilde tek bir örnek oluşturulması sağlanır.
        lock (_lock)
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }
    }

    public void DisplayMessage()
    {
        Console.WriteLine("Singleton instance is created and ready to use.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Singleton örneğine erişim.
        Singleton instance1 = Singleton.GetInstance();
        Singleton instance2 = Singleton.GetInstance();

        // İki örnek aynı nesneye referans ediyor.
        Console.WriteLine("Are instances the same? " + (instance1 == instance2));

        // Singleton örneğini kullanma.
        instance1.DisplayMessage();
    }
}
