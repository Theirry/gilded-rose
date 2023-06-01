using System.Collections.Generic;
using System.Xml.Serialization;
using Xunit;

namespace GildedTros.App
{
    public class GildedTrosTest
    {
        [Fact]
        public void foo()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal("foo", Items[0].Name); //Changed "fixme" to "foo". UpdateQuality() does not alter names.
        }

        [Fact]
        public void passedSellDate()
        {
            //Testing if due date is equal to 1 or 0. It is equal to 1.
            //Testing what the values are for items that are past the sell date.
            //Testing that the quality does not drop below 0.
            IList<Item> Items = new List<Item> { new Item { Name = "passedSellDate", SellIn = 1, Quality = 4 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(3, Items[0].Quality);
            app.UpdateQuality();
            Assert.Equal(1, Items[0].Quality);
            app.UpdateQuality();
            Assert.Equal(0, Items[0].Quality);
        }

        [Fact]
        public void maxQuality()
        {
            //Testing if quality does not go above 50.
            //Testing if Good Wine only goes up in quality.
            IList<Item> Items = new List<Item> { new Item { Name = "Good Wine", SellIn = 3, Quality = 48 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(49, Items[0].Quality);
            app.UpdateQuality();
            Assert.Equal(50, Items[0].Quality);
            app.UpdateQuality();
            Assert.Equal(50, Items[0].Quality);
        }

        [Fact]
        public void legendaryQuality()
        {
            //Testing if B-DAWG selldate and quality never change. And that it is always equal to 80.
            IList<Item> Items = new List<Item> { new Item { Name = "B-DAWG Keychain", SellIn = 0, Quality = 80 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(80, Items[0].Quality);
            Assert.Equal(0, Items[0].SellIn);
        }

        [Fact]
        public void backstageQuality()
        {
            //Testing if quality of Backstage passes is updated correctly.
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 11, Quality = 0 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(1, Items[0].Quality);
            app.UpdateQuality(); //Sell date is 10 or less
            Assert.Equal(3, Items[0].Quality);
            Items[0].SellIn = 5; //Sell date is 5 or less
            app.UpdateQuality();
            Assert.Equal(6, Items[0].Quality);
            Items[0].SellIn = 0; // Past sell date
            Assert.Equal(0, Items[0].Quality);
        }

        [Fact]
        public void smellyQuality()
        {
            //Testing is smelly items drop quality twice as fast as normal items. 
            IList<Item> Items = new List<Item> { new Item { Name = "Duplicate Code", SellIn = 1, Quality = 6 }, 
                new Item { Name = "Long Methods", SellIn = 2, Quality = 6 }, 
                new Item { Name = "Ugly Variable Names", SellIn = 2, Quality = 6 } };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality(); //Not past sell date
            Assert.Equal(4, Items[0].Quality);
            Assert.Equal(4, Items[1].Quality);
            Assert.Equal(4, Items[2].Quality);
            app.UpdateQuality(); //Past sell date
            Assert.Equal(0, Items[0].Quality);
            Assert.Equal(0, Items[1].Quality);
            Assert.Equal(0, Items[2].Quality);
        }

        [Fact]
        public void testAllOnce()
        {
            IList<Item> Items = new List<Item> {
            new Item { Name = "passedSellDate", SellIn = 0, Quality = 4 },
            new Item { Name = "Backstage passes for Re:factor", SellIn = 10, Quality = 0 },
            new Item { Name = "B-DAWG Keychain", SellIn = 0, Quality = 80 },
            new Item { Name = "Good Wine", SellIn = 1, Quality = 49 }
            };
            GildedTros app = new GildedTros(Items);
            app.UpdateQuality();
            Assert.Equal(2, Items[0].Quality);
            Assert.Equal(2, Items[1].Quality);
            Assert.Equal(80, Items[2].Quality);
            Assert.Equal(50, Items[3].Quality);
        }
    }
}