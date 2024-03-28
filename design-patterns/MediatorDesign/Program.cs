// Event Sourcing ve Mediator deseni

// Event sınıfı
class MessageSentEvent
{
    public string Sender { get; }
    public string Receiver { get; }
    public string Message { get; }
    public DateTime Timestamp { get; }

    public MessageSentEvent(string sender, string receiver, string message)
    {
        Sender = sender;
        Receiver = receiver;
        Message = message;
        Timestamp = DateTime.UtcNow;
    }
}

// Event kaydedici
class EventStore
{
    private List<MessageSentEvent> _events = new List<MessageSentEvent>();

    public void RecordEvent(MessageSentEvent @event)
    {
        _events.Add(@event);
    }

    public IEnumerable<MessageSentEvent> GetEvents()
    {
        return _events;
    }
}

// Mediator arayüzü
interface IChatMediator
{
    void SendMessage(string sender, string receiver, string message);
}

// Concrete Mediator
class ChatMediator : IChatMediator
{
    private EventStore _eventStore;

    public ChatMediator(EventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public void SendMessage(string sender, string receiver, string message)
    {
        // Mesaj gönderildiğinde event oluşturulur ve kaydedilir
        var @event = new MessageSentEvent(sender, receiver, message);
        _eventStore.RecordEvent(@event);

        Console.WriteLine($"[{sender}]: {message} -> [{receiver}]");
    }
}

// Kullanıcı sınıfı
class User
{
    private string _name;
    private IChatMediator _mediator;

    public User(string name, IChatMediator mediator)
    {
        _name = name;
        _mediator = mediator;
    }

    public void SendMessage(string receiver, string message)
    {
        _mediator.SendMessage(_name, receiver, message);
    }
}

// Uygulama
class Program
{
    static void Main(string[] args)
    {
        // Event store oluşturulur
        EventStore eventStore = new EventStore();

        // Mediator oluşturulur ve kullanıcılara atanır
        IChatMediator mediator = new ChatMediator(eventStore);
        User alice = new User("Alice", mediator);
        User bob = new User("Bob", mediator);

        // Mesajlar gönderilir
        alice.SendMessage("Bob", "Merhaba Bob!");
        bob.SendMessage("Alice", "Merhaba Alice!");

        // Event store'daki event'ler gösterilir
        Console.WriteLine("\nEvent Log:");
        foreach (var @event in eventStore.GetEvents())
        {
            Console.WriteLine($"[{@event.Timestamp}] {@event.Sender} -> {@event.Receiver}: '{@event.Message}'");
        }
    }
}
