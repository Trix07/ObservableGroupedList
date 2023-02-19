using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Firell.Common.Collections;

/// <summary>
/// Implementation of a <see cref="SortedList{TKey, TValue}"/> implementing INotifyCollectionChanged to notify listeners when items get added, removed or the whole list is refreshed.
/// </summary>
/// <typeparam name="TKey">The type of key for the values in the sorted list.</typeparam>
/// <typeparam name="TValue">The type of values in the sorted list.</typeparam>
public class ObservableSortedList<TKey, TValue> : SortedList<TKey, TValue>, IEnumerable, INotifyPropertyChanged, INotifyCollectionChanged where TKey : notnull
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableSortedList{TKey, TValue}"/> class that is empty, has the default inital capacity, and uses the default <see cref="IComparer{T}"/>.
    /// </summary>
    public ObservableSortedList() : base()
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableSortedList{TKey, TValue}"/> class that is empty, has the specified inital capacity, and uses the default <see cref="IComparer{T}"/>.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public ObservableSortedList(int capacity) : base(capacity)
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableSortedList{TKey, TValue}"/> class that is empty, has the default inital capacity, and uses the specified <see cref="IComparer{T}"/>.
    /// </summary>
    /// <param name="comparer">The <see cref="IComparer{T}"/> implementation to use when comparing keys. -or- null to use the default <see cref="IComparer{T}"/> for the type of the key.</param>
    public ObservableSortedList(IComparer<TKey>? comparer) : base(comparer)
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableSortedList{TKey, TValue}"/> class that is empty, has the specified inital capacity, and uses the specified <see cref="IComparer{T}"/>.
    /// </summary>
    /// <param name="capacity">The initial number of elements that the <see cref="ObservableSortedList{TKey, TValue}"/> can contain.</param>
    /// <param name="comparer">The <see cref="IComparer{T}"/> implementation to use when comparing keys. -or- null to use the default <see cref="IComparer{T}"/> for the type of the key.</param>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public ObservableSortedList(int capacity, IComparer<TKey>? comparer) : base(capacity, comparer)
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableSortedList{TKey, TValue}"/> class that contains elements copied from the specified <see cref="IDictionary{TKey, TValue}"/>, has sufficient capacity to accommodate the number of elements copied, and uses the default <see cref="IComparer{T}"/>.
    /// </summary>
    /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/> whose elements are copied to the new <see cref="ObservableSortedList{TKey, TValue}"/>.</param>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    public ObservableSortedList(IDictionary<TKey, TValue> dictionary) : base(dictionary)
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableSortedList{TKey, TValue}"/> class that contains elements copied from the specified <see cref="IDictionary{TKey, TValue}"/>, has sufficient capacity to accommodate the number of elements copied, and uses the specified <see cref="IComparer{T}"/>.
    /// </summary>
    /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/> whose elements are copied to the new <see cref="ObservableSortedList{TKey, TValue}"/>.</param>
    /// <param name="comparer">The <see cref="IComparer{T}"/> implementation to use when comparing keys. -or- null to use the default <see cref="IComparer{T}"/> for the type of the key.</param>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    public ObservableSortedList(IDictionary<TKey, TValue> dictionary, IComparer<TKey>? comparer) : base(dictionary, comparer)
    {

    }

    /// <summary>
    /// Gets or sets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key whose value to get or set.</param>
    /// <returns>The value associated with the specified key. If the specified key is not found, a get operation thorws a <see cref="KeyNotFoundException"/> and a set operation creates a new element using the specified key.</returns>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="KeyNotFoundException"/>
    public new TValue this[TKey key]
    {
        get => base[key];
        set => base[key] = value;
    }

    /// <summary>
    /// Gets a collection containing the keys in the <see cref="ObservableSortedList{TKey, TValue}"/>, in sorted order.
    /// </summary>
    public new IList<TKey> Keys
    {
        get => base.Keys;
    }

    /// <summary>
    /// Gets a collection containing the values in the <see cref="ObservableSortedList{TKey, TValue}"/>.
    /// </summary>
    public new IList<TValue> Values
    {
        get => base.Values;
    }

    /// <summary>
    /// Gets the <see cref="IComparer{T}"/> for the <see cref="ObservableSortedList{TKey, TValue}"/>.
    /// </summary>
    public new IComparer<TKey> Comparer
    {
        get => base.Comparer;
    }

    /// <summary>
    /// Gets the number of key/value pairs contained in the <see cref="ObservableSortedList{TKey, TValue}"/>.
    /// </summary>
    public new int Count
    {
        get => base.Count;
    }

    /// <summary>
    /// Gets or sets the number of elements that the <see cref="ObservableSortedList{TKey, TValue}"/> can contain.
    /// </summary>
    public new int Capacity
    {
        get => base.Capacity;
        set => base.Capacity = value;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the <see cref="ObservableSortedList{TKey, TValue}"/>.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{out T}"/> of type <see cref="KeyValuePair{TKey, TValue}"/> for the <see cref="ObservableSortedList{TKey, TValue}"/>.</returns>
    public new IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return base.GetEnumerator();
    }

    /// <summary>
    /// Gets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key whose value to get.</param>
    /// <param name="value">When this method returns, the value associated with the specified key, if the key is not found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
    /// <returns><see langword="true"/> if the <see cref="ObservableSortedList{TKey, TValue}"/> contains an element with the specified key; otherwise, <see langword="false"/>.</returns>
    public new bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
    {
        return base.TryGetValue(key, out value);
    }

    /// <summary>
    /// Adds an element with the specified key and value into the <see cref="ObservableSortedList{TKey, TValue}"/>.
    /// </summary>
    /// <param name="key">The key of the element to add.</param>
    /// <param name="value">The value of the element to add. The value can be null for reference types.</param>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    public new void Add(TKey key, TValue value)
    {
        base.Add(key, value);
        int index = base.IndexOfKey(key);
        OnCollectionChanged(NotifyCollectionChangedAction.Add, value, index);
    }

    /// <summary>
    /// Removes the element with the specified key from the <see cref="ObservableSortedList{TKey, TValue}"/>.
    /// </summary>
    /// <param name="key">The key of the element to remove.</param>
    /// <returns><see langword="true"/> if the element is successfully removed; otherwise, <see langword="false"/>. This method also returns <see langword="false"/> if <see href="key"/> was not found in the <see cref="ObservableSortedList{TKey, TValue}"/>.</returns>
    /// <exception cref="ArgumentNullException"/>
    public new bool Remove(TKey key)
    {
        TValue removedItem = base[key];

        int index = base.IndexOfKey(key);
        bool result = base.Remove(key);
        OnCollectionChanged(NotifyCollectionChangedAction.Remove, removedItem, index);

        return result;
    }

    /// <summary>
    /// Removes the element at the specified index of the <see cref="ObservableSortedList{TKey, TValue}"/>.
    /// </summary>
    /// <param name="index">The zero-based index of the element to remove.</param>
    /// <exception cref="ArgumentOutOfRangeException"/>
    public new void RemoveAt(int index)
    {
        base.RemoveAt(index);
        OnCollectionChanged(NotifyCollectionChangedAction.Remove, null, index);
    }

    /// <summary>
    /// Determines whether the <see cref="ObservableSortedList{TKey, TValue}"/> contains a specific key.
    /// </summary>
    /// <param name="key">The key to locate in the <see cref="ObservableSortedList{TKey, TValue}"/>.</param>
    /// <returns><see langword="true"/> if the <see cref="ObservableSortedList{TKey, TValue}"/> contains an element with the specified key; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentNullException"/>
    public new bool ContainsKey(TKey key)
    {
        return base.ContainsKey(key);
    }

    /// <summary>
    /// Determines whether the <see cref="ObservableSortedList{TKey, TValue}"/> contains a specific value.
    /// </summary>
    /// <param name="value">The value to locate in the <see cref="ObservableSortedList{TKey, TValue}"/>. The value can be null for reference types.</param>
    /// <returns><see langword="true"/> if  the <see cref="ObservableSortedList{TKey, TValue}"/> contains an element with the specified value; otherwise, <see langword="false"/>.</returns>
    public new bool ContainsValue(TValue value)
    {
        return base.ContainsValue(value);
    }

    /// <summary>
    /// Removes all elements from the <see cref="ObservableSortedList{TKey, TValue}"/>.
    /// </summary>
    public new void Clear()
    {
        base.Clear();
        OnCollectionReset();
    }

    /// <summary>
    /// Raise PropertyChanged event to any listeners.
    /// </summary>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        PropertyChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Raise CollectionChanged event to any listeners. Properties/methods modifying this ObservableCollection will raise a collection changed event through this virtual method.
    /// </summary>
    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        OnCountPropertyChanged();
        OnIndexerPropertyChanged();
        CollectionChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Helper to raise a PropertyChanged event for the Count property.
    /// </summary>
    private void OnCountPropertyChanged()
    {
        OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
    }

    /// <summary>
    /// Helper to raise a PropertyChanged event for the Indexer property.
    /// </summary>
    private void OnIndexerPropertyChanged()
    {
        OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
    }

    /// <summary>
    /// Helper to raise CollectionChanged event to any listeners.
    /// </summary>
    private void OnCollectionChanged(NotifyCollectionChangedAction action, object? item, int index)
    {
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
    }

    /// <summary>
    /// Helper to raise CollectionChanged event with action <see cref="NotifyCollectionChangedAction.Reset"/> to any listeners.
    /// </summary>
    private void OnCollectionReset()
    {
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
}
