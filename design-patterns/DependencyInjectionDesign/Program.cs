public enum ServiceLifetime
{
    Transient,
    //Scoped,
    Singleton
}


// Servis arayüzü
public interface IService
{
    void PerformAction();
}


// Servis sınıfı
public class ServiceA : IService
{
    public void PerformAction()
    {
        Console.WriteLine("Performing action...");
    }
}

public class ServiceB : IService
{
    public void PerformAction()
    {
        Console.WriteLine("Performing action...");
    }
}

public class DIContainer
{
    private readonly Dictionary<Type, (Func<object> factory, ServiceLifetime lifetime)> _serviceRegistrations =
          new Dictionary<Type, (Func<object> factory, ServiceLifetime lifetime)>();

    private readonly Dictionary<Type, object> _singletonInstances = new Dictionary<Type, object>();

    public void Register<TInterface, TImplementation>(ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TImplementation : TInterface, new()
    {
        if (lifetime == ServiceLifetime.Singleton)
        {
            _singletonInstances[typeof(TInterface)] = CreateInstance<TImplementation>(lifetime);
        }
        if (lifetime == ServiceLifetime.Transient)
        {
            _serviceRegistrations[typeof(TInterface)] = (() => CreateInstance<TImplementation>(lifetime), lifetime);
        }
    }

    public TInterface Resolve<TInterface>()
    {
        if (_serviceRegistrations.TryGetValue(typeof(TInterface), out var registration))
        {
            return (TInterface)registration.factory.Invoke();
        }
        else if (_singletonInstances.TryGetValue(typeof(TInterface), out var singletonInstance))
        {
            return (TInterface)singletonInstance;
        }
        else
        {
            throw new InvalidOperationException($"Service of type {typeof(TInterface)} is not registered.");
        }
    }
 
    private object CreateInstance<TImplementation>(ServiceLifetime lifetime) where TImplementation : new()
    {
        if (lifetime == ServiceLifetime.Singleton)
        {
            Type type = typeof(TImplementation);
            if (_singletonInstances.ContainsKey(type))
            {
                return _singletonInstances[type];
            }
            else
            {
                TImplementation instance = new TImplementation();
                _singletonInstances[type] = instance;
                return instance;
            }
        }
        else //Transient
        {
            return new TImplementation();
        }
       
    }

}

class Program
{
    static void Main(string[] args)
    {
        var container = new DIContainer();

        // Transient lifetime için
        container.Register<IService, ServiceA>(ServiceLifetime.Transient);

        var service1 = container.Resolve<IService>();
        var service2 = container.Resolve<IService>();


        var container2 = new DIContainer();


        // Singleton lifetime için
        container2.Register<IService, ServiceB>(ServiceLifetime.Singleton);
        var service3 = container2.Resolve<IService>();
        var service4 = container2.Resolve<IService>();

        bool state = ReferenceEquals(service1, service2),
            state2 = ReferenceEquals(service3, service4);

        Console.ForegroundColor = ConsoleColor.Green;

        Console.WriteLine($"Hizmet Yaşam Döngüsü :Transient(Geçici) = {state}"); 
        Console.WriteLine($"Hizmet Yaşam Döngüsü :Singleton(Tekil) = {state2}");
        Console.ResetColor();
        Console.ReadLine();

    }
}
