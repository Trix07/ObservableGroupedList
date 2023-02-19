namespace Firell.Common.Collections;

/// <summary>
/// An observable group that associates a key to an <see cref="ObservableSortedList{TKey, TValue}"/>.
/// </summary>
/// <typeparam name="TValueKey">The type of key for the values in the group.</typeparam>
/// <typeparam name="TValue">The type of values in the group.</typeparam>
public class ObservableGroup<TValueKey, TValue> : ObservableSortedList<TValueKey, TValue> where TValueKey : notnull
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableGroup{TValueKey, TValue}"/> class with the specified group key that is empty, and uses the default <see cref="IComparer{T}"/>.
    /// </summary>
    public ObservableGroup(object key) : this(key, new Dictionary<TValueKey, TValue>(), null)
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableGroup{TValueKey, TValue}"/> class with the specified group key that is empty, and uses the specified <see cref="IComparer{T}"/>.
    /// </summary>
    public ObservableGroup(object key, IComparer<TValueKey>? comparer) : this(key, new Dictionary<TValueKey, TValue>(), comparer)
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableGroup{TValueKey, TValue}"/> class with the specified group key that contains elements copied from the specified <see cref="IDictionary{TValueKey, TValue}"/>, and uses the default <see cref="IComparer{T}"/>.
    /// </summary>
    public ObservableGroup(object key, IDictionary<TValueKey, TValue> dictionary) : this(key, dictionary, null)
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableGroup{TValueKey, TValue}"/> class with the specified group key that contains elements copied from the specified <see cref="IDictionary{TValueKey, TValue}"/>, and uses the specified <see cref="IComparer{T}"/>.
    /// </summary>
    public ObservableGroup(object key, IDictionary<TValueKey, TValue> dictionary, IComparer<TValueKey>? comparer) : base(dictionary, comparer)
    {
        Key = key;
    }

    /// <summary>
    /// A unique key that identifies the group for sorting and grouping purposes.
    /// </summary>
    public object Key { get; set; }
}
