using System;
using System.Collections.Generic;

// Gözlemci arabirimi
interface IObserver
{
    void Update(string message);
}

// Gözlemcilere sahip olan nesne
class Subject
{
    private List<IObserver> _observers = new List<IObserver>();
    private string _state;

    public string State
    {
        get { return _state; }
        set
        {
            _state = value;
            NotifyObservers();
        }
    }

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    private void NotifyObservers()
    {
        foreach (var observer in _observers)
        {
            observer.Update(_state);
        }
    }
}

// Gerçek gözlemci
class ConcreteObserver : IObserver
{
    private string _name;

    public ConcreteObserver(string name)
    {
        _name = name;
    }

    public void Update(string message)
    {
        Console.WriteLine($"{_name} received message: {message}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Konu oluştur
        Subject subject = new Subject();

        // Gözlemcileri oluştur
        IObserver observer1 = new ConcreteObserver("Observer 1");
        IObserver observer2 = new ConcreteObserver("Observer 2");

        // Gözlemcileri konuya ekle
        subject.Attach(observer1);
        subject.Attach(observer2);

        // Konu durumunu güncelle (Gözlemcilere bildirim gönder)
        subject.State = "New state!";
    }
}
