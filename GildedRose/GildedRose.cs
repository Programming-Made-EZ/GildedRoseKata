using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GildedRoseKata;

public class GildedRose
{
    private readonly IList<Item> _items;

    public GildedRose(IList<Item> items)
    {
        _items = items;
    }

    public Dictionary<string, Func<Item, UpdatableItem>> UpdateableItemsTable = new() {
           { "Aged Brie", (item) => new AgedBrieItem(item) },
           { "Backstage passes to a TAFKAL80ETC concert", (item) => new BackstagePassItem(item) },
           { "Sulfuras, Hand of Ragnaros", (item) => new UpdatableItem(item) },
           { "Default", (item) => new NormalItem(item) }
    };

    public void UpdateQuality()
    {
        foreach (var item in _items)
        {
            CreateUpdatableItem(item).Update();
        }
    }

    public UpdatableItem CreateUpdatableItem(Item item)
    {
        return UpdateableItemsTable.First((kvp) => kvp.Key.Equals(item.Name) || kvp.Key.Equals("Default")).Value(item);
        }
}