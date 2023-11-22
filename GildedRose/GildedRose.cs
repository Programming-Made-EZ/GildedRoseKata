using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRoseKata;

public partial class GildedRose
{
    private readonly IList<Item> _items;

    public GildedRose(IList<Item> items, Dictionary<string, Func<Item, UpdatableItem>> updateableItemsTable)
    {
        if (updateableItemsTable is null) throw new ArgumentException();
        else UpdateableItemsTable = updateableItemsTable;
        _items = items;
    }

    public Dictionary<string, Func<Item, UpdatableItem>> UpdateableItemsTable;

    public void UpdateQuality()
    {
        foreach (var item in _items)
        {
            var typeItem = UpdatableItemFactory(item);
            typeItem.Update();
        }
    }

    public UpdatableItem UpdatableItemFactory(Item item) {
        return UpdateableItemsTable
            .FirstOrDefault(updatableItem => updatableItem.Key.Equals(item.Name) || updatableItem.Key.Equals("Default"))
            .Value(item);
    }
}