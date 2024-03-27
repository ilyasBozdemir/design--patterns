// Temel paket arayüzü
interface IPackage
{
    string GetDescription();
}

// Temel paket sınıfı
class BasePackage : IPackage
{
    public string GetDescription()
    {
        return "";
    }
}

// Decorator sınıfı
abstract class PackageDecorator : IPackage
{
    protected IPackage _package;

    public PackageDecorator(IPackage package)
    {
        _package = package;
    }

    public virtual string GetDescription()
    {
        return _package.GetDescription();
    }
}

// Standart paket dekoratörü
class StandardPackageDecorator : PackageDecorator
{
    public StandardPackageDecorator(IPackage package) : base(package)
    {
    }

    public override string GetDescription()
    {
        return base.GetDescription() + "\nTüm Temel Paket Özellikleri, Tedarikçi Yönetimi, E-Ticaret Entegrasyonları, Kargo Yönetimi.";
    }
}

// Profesyonel paket dekoratörü
class ProfessionalPackageDecorator : PackageDecorator
{
    public ProfessionalPackageDecorator(IPackage package) : base(package)
    {
    }

    public override string GetDescription()
    {
        return base.GetDescription() + "\nTüm Standart Paket Özellikleri, Fatura Yönetimi, Entegrasyon ve Uygulamalar, Mali Müşavir Entegrasyonu.";
    }
}

// Kurumsal paket dekoratörü
class EnterprisePackageDecorator : PackageDecorator
{
    public EnterprisePackageDecorator(IPackage package) : base(package)
    {
    }

    public override string GetDescription()
    {
        return base.GetDescription() + "\nTüm Profesyonel Paket Özellikleri, Mutabakat Entegrasyonları, Bayi Yönetimi, Gelişmiş E-Ticaret ve Kargo Yönetimi.";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Temel paket oluştur
        IPackage package = new BasePackage();

        // Standart paket özelliklerini ekleyerek paketi genişlet
        package = new StandardPackageDecorator(package);

        // Profesyonel paket özelliklerini ekleyerek paketi genişlet
        package = new ProfessionalPackageDecorator(package);

        // Kurumsal paket özelliklerini ekleyerek paketi genişlet
        package = new EnterprisePackageDecorator(package);

        // Paket özelliklerini göster
        Console.WriteLine(package.GetDescription());

        Console.ReadLine();
    }
}
