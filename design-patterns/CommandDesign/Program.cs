using System;
using System.Collections.Generic;

// Alıcı (Receiver) arayüzü
interface IReceiver
{
    void Action();
}

// Gerçek Alıcı (Concrete Receiver) sınıfı
class Light : IReceiver
{
    public void Action()
    {
        Console.WriteLine("Işıklar açıldı.");
    }
}

// Komut (Command) arayüzü
interface ICommand
{
    void Execute();
}

// Işıkları Aç Komutu (Concrete Command) sınıfı
class TurnOnLightCommand : ICommand
{
    private IReceiver _receiver;

    public TurnOnLightCommand(IReceiver receiver)
    {
        _receiver = receiver;
    }

    public void Execute()
    {
        _receiver.Action();
    }
}

// Invoker sınıfı
class RemoteControl
{
    private List<ICommand> _commands = new List<ICommand>();

    public void SetCommand(ICommand command)
    {
        _commands.Add(command);
    }

    public void PressButton()
    {
        foreach (var command in _commands)
        {
            command.Execute();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Alıcı oluştur
        IReceiver receiver = new Light();

        // Komutu oluştur ve alıcıyı ile eşleştir
        ICommand command = new TurnOnLightCommand(receiver);

        // Uzaktan kumandayı oluştur ve komutu ayarla
        RemoteControl remoteControl = new RemoteControl();
        remoteControl.SetCommand(command);

        // Uzaktan kumandayı kullanarak komutu çalıştır
        remoteControl.PressButton();
    }
}
