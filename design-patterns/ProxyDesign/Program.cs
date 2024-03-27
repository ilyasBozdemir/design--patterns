using System;

// Subject arayüzü
interface IImage
{
    void Display();
}

// RealSubject sınıfı
class HighResolutionImage : IImage
{
    private string _fileName;

    public HighResolutionImage(string fileName)
    {
        _fileName = fileName;
        LoadImageFromDisk();
    }

    private void LoadImageFromDisk()
    {
        Console.WriteLine("Yüksek çözünürlüklü resim " + _fileName + " yükleniyor...");
    }

    public void Display()
    {
        Console.WriteLine("Yüksek çözünürlüklü resim " + _fileName + " görüntüleniyor.");
    }
}

// Proxy sınıfı
class ImageProxy : IImage
{
    private string _fileName;
    private HighResolutionImage _realImage;

    public ImageProxy(string fileName)
    {
        _fileName = fileName;
    }

    public void Display()
    {
        if (_realImage == null)
        {
            _realImage = new HighResolutionImage(_fileName);
        }
        _realImage.Display();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Proxy kullanarak resmi görüntüleme
        IImage image1 = new ImageProxy("image1.jpg");
        image1.Display();

        // Aynı resmi tekrar görüntüleme, bu sefer proxy gerçek resmi yeniden yüklemez
        IImage image2 = new ImageProxy("image1.jpg");
        image2.Display();
    }
}
