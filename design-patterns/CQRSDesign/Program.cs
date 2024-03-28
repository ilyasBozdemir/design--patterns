// Komutlar
public class AddProductCommand
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class UpdateProductCommand
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class DeleteProductCommand
{
    public int Id { get; set; }
}

// Komut işleyici
public class ProductCommandHandler
{
    private readonly List<Product> _products;

    public ProductCommandHandler(List<Product> products)
    {
        _products = products;
    }

    public void Handle(AddProductCommand command)
    {
        var newProduct = new Product
        {
            Id = _products.Count + 1,
            Name = command.Name,
            Price = command.Price
        };
        _products.Add(newProduct);
    }

    public void Handle(UpdateProductCommand command)
    {
        var productToUpdate = _products.FirstOrDefault(p => p.Id == command.Id);
        if (productToUpdate != null)
        {
            productToUpdate.Name = command.Name;
            productToUpdate.Price = command.Price;
        }
    }

    public void Handle(DeleteProductCommand command)
    {
        var productToRemove = _products.FirstOrDefault(p => p.Id == command.Id);
        if (productToRemove != null)
        {
            _products.Remove(productToRemove);
        }
    }
}

// Sorgular
public class GetProductByIdQuery
{
    public int Id { get; set; }
}

public class GetProductsQuery
{
}

// Sorgu işleyici
public class ProductQueryHandler
{
    private readonly List<Product> _products;

    public ProductQueryHandler(List<Product> products)
    {
        _products = products;
    }

    public Product Handle(GetProductByIdQuery query)
    {
        return _products.FirstOrDefault(p => p.Id == query.Id);
    }

    public IEnumerable<Product> Handle(GetProductsQuery query)
    {
        return _products;
    }
}

// Ürün modeli
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        var products = new List<Product>();
        var productCommandHandler = new ProductCommandHandler(products);
        var productQueryHandler = new ProductQueryHandler(products);

        // Ürün ekleme komutu
        var addCommand = new AddProductCommand { Name = "Ürün 1", Price = 10.0m };
        productCommandHandler.Handle(addCommand);

        // Ürün ekleme komutu
        var addCommand2 = new AddProductCommand { Name = "Ürün 2", Price = 15.0m };
        productCommandHandler.Handle(addCommand2);

        // Ürün ekleme komutu
        var addCommand3 = new AddProductCommand { Name = "Ürün 3", Price = 45.0m };
        productCommandHandler.Handle(addCommand2);

        // Ürün güncelleme komutu
        var updateCommand = new UpdateProductCommand { Id = 1, Name = "Güncellenmiş Ürün 1", Price = 15.0m };
        productCommandHandler.Handle(updateCommand);

        // Ürün silme komutu
        var deleteCommand = new DeleteProductCommand { Id = 2 };
        productCommandHandler.Handle(deleteCommand);

        // Ürün sorgusu
        var getProductQuery = new GetProductByIdQuery { Id = 1 };
        var product = productQueryHandler.Handle(getProductQuery);
        Console.WriteLine("GetProductByIdQuery ile");
        Console.WriteLine();
        if (product != null)
            Console.WriteLine($"Ürün Id: {product.Id}, Ürün Adı: {product.Name}, Fiyatı: {product.Price}");
        
        else
            Console.WriteLine($"Id:{getProductQuery.Id} olan ürün bulunamadı.");

        

        var getAllProductQuery = new GetProductsQuery {  };
        var allProducts = productQueryHandler.Handle(getAllProductQuery);
      
        Console.WriteLine();
        Console.WriteLine("GetProductsQuery ile");
        Console.WriteLine();

        foreach (var Product in allProducts)
        {
            if (Product != null)
            {
                Console.WriteLine($"Ürün Adı: {Product.Name}, Fiyatı: {Product.Price}");
            }
            else
            {
                Console.WriteLine("Ürün bulunamadı.");
            }
        }

    }
}
