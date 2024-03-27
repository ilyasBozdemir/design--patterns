using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

// BaseEntity sınıfı
public class BaseEntity
{
    public int Id { get; set; }
}


public class Customer : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
}

public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}

public class DbSet<T> where T : BaseEntity
{
    private List<T> _entities;

    public DbSet()
    {
        _entities = new List<T>();
    }

    // Tüm varlıkları döndür
    public IQueryable<T> AsQueryable()
    {
        return _entities.AsQueryable();
    }

    // Belirli bir varlığı ekle
    public void Add(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        _entities.Add(entity);
    }

    // Belirli bir varlığı kaldır
    public void Remove(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        _entities.Remove(entity);
    }
}


public class MyDbContext
{
    private static MyDbContext _instance;
    private static readonly object _lock = new object();
    public List<Customer> Customers { get; set; }
    public List<Product> Products { get; set; }


    public static MyDbContext GetInstance()
    {
    
        lock (_lock)
        {
            if (_instance == null)
                _instance = new MyDbContext();
            
            return _instance;
        }
    }

    public MyDbContext()
    {
        Customers = new List<Customer>();
        Products = new List<Product>();
    }


    public DbSet<T> Set<T>() where T : BaseEntity
    {
        return new DbSet<T>();
    }
}


// Temel repository arabirimi
public interface IBaseRepository<T> where T : BaseEntity
{
    List<T> Table { get; }
}

public interface IReadRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    IQueryable<T> GetAll(bool tracking = true);
    IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
    Task<T> GetByIdAsync(string id, bool tracking = true);


    //Task<Paginate<T>> GetPaginatedAsync(int pageIndex, int pageSize, bool tracking = true);
    //Task<Paginate<T>> GetPaginatedAsync(Expression<Func<T, bool>> filter, int pageIndex, int pageSize, bool tracking = true);
}

public interface IWriteRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    Task<bool> AddAsync(T model);
    Task<bool> AddRangeAsync(List<T> datas);
    bool Remove(T model);
    bool RemoveRange(List<T> datas);
    Task<bool> RemoveAsync(int id);
    bool Update(T model);
    Task<int> SaveAsync();
}


public interface IUnitOfWork : IDisposable
{
    IReadRepository<T> GetReadRepository<T>() where T : BaseEntity;
    IWriteRepository<T> GetWriteRepository<T>() where T : BaseEntity;
}

public class UnitOfWork : IUnitOfWork
{
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public IReadRepository<T> GetReadRepository<T>() where T : BaseEntity
    {
       return new ReadRepository<T>();
    }

    public IWriteRepository<T> GetWriteRepository<T>() where T : BaseEntity
    {
        return new WriteRepository<T>();
    }
}


public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
{

    private readonly MyDbContext _dbContext;


    public List<T> Table => GetDbSet();

    public WriteRepository()
    {
        _dbContext = new MyDbContext();
    }

    private List<T> GetDbSet()
    {
        if (typeof(T) == typeof(Customer))
            return _dbContext.Customers.Cast<T>().ToList();
        
        else if (typeof(T) == typeof(Product))
            return _dbContext.Products.Cast<T>().ToList();
        

        else
            throw new NotImplementedException("Table not implemented for this entity type");
        
    }

    public async Task<bool> AddAsync(T model)
    {
        try
        {
            await Task.Delay(0); 
            GetDbSet().Add(model);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> AddRangeAsync(List<T> datas)
    {
        throw new NotImplementedException();
    }

    public  bool Remove(T model)
    {
        try
        {
            GetDbSet().Remove(model);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> RemoveAsync(int id)
    {
       throw new NotImplementedException();
    }



    public bool RemoveRange(List<T> datas)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveAsync()
    {
        throw new NotImplementedException();
    }

    public bool Update(T model)
    {
        throw new NotImplementedException();
    }
}

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly MyDbContext _dbContext;
    public List<T> Table => GetDbSet();

   
    private List<T> GetDbSet()
    {
        if (typeof(T) == typeof(Customer))
            return _dbContext.Customers.Cast<T>().ToList();

        else if (typeof(T) == typeof(Product))
            return _dbContext.Products.Cast<T>().ToList();


        else
            throw new NotImplementedException("Table not implemented for this entity type");

    }


    public ReadRepository()
    {
        _dbContext = new MyDbContext();
    }

    public IQueryable<T> GetAll(bool tracking = true)
    {
        return Table.AsQueryable();
    }

    public Task<T> GetByIdAsync(string id, bool tracking = true)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
    {
        throw new NotImplementedException();
    }
}


class Program
{
    static void Main(string[] args)
    {
        // => UnitOfWork ve Repository Pattern temsil eden yapı kullanılmıştır

        IUnitOfWork unitOfWork = new UnitOfWork();

        var customerWriteRepository  = unitOfWork.GetWriteRepository<Customer>();
        var customerReadRepository  = unitOfWork.GetReadRepository<Customer>();



        customerWriteRepository.AddAsync(new Customer { Id = 1, Email = "ib@gmail.com", Name = "ilyas" });


        var customerList = customerReadRepository.GetAll();

        foreach (var item in customerList)
        {
            Console.WriteLine(item);
        }

    }
}