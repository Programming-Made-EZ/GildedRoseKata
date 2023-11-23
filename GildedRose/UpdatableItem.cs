namespace GildedRoseKata;

public class UpdatableItem
{
    public Item item;

    public UpdatableItem(Item item)
    {
        this.item = item;
    }

    public virtual void Update() {
        return;
    }
}
