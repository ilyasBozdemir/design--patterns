public enum ServiceLifetime
{
    Transient,
    Singleton
}


public interface IServiceA
{
    void PerformAction();
}
public interface IServiceB
{
    void PerformAction();
}


public class ServiceA : IServiceA
{
    public void PerformAction()
    {
        Console.WriteLine("ServiceA Performing action...");
    }
}

public class ServiceB : IServiceB
{
    public void PerformAction()
    {
        Console.WriteLine("ServiceB Performing action...");
    }
}


public class DIContainer // Dependency Injection Container
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

        // Singleton lifetime için ServiceA kaydı
        container.Register<IServiceA, ServiceA>(ServiceLifetime.Singleton);
        var serviceA1 = container.Resolve<IServiceA>();
        var serviceA2 = container.Resolve<IServiceA>();

        // Transient lifetime için ServiceB kaydı
        container.Register<IServiceB, ServiceB>(ServiceLifetime.Transient);
        var serviceB1 = container.Resolve<IServiceB>();
        var serviceB2 = container.Resolve<IServiceB>();

        Console.ForegroundColor = ConsoleColor.Green;

        Console.WriteLine("Singleton ServiceA:");
        Console.WriteLine($"ReferenceEquals: {ReferenceEquals(serviceA1, serviceA2)}"); // true
        Console.Write("Singleton ServiceA: ");
        serviceA1.PerformAction(); // Performing action...
        Console.WriteLine();

        Console.WriteLine("Transient ServiceB:");
        Console.WriteLine($"ReferenceEquals: {ReferenceEquals(serviceB1, serviceB2)}"); // false
        Console.Write("Transient ServiceB: ");
        serviceB1.PerformAction(); // Performing action...
        Console.ResetColor();
    }
}
