using System.Collections;
using System.Collections.Generic;

namespace Music_Player.Helpers {
  public abstract class AList<T> : IList<T> {

    protected IList<T> items;

    //public AList(IEnumerable<T> items) => this.items = (IList<T>)items;

    public T this[int index] { get => this.items[index]; set => this.items[index] = value; }
    public int Count => this.items.Count;
    public bool IsReadOnly => this.items.IsReadOnly;

    public void Add(T item) => this.items.Add(item);
    public void Clear() => this.items.Clear();
    public bool Contains(T item) => this.items.Contains(item);
    public void CopyTo(T[] array, int arrayIndex) => this.items.CopyTo(array, arrayIndex);
    public IEnumerator<T> GetEnumerator() => this.items.GetEnumerator();
    public int IndexOf(T item) => this.items.IndexOf(item);
    public void Insert(int index, T item) => this.items.Insert(index, item);
    public bool Remove(T item) => this.items.Remove(item);
    public void RemoveAt(int index) => this.items.RemoveAt(index);
    IEnumerator IEnumerable.GetEnumerator() => this.items.GetEnumerator();
  }
}
