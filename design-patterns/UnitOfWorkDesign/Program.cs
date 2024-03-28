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


// Temel repository arabirimi
public interface IBaseRepository<T> where T : BaseEntity
{
    //DbSet<T> Table { get; }

}

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
  
}


public class WriteRepository<T> : BaseRepository<T>, IWriteRepository<T> where T : BaseEntity
{

    //private readonly MyDbContext _dbContext;



    public WriteRepository()
    {
        
    }



    public async Task<bool> AddAsync(T model)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddRangeAsync(List<T> datas)
    {
        throw new NotImplementedException();
    }

    public  bool Remove(T model)
    {
        throw new NotImplementedException();
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


public class ReadRepository<T> : BaseRepository<T>, IReadRepository<T> where T : BaseEntity
{
    //private readonly MyDbContext _dbContext;

    public ReadRepository()
    {

    }

    public IQueryable<T> GetAll(bool tracking = true)
    {
        throw new NotImplementedException();
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


public class UnitOfWork : IUnitOfWork
{
    public void Dispose()
    {

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

class Program
{
    static void Main(string[] args)
    {
        // UnitOfWork ve Repository Pattern temsil eden yapı kullanılmıştır


        IUnitOfWork unitOfWork = new UnitOfWork();

        var customerWriteRepository = unitOfWork.GetWriteRepository<Customer>();
        var customerReadRepository = unitOfWork.GetReadRepository<Customer>();

        customerWriteRepository.AddAsync(new Customer { Id = 1, Email = "ib@gmail.com", Name = "ilyas" });


        var customerList = customerReadRepository.GetAll();

        foreach (var item in customerList)
            Console.WriteLine(item);
        

        Console.WriteLine();

        Console.ReadLine();

    }
}