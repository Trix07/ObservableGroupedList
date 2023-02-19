using System.Collections.Specialized;

namespace Firell.Common.Collections;

/// <summary>
/// An observable grouped list that associates a key to an <see cref="ObservableGroup{TValueKey, TValue}"/>.
/// </summary>
/// <typeparam name="TGroupKey">The type of key for the group.</typeparam>
/// <typeparam name="TValueKey">The type of key for the values in the group.</typeparam>
/// <typeparam name="TValue">The type of values in the group.</typeparam>
public class ObservableGroupedList<TGroupKey, TValueKey, TValue> : ObservableSortedList<TGroupKey, ObservableGroup<TValueKey, TValue>> where TGroupKey : notnull where TValueKey : notnull
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableGroupedList{TGroupKey, TValueKey, TValue}"/> class with the specified key selector key that is empty, and uses the default <see cref="IComparer{T}"/> for groups and items.
    /// </summary>
    public ObservableGroupedList(Func<TValue, TGroupKey> keySelector) : this(keySelector, new Dictionary<TValueKey, TValue>(), null, null)
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableGroupedList{TGroupKey, TValueKey, TValue}"/> class with the specified key selector key that is empty, and uses the specified <see cref="IComparer{T}"/> for groups and items.
    /// </summary>
    public ObservableGroupedList(Func<TValue, TGroupKey> keySelector, IComparer<TGroupKey>? groupComparer, IComparer<TValueKey>? itemComparer) : this(keySelector, new Dictionary<TValueKey, TValue>(), groupComparer, itemComparer)
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableGroupedList{TGroupKey, TValueKey, TValue}"/> class with the specified key selector key that contains elements copied from the specified <see cref="IDictionary{TValueKey, TValue}"/>, and uses the default <see cref="IComparer{T}"/> for groups and items.
    /// </summary>
    public ObservableGroupedList(Func<TValue, TGroupKey> keySelector, IDictionary<TValueKey, TValue> dictionary) : this(keySelector, dictionary, null, null)
    {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ObservableGroupedList{TGroupKey, TValueKey, TValue}"/> class with the specified key selector key that contains elements copied from the specified <see cref="IDictionary{TValueKey, TValue}"/>, and uses the specified <see cref="IComparer{T}"/> for groups and items.
    /// </summary>
    public ObservableGroupedList(Func<TValue, TGroupKey> keySelector, IDictionary<TValueKey, TValue> dictionary, IComparer<TGroupKey>? groupComparer, IComparer<TValueKey>? itemComparer) : base(groupComparer)
    {
        KeySelector = keySelector;
        ItemComparer = itemComparer ?? Comparer<TValueKey>.Default;

        foreach (KeyValuePair<TValueKey, TValue> item in dictionary)
        {
            Add(item.Key, item.Value);
        }
    }

    protected Func<TValue, TGroupKey> KeySelector { get; }

    /// <summary>
    /// Gets the item <see cref="IComparer{T}"/> for all <see cref="ObservableGroup{TValueKey, TValue}"/>.
    /// </summary>
    protected IComparer<TValueKey> ItemComparer { get; }

    /// <summary>
    /// Gets the last effected group, used for optimisation for when items are modified in key order.
    /// </summary>
    protected ObservableGroup<TValueKey, TValue>? LastEffectedGroup { get; set; }

    /// <summary>
    /// Gets a collection containing the keys in the <see cref="ObservableGroupedList{TGroupKey, TValueKey, TValue}"/>, in sorted order.
    /// </summary>
    public new IEnumerable<TGroupKey> Keys
    {
        get => base.Keys;
    }

    /// <summary>
    /// Gets a collection containing the values in the <see cref="ObservableGroupedList{TGroupKey, TValueKey, TValue}"/>.
    /// </summary>
    public new IEnumerable<ObservableGroup<TValueKey, TValue>> Values
    {
        get => base.Values;
    }

    /// <summary>
    /// Gets a collection containing the group keys in the <see cref="ObservableGroupedList{TGroupKey, TValueKey, TValue}"/>, in sorted order.
    /// </summary>
    public IEnumerable<TValueKey> GroupKeys
    {
        get => this.SelectMany(x => x.Value.Keys);
    }

    /// <summary>
    /// Gets a collection containing the group values in the <see cref="ObservableGroupedList{TGroupKey, TValueKey, TValue}"/>.
    /// </summary>
    public IEnumerable<TValue> GroupValues
    {
        get => this.SelectMany(x => x.Value.Values);
    }

    /// <summary>
    /// Gets a dictionary of all the groups in the <see cref="ObservableGroupedList{TGroupKey, TValueKey, TValue}"/>.
    /// </summary>
    public IDictionary<TValueKey, TValue> GroupDictionary
    {
        get => this.SelectMany(x => x.Value).ToDictionary(key => key.Key, value => value.Value);
    }

    /// <summary>
    /// Adds an element with the specified key and value into the appropriate group; if no suitable group is found, one is created.
    /// </summary>
    /// <param name="key">The key of the element to add.</param>
    /// <param name="value">The value of the element to add. The value can be null for reference types.</param>
    public void Add(TValueKey key, TValue value)
    {
        TGroupKey groupKey = KeySelector(value);

        FindOrCreateGroup(groupKey).Add(key, value);
    }

    /// <summary>
    /// Removes the element with the specified key from the appropriate group; if group is empty afterwards it's removed aswell.
    /// </summary>
    /// <param name="key">The key of the element to remove</param>
    /// <returns><see langword="true"/> if the element is successfully removed; otherwise, <see langword="false"/>. This method also returns <see langword="false"/> if <see href="key"/> was not found in the <see cref="ObservableGroupedList{TGroupKey, TValueKey, TValue}"/>.</returns>
    public bool Remove(TValueKey key)
    {
        // Find group key from value key.
        KeyValuePair<TValueKey, TValue> item = GroupDictionary.FirstOrDefault(x => x.Key.Equals(key));
        TGroupKey groupKey = KeySelector(item.Value);

        bool result = false;

        // Find and remove item from group if found.
        ObservableGroup<TValueKey, TValue>? group = FindGroup(groupKey);
        if (group != null)
        {
            result = group.Remove(key);
        }

        // Remove group aswell if all items have been removed.
        if (group != null && group.Count == 0)
        {
            LastEffectedGroup = null;
            group.CollectionChanged -= Group_CollectionChanged;
            Remove(groupKey);
        }

        return result;
    }

    /// <summary>
    /// Determines whether the <see cref="ObservableGroupedList{TGroupKey, TValueKey, TValue}"/> contains a specific key.
    /// </summary>
    /// <param name="key">The key to locate in the <see cref="ObservableGroupedList{TGroupKey, TValueKey, TValue}"/>.</param>
    /// <returns><see langword="true"/> if the <see cref="ObservableGroupedList{TGroupKey, TValueKey, TValue}"/> contains an element with the specified key; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentNullException"/>
    public bool ContainsKey(TValueKey key)
    {
        return GroupDictionary.ContainsKey(key);
    }

    /// <summary>
    /// Modifies the current collection to contain only elements that are present in the specified collection, duplicates are ignored.
    /// </summary>
    /// <param name="other">The collection to compare to the current collection.</param>
    /// <param name="keySelector">The key selector for removing/adding items.</param>
    public void ReplaceWith(IEnumerable<TValue> other, Func<TValue, TValueKey> keySelector)
    {
        IEnumerable<TValue> values = GroupValues;

        HashSet<TValue> itemsToRemove = new HashSet<TValue>(values);
        itemsToRemove.ExceptWith(other);
        foreach (TValue? item in itemsToRemove)
        {
            TValueKey key = keySelector(item);
            Remove(key);
        }

        HashSet<TValue> itemsToAdd = new HashSet<TValue>(values);
        itemsToAdd.SymmetricExceptWith(other);
        foreach (TValue? item in itemsToAdd)
        {
            TValueKey key = keySelector(item);
            Add(key, item);
        }
    }

    /// <summary>
    /// Attempts to find a group based on the specified group key.
    /// </summary>
    /// <param name="key">The key of the group to find.</param>
    /// <returns>The <see cref="ObservableGroup{TValueKey, TValue}"/> that has the specified key; otherwise <see langword="null"/>.</returns>
    private ObservableGroup<TValueKey, TValue>? FindGroup(TGroupKey key)
    {
        if (LastEffectedGroup != null && LastEffectedGroup.Key.Equals(key))
        {
            return LastEffectedGroup;
        }

        if (TryGetValue(key, out ObservableGroup<TValueKey, TValue>? group))
        {
            LastEffectedGroup = group;
            return group;
        }

        return default;
    }

    /// <summary>
    /// Attempts to find a group based on the specified group key; otherwise creates group with specified group key.
    /// </summary>
    /// <param name="key">The key of the group to find.</param>
    /// <returns>The <see cref="ObservableGroup{TValueKey, TValue}"/> that has the specified key; otherwise the newly created <see cref="ObservableGroup{TValueKey, TValue}"/>.</returns>
    private ObservableGroup<TValueKey, TValue> FindOrCreateGroup(TGroupKey key)
    {
        ObservableGroup<TValueKey, TValue>? group = FindGroup(key);
        if (group != null)
        {
            return group;
        }

        // No suitable group found; create new with specified key and subscribe to collection changed event.
        group = new ObservableGroup<TValueKey, TValue>(key, ItemComparer);
        group.CollectionChanged += Group_CollectionChanged;
        Add(key, group);

        LastEffectedGroup = group;
        return group;
    }

    /// <summary>
    /// Raises CollectionChanged event from <see cref="ObservableGroup{TValueKey, TValue}"/>.
    /// </summary>
    private void Group_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        // Propagate OnCollectionChanged events from groups.
        OnCollectionChanged(e);
    }
}
