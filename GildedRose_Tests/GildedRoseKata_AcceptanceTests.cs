using GildedRoseKata;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;

namespace GildedRose_Tests
{
    public class GildedRose_AcceptanceTests
    {
        [Fact]
        // - At the end of each day, our system lowers the value of SellIn for every item
        public void UpdateQuality_Should_ReduceSellInTo4WhenInitialzedAs5()
        {
            // Given the item has sellIn of 5
            Item fooItem = CreateItem("Foo", 5, 5);
            var SUT = CreateGildedRose(fooItem);

            // When the Quality Update occurs
            SUT.UpdateQuality();

            // Then the sellIn is 4
            Assert.Equal(4, fooItem.SellIn);
        }

        [Fact]
        // - At the end of each day, our system lowers the value of Quality for every item
        public void UpdateQuality_Should_ReduceQualityTo4WhenInitialzedAs5()
        {
            // Given the item has a quality of 5
            var fooItem = CreateItem("Foo", 5, 5);
            var SUT = CreateGildedRose(fooItem);

            // When the Quality Update occurs
            SUT.UpdateQuality();

            // Then the quality value is 4
            Assert.Equal(4, fooItem.Quality);
        }

        [Fact]
        // - Once the sell-by date has passed, Quality degrades twice as fast
        public void UpdateQuality_Should_ReduceQualityBy2WhenSellInIsLessThanOrEqualToZero()
        {
            // Given the sell-by date has passed
            var zeroSellInItem = CreateItem("Foo", 3, -1);
            var SUT = CreateGildedRose(zeroSellInItem);

            // When the Quality Update occurs
            SUT.UpdateQuality();

            // Then the quality degraded twice as fast
            Assert.Equal(1, zeroSellInItem.Quality);
        }

        [Fact]
        // - The Quality of an item is never negative
        public void UpdateQuality_Should_KeepZeroQuality_When_QualityIsAlreadyZero()
        {
            // Given the quality of an item is 0
            var zeroQualityItem = CreateItem("Foo", 0, 5);
            var SUT = CreateGildedRose(zeroQualityItem);

            // When the Quality Update occurs
            SUT.UpdateQuality();

            // Then the quality is still 0
            Assert.Equal(0, zeroQualityItem.Quality);
        }

        [Fact]
        //- "Aged Brie" actually increases in Quality the older it gets
        public void UpdateQuality_Should_IncreseQualityByOne_When_ItemIsAgedBrie()
        {
            // Given the item name is "Aged Brie" and it's quality is 1
            var agedBrieItem = CreateItem("Aged Brie", 1, 5);
            var SUT = CreateGildedRose(agedBrieItem);

            // When the Quality Update occurs
            SUT.UpdateQuality();

            // Then the quality of the aged brie should be 2
            Assert.Equal(2, agedBrieItem.Quality);
        }

        [Fact]
        // - The Quality of an item is never more than 50
        public void UpdateQuality_Should_KeepQualityAt50_When_AgedBrieQualityIsAlready50()
        {
            // Given the quality of an item is already 50
            var agedBrieItem = CreateItem("Aged Brie", 50, 5);
            var SUT = CreateGildedRose(agedBrieItem);

            // When the Quality Update occurs
            SUT.UpdateQuality();

            // Then the quality of the item should remain 50
            Assert.Equal(50, agedBrieItem.Quality);
        }

        [Fact]
        //- "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
        public void UpdateQuality_Should_KeepQualityAt10_When_SulfurasQualityIsAlready10()
        {
            // Given the item is named "Sulfuras" and has a quality of 10
            var sulfurasItem = CreateItem("Sulfuras, Hand of Ragnaros", 10, 5);
            var SUT = CreateGildedRose(sulfurasItem);

            // When the quality update occurs
            SUT.UpdateQuality();

            // Then the quality of the Sulfuras is still 10
            Assert.Equal(10, sulfurasItem.Quality);
        }

        [Fact]
        //- "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
        public void UpdateQuality_Should_KeepSellInAt10_When_SulfurasSellInIsAlready10()
        {
            // Given the item is named "Sulfuras" and has a quality of 10
            var sulfurasItem = CreateItem("Sulfuras, Hand of Ragnaros", 5, 10);
            var SUT = CreateGildedRose(sulfurasItem);

            // When the quality update occurs
            SUT.UpdateQuality();

            // Then the quality of the Sulfuras is still 10
            Assert.Equal(10, sulfurasItem.SellIn);
        }

        [Theory]
        [InlineData(20)]
        [InlineData(11)]
        /*- "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches; 
         Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less
         but Quality drops to 0 after the concert */
        public void UpdateQuality_Should_IncreseQualityByOne_When_ItemIsBackstagePasses(int sellIn)
        {
            // Given the item is Backstage Passes and the Sell In value is greater than 10
            var backstagePassesItem = CreateItem("Backstage passes to a TAFKAL80ETC concert", 10, sellIn);
            var SUT = CreateGildedRose(backstagePassesItem);

            // When the quality update occurs
            SUT.UpdateQuality();

            // Then the quality of the Backstage Passes increases by 1
            Assert.Equal(11, backstagePassesItem.Quality);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(8)]
        [InlineData(6)]
        /*- "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches; 
         Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less
         but Quality drops to 0 after the concert */
        public void UpdateQuality_Should_IncreseQualityByTwo_When_ItemIsBackstagePassesAndSellInIsLessThan11ButGreaterThan5(int sellIn)
        {
            // Given the item is Backstage Passes and the Sell In value is greater than 10
            var backstagePassesItem = CreateItem("Backstage passes to a TAFKAL80ETC concert", 10, sellIn);
            var SUT = CreateGildedRose(backstagePassesItem);

            // When the quality update occurs
            SUT.UpdateQuality();

            // Then the quality of the Backstage Passes increases by 1
            Assert.Equal(12, backstagePassesItem.Quality);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(1)]
        [InlineData(3)]
        /*- "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches; 
         Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less
         but Quality drops to 0 after the concert */
        public void UpdateQuality_Should_IncreseQualityByThree_When_ItemIsBackstagePassesAndSellInIsLessThan6ButGreaterThan1(int sellIn)
        {
            // Given the item is Backstage Passes and the Sell In value is greater than 10
            var backstagePassesItem = CreateItem("Backstage passes to a TAFKAL80ETC concert", 10, sellIn);
            var SUT = CreateGildedRose(backstagePassesItem);

            // When the quality update occurs
            SUT.UpdateQuality();

            // Then the quality of the Backstage Passes increases by 1
            Assert.Equal(13, backstagePassesItem.Quality);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        /*- "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches; 
         Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less
         but Quality drops to 0 after the concert */
        public void UpdateQuality_Should_DecreaseQualityTo0_When_ItemIsBackstagePassesAndSellInIsLessThanOrEqualTo0(int sellIn)
        {
            // Given the item is Backstage Passes and the Sell In value is greater than 10
            var backstagePassesItem = CreateItem("Backstage passes to a TAFKAL80ETC concert", 10, sellIn);
            var SUT = CreateGildedRose(backstagePassesItem);

            // When the quality update occurs
            SUT.UpdateQuality();

            // Then the quality of the Backstage Passes increases by 1
            Assert.Equal(0, backstagePassesItem.Quality);
        }

        private static Item CreateItem(string name, int quality, int sellIn) =>
            new Item { Name = name, Quality = quality, SellIn = sellIn };

        private static GildedRose CreateGildedRose(Item newItem) =>
            new GildedRose(new List<Item> { newItem });
    }
}