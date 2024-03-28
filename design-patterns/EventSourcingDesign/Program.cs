using System;
using System.Collections.Generic;

// Olay sınıfı
class BankAccountEvent
{
    public decimal Amount { get; }
    public DateTime Timestamp { get; }

    public BankAccountEvent(decimal amount)
    {
        Amount = amount;
        Timestamp = DateTime.UtcNow;
    }
}

// Event Store
class EventStore
{
    private List<BankAccountEvent> _events = new List<BankAccountEvent>();

    public void RecordEvent(BankAccountEvent @event)
    {
        _events.Add(@event);
    }

    public IEnumerable<BankAccountEvent> GetEvents()
    {
        return _events;
    }
}

// Banka Hesabı sınıfı
class BankAccount
{
    private decimal _balance = 0;
    private EventStore _eventStore;

    public BankAccount(EventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public decimal Balance => _balance;

    public void Deposit(decimal amount)
    {
        _balance += amount;
        _eventStore.RecordEvent(new BankAccountEvent(amount));
    }

    public void Withdraw(decimal amount)
    {
        if (_balance >= amount)
        {
            _balance -= amount;
            _eventStore.RecordEvent(new BankAccountEvent(-amount));
        }
        else
        {
            Console.WriteLine("Yetersiz bakiye!");
        }
    }
}

// Uygulama
class Program
{
    static void Main(string[] args)
    {
        EventStore eventStore = new EventStore();
        BankAccount account = new BankAccount(eventStore);

        account.Deposit(1000);
        account.Withdraw(500);
        account.Withdraw(501); // Yetersiz bakiye, işlem gerçekleşmez

        // Hesap geçmişi görüntülenir
        Console.WriteLine("\nHesap Geçmişi:");
        foreach (var @event in eventStore.GetEvents())
        {
            if (@event.Amount>0)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{@event.Timestamp}] {@event.Amount}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

       
        Console.WriteLine($"\nMevcut Bakiye: {account.Balance}");
    }
}
