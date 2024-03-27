using System;
using System.Collections.Generic;

// Component arayüzü
interface IGraphic
{
    void Draw();
}

// Leaf sınıfı
class Line : IGraphic
{
    public void Draw()
    {
        Console.WriteLine("Çizgi çiziliyor.");
    }
}

// Leaf sınıfı
class Circle : IGraphic
{
    public void Draw()
    {
        Console.WriteLine("Daire çiziliyor.");
    }
}

// Composite sınıfı
class Picture : IGraphic
{
    private List<IGraphic> _graphics = new List<IGraphic>();

    public void Add(IGraphic graphic)
    {
        _graphics.Add(graphic);
    }

    public void Remove(IGraphic graphic)
    {
        _graphics.Remove(graphic);
    }

    public void Draw()
    {
        foreach (IGraphic graphic in _graphics)
        {
            graphic.Draw();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Leaf nesneleri oluştur
        Line line = new Line();
        Circle circle = new Circle();

        // Composite nesnesi oluştur
        Picture picture = new Picture();

        // Composite nesneye Leaf nesnelerini ekle
        picture.Add(line);
        picture.Add(circle);

        // Composite nesneyi çiz
        picture.Draw();
    }
}
