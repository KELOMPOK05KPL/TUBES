using System;
using System.Collections.Generic;

public class Repository<T>
{
    private List<T> items = new List<T>();

    public void Add(T item)
    {
        items.Add(item);
    }

    public List<T> GetAll()
    {
        return items;
    }

    public T GetById(Func<T, bool> predicate)
    {
        return items.Find(new Predicate<T>(predicate)); // Konversi eksplisit
    }

}
