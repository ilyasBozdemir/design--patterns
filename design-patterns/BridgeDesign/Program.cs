using System;

// Bridge arayüzü
interface IMessageSender
{
    void SendMessage(string message);
}

// Concrete Implementor 1
class EmailSender : IMessageSender
{
    public void SendMessage(string message)
    {
        Console.WriteLine("E-mail gönderiliyor: " + message);
    }
}

// Concrete Implementor 2
class SmsSender : IMessageSender
{
    public void SendMessage(string message)
    {
        Console.WriteLine("SMS gönderiliyor: " + message);
    }
}

// Abstraction sınıfı
abstract class Message
{
    protected IMessageSender _messageSender;

    public Message(IMessageSender messageSender)
    {
        _messageSender = messageSender;
    }

    public abstract void Send();
}

// Refined Abstraction 1
class SystemAlert : Message
{
    public SystemAlert(IMessageSender messageSender) : base(messageSender)
    {
    }

    public override void Send()
    {
        _messageSender.SendMessage("Sistem uyarısı!");
    }
}

// Refined Abstraction 2
class PromotionNotification : Message
{
    public PromotionNotification(IMessageSender messageSender) : base(messageSender)
    {
    }

    public override void Send()
    {
        _messageSender.SendMessage("Promosyon bildirimi!");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Concrete Implementor'ları oluştur
        IMessageSender emailSender = new EmailSender();
        IMessageSender smsSender = new SmsSender();

        // Abstraction sınıflarını oluştur ve Implementor'ları enjekte et
        Message systemAlertEmail = new SystemAlert(emailSender);
        Message promotionSms = new PromotionNotification(smsSender);

        // Mesajları gönder
        systemAlertEmail.Send();
        promotionSms.Send();
    }
}
