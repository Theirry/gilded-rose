using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace GildedTros.App
{
    public class GildedTros
    {
        IList<Item> Items;
        private const int _maxQuality = 50;
        private const int _minQuality = 0;
        private const int _normalQualityIncrement = 1;
        public GildedTros(IList<Item> Items)
        {
            this.Items = Items;
        }

        public int DecreaseSellIn(int date)
        {
            return --date;
        }
        public int CalculateQuality(int quality, int sellIn, int degrationSpeed)
        {
            int multiplier = (sellIn >= 1) ? 1 : 2; // If past SellDate --> quality drops twice as fast.
            int newQuality = quality - _normalQualityIncrement * degrationSpeed * multiplier;
            return (newQuality < 0) ? 0 : newQuality; // Quality cannot be negative.
        }

        /*These methodes are separated because Good Wine and Backstage Passes do not neccesarily lose or gain quaility in a multiplying manner.
         The only information I have is that the quality is incremented by a fix number.*/
        public Item UpdateGoodWine(Item wine)
        {
            if (wine.Quality < 50)
            {
                ++wine.Quality;
            }
            wine.SellIn = DecreaseSellIn(wine.SellIn);
            return wine;
        }
        public Item UpdateBackstagePass(Item pass)
        {
            //No need to use SetQuality() here since the checks on SellIn are needed either way
            ////and the Quality only increments until it is set to 0.
            if (pass.SellIn > 10)
            {
                ++pass.Quality;
            }
            else if(pass.SellIn <= 10 && pass.SellIn > 5)
            {
                    pass.Quality = pass.Quality + 2;
            }
            else if(pass.SellIn <= 5 && pass.SellIn > 0)
            {
                pass.Quality = pass.Quality + 3;
            }
            else
            {
                pass.Quality = 0;
            }
            if(pass.Quality > 50)
            {
                pass.Quality = 50;
            }
            pass.SellIn = DecreaseSellIn(pass.SellIn);
            return pass;
        }
        public Item UpdateSmellyItem(Item smelly)
        {
            smelly.Quality = CalculateQuality(smelly.Quality, smelly.SellIn, 2);
            smelly.SellIn = DecreaseSellIn(smelly.SellIn);
            return smelly;
        }
        public Item UpdateNormalItem(Item item)
        {
            item.Quality = CalculateQuality(item.Quality, item.SellIn, 1);
            item.SellIn = DecreaseSellIn(item.SellIn);
            return item;
        }

        public Item mappFunction(Item item)
        {
            if (item.Quality >= _minQuality && item.Quality <= _maxQuality)
            {
                if (item.Name.StartsWith("Good Wine")) { return UpdateGoodWine(item); }
                if (item.Name.StartsWith("Backstage passes")) { return UpdateBackstagePass(item); }
                if(item.Name.StartsWith("Duplicate Code") || item.Name.StartsWith("Long Methods") || item.Name.StartsWith("Ugly Variable Names")) { return UpdateSmellyItem(item); }
                else { return UpdateNormalItem(item); }
            }
            return item;
        }

        public void UpdateQuality()
        {
            Item[] mappedItems = Items.Select(item => mappFunction(item)).ToArray();
        }
    }
}
