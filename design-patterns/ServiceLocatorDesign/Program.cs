// Servis arayüzü
public interface IService
{
    void Serve();
}

// Servis sınıfı
public class ServiceA : IService
{
    public void Serve()
    {
        Console.WriteLine("Service A is serving...");
    }
}

// Başka bir servis sınıfı
public class ServiceB : IService
{
    public void Serve()
    {
        Console.WriteLine("Service B is serving...");
    }
}

// Servis yerini bulma sınıfı
public class ServiceLocator
{
    private Dictionary<string, IService> services;

    public ServiceLocator()
    {
        services = new Dictionary<string, IService>();
        services.Add("ServiceA", new ServiceA());
        services.Add("ServiceB", new ServiceB());
    }

    public IService GetService(string serviceName)
    {
        if (services.ContainsKey(serviceName))
        {
            return services[serviceName];
        }
        else
        {
            throw new ArgumentException("Service not found.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Servis yerini bulma nesnesi oluşturulur
        ServiceLocator locator = new ServiceLocator();

        // Servisler alınır ve kullanılır
        IService serviceA = locator.GetService("ServiceA");
        serviceA.Serve();

        IService serviceB = locator.GetService("ServiceB");
        serviceB.Serve();

        // Bilinmeyen bir servis denemek
        try
        {
            IService unknownService = locator.GetService("UnknownService");
            unknownService.Serve();
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
