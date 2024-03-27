using System;
using System.Collections.Generic;

// Flyweight arayüzü
interface IRobot
{
    void Print();
}

// Concrete Flyweight sınıfı
class SmallRobot : IRobot
{
    public void Print()
    {
        Console.WriteLine("Küçük robot oluşturuldu.");
    }
}

// Concrete Flyweight sınıfı
class LargeRobot : IRobot
{
    public void Print()
    {
        Console.WriteLine("Büyük robot oluşturuldu.");
    }
}

// Flyweight Factory sınıfı
class RobotFactory
{
    private Dictionary<string, IRobot> _robots = new Dictionary<string, IRobot>();

    public int TotalObjectsCreated
    {
        get { return _robots.Count; }
    }

    public IRobot GetRobotFromFactory(string robotType)
    {
        IRobot robot = null;

        if (_robots.ContainsKey(robotType))
        {
            robot = _robots[robotType];
        }
        else
        {
            switch (robotType)
            {
                case "small":
                    Console.WriteLine("Küçük robot oluşturuluyor.");
                    robot = new SmallRobot();
                    _robots.Add("small", robot);
                    break;
                case "large":
                    Console.WriteLine("Büyük robot oluşturuluyor.");
                    robot = new LargeRobot();
                    _robots.Add("large", robot);
                    break;
                default:
                    throw new ArgumentException("Geçersiz robot tipi.");
            }
        }
        return robot;
    }
}

class Program
{
    static void Main(string[] args)
    {
        RobotFactory factory = new RobotFactory();

        IRobot smallRobot1 = factory.GetRobotFromFactory("small");
        smallRobot1.Print();

        IRobot smallRobot2 = factory.GetRobotFromFactory("small");
        smallRobot2.Print();

        IRobot largeRobot1 = factory.GetRobotFromFactory("large");
        largeRobot1.Print();

        IRobot largeRobot2 = factory.GetRobotFromFactory("large");
        largeRobot2.Print();

        // Fabrikadan oluşturulan toplam nesne sayısı
        Console.WriteLine("Toplam nesne sayısı: " + factory.TotalObjectsCreated);
    }
}
