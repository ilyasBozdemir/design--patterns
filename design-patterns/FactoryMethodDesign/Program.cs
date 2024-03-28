using System;

// Üst sınıf: Belge arayüzü
interface IDocument
{
    void Open();
    void Close();
}

// Concrete Product 1: WordBelgesi
class WordDocument : IDocument
{
    public void Open()
    {
        Console.WriteLine("Microsoft Word belgesi açılıyor...");
    }

    public void Close()
    {
        Console.WriteLine("Microsoft Word belgesi kapatılıyor...");
    }
}

// Concrete Product 2: ExcelBelgesi
class ExcelDocument : IDocument
{
    public void Open()
    {
        Console.WriteLine("Microsoft Excel belgesi açılıyor...");
    }

    public void Close()
    {
        Console.WriteLine("Microsoft Excel belgesi kapatılıyor...");
    }
}

// Creator: Abstract class
abstract class DocumentCreator
{
    public abstract IDocument CreateDocument();
}

// Concrete Creator 1: WordBelgesiYaratici
class WordDocumentCreator : DocumentCreator
{
    public override IDocument CreateDocument()
    {
        return new WordDocument();
    }
}

// Concrete Creator 2: ExcelBelgesiYaratici
class ExcelDocumentCreator : DocumentCreator
{
    public override IDocument CreateDocument()
    {
        return new ExcelDocument();
    }
}

// Kullanıcıdan alınan bilgiye göre belge oluşturan sınıf
class DocumentManager
{
    public IDocument CreateDocument(string documentType)
    {
        DocumentCreator creator;

        // Kullanıcı girdisine göre uygun belge yaratıcıyı seç
        if (documentType == "Word")
        {
            creator = new WordDocumentCreator();
        }
        else if (documentType == "Excel")
        {
            creator = new ExcelDocumentCreator();
        }
        else
        {
            throw new ArgumentException("Geçersiz belge türü.");
        }

        // Belge yaratıcı üzerinden belge oluştur
        return creator.CreateDocument();
    }
}

class Program
{
    static void Main(string[] args)
    {
        DocumentManager manager = new DocumentManager();

        Console.WriteLine("Hangi belge türünü oluşturmak istiyorsunuz? (Word/Excel)");
        string documentType = Console.ReadLine();

        // Kullanıcı girdisine göre belge oluştur
        IDocument document = manager.CreateDocument(documentType);

        // Belgeyi aç ve kapat
        document.Open();
        document.Close();
    }
}
