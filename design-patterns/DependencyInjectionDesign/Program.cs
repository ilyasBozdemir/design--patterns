

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
public class Service : IService
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
        else
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
        container.Register<IService, Service>(ServiceLifetime.Transient);


        var service1 = container.Resolve<IService>();
        var service2 = container.Resolve<IService>();
        Console.WriteLine(ReferenceEquals(service1, service2)); // false


        container = new DIContainer();

        // Singleton lifetime için
        container.Register<IService, Service>(ServiceLifetime.Singleton);
        var service3 = container.Resolve<IService>();
        var service4 = container.Resolve<IService>();
        Console.WriteLine(ReferenceEquals(service3, service4)); // true

    }
}
