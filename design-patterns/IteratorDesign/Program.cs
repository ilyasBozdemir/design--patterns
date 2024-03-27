using System;
using System.Collections;

// Iterator arabirimi
interface IIterator
{
    bool HasNext();
    object Next();
}

// Koleksiyon arabirimi
interface IAggregate
{
    IIterator GetIterator();
}

// Koleksiyon sınıfı
class MyCollection : IAggregate
{
    private ArrayList _items = new ArrayList();

    public void AddItem(object item)
    {
        _items.Add(item);
    }

    public IIterator GetIterator()
    {
        return new MyIterator(this);
    }

    public object GetItem(int index)
    {
        return _items[index];
    }

    public int Count
    {
        get { return _items.Count; }
    }
}

// Iterator sınıfı
class MyIterator : IIterator
{
    private MyCollection _collection;
    private int _currentIndex = 0;

    public MyIterator(MyCollection collection)
    {
        _collection = collection;
    }

    public bool HasNext()
    {
        return _currentIndex < _collection.Count;
    }

    public object Next()
    {
        if (HasNext())
        {
            object nextItem = _collection.GetItem(_currentIndex);
            _currentIndex++;
            return nextItem;
        }
        else
        {
            throw new InvalidOperationException("Iterator has reached the end of collection.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        MyCollection collection = new MyCollection();
        collection.AddItem("Item 1");
        collection.AddItem("Item 2");
        collection.AddItem("Item 3");

        IIterator iterator = collection.GetIterator();

        while (iterator.HasNext())
        {
            object item = iterator.Next();
            Console.WriteLine(item);
        }
    }
}
