using System;
using System.Collections.Generic;

// Arabirim tanımlayıcı: ChatRoom
interface IChatRoom
{
    void SendMessage(string sender, string message);
    void RegisterUser(User user);
}

// Mediator sınıfı: Concrete Mediator
class ChatRoom : IChatRoom
{
    private Dictionary<string, User> _users = new Dictionary<string, User>();

    public void RegisterUser(User user)
    {
        _users[user.Name] = user;
    }

    public void SendMessage(string sender, string message)
    {
        foreach (var user in _users.Values)
        {
            if (user.Name != sender) // Kendi kendine mesaj gönderme
            {
                user.ReceiveMessage(sender, message);
            }
        }
    }
}

// Katılımcı sınıfı: Colleague
class User
{
    public string Name { get; }

    public User(string name)
    {
        Name = name;
    }

    public void SendMessage(IChatRoom chatRoom, string message)
    {
        chatRoom.SendMessage(Name, message);
    }

    public void ReceiveMessage(string sender, string message)
    {
        Console.WriteLine($"{Name} receives message from {sender}: {message}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Mediator oluştur
        IChatRoom chatRoom = new ChatRoom();

        // Kullanıcıları oluştur ve mediator'e kaydet
        User user1 = new User("User 1");
        User user2 = new User("User 2");
        User user3 = new User("User 3");

        chatRoom.RegisterUser(user1);
        chatRoom.RegisterUser(user2);
        chatRoom.RegisterUser(user3);

        // Kullanıcılar arasında iletişim kur
        user1.SendMessage(chatRoom, "Hello everyone!");
        user2.SendMessage(chatRoom, "Hi there!");
        user3.SendMessage(chatRoom, "Hey!");
    }
}
