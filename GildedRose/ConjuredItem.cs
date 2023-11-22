namespace GildedRoseKata
{
    public class ConjuredItem : UpdatableItem
    {
        public ConjuredItem(Item item) : base(item)  { }
        public override void Update()
        {
            item.Quality -= 2;
            item.SellIn--;
        }
    }
}