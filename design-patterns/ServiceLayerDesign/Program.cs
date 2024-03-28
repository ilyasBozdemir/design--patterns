// Veri modeli
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// Hizmet katmanı arayüzü
public interface IProductService
{
    IEnumerable<Product> GetAllProducts();
    Product GetProductById(int id);
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(int id);
}

// Hizmet katmanı uygulaması
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _productRepository.GetAll();
    }

    public Product GetProductById(int id)
    {
        return _productRepository.GetById(id);
    }

    public void AddProduct(Product product)
    {
        _productRepository.Add(product);
    }

    public void UpdateProduct(Product product)
    {
        _productRepository.Update(product);
    }

    public void DeleteProduct(int id)
    {
        _productRepository.Delete(id);
    }
}

// Veri erişim katmanı arayüzü
public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    Product GetById(int id);
    void Add(Product product);
    void Update(Product product);
    void Delete(int id);
}

// Veri erişim katmanı uygulaması
public class ProductRepository : IProductRepository
{
    // Gerçek veritabanı erişim kodları burada olacaktır.
    // Bu örnekte basitlik için bir simülasyon kullanıyoruz.

    private List<Product> _products = new List<Product>();

    public IEnumerable<Product> GetAll()
    {
        return _products;
    }

    public Product GetById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public void Add(Product product)
    {
        _products.Add(product);
    }

    public void Update(Product product)
    {
        var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
        }
    }

    public void Delete(int id)
    {
        var productToRemove = _products.FirstOrDefault(p => p.Id == id);
        if (productToRemove != null)
        {
            _products.Remove(productToRemove);
        }
    }
}

// Kullanım örneği
class Program
{
    static void Main(string[] args)
    {
        // Hizmet katmanı kullanımı
        IProductService productService = new ProductService(new ProductRepository());
        var allProducts = productService.GetAllProducts();

        // Yeni ürün ekleme
        productService.AddProduct(new Product { Id = 1, Name = "Ürün 1", Price = 10.0m });

        // Ürün güncelleme
        var productToUpdate = productService.GetProductById(1);
        if (productToUpdate != null)
        {
            productToUpdate.Name = "Güncellenmiş Ürün 1";
            productService.UpdateProduct(productToUpdate);
        }

        // Ürün silme
        productService.DeleteProduct(1);
    }
}
