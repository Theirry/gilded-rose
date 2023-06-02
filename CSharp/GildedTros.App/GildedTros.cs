using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace GildedTros.App
{
    public class GildedTros
    {
        IList<Item> Items;
        const int MaxQuality = 50;
        const int MinQuality = 0;
        const int NormalQualityIncrement = 1;
        public GildedTros(IList<Item> Items)
        {
            this.Items = Items;
        }

        public int SetSellDate(int Date)
        {
            return --Date;
        }
        public int SetQuality(int Quality, int SellIn, int DegrationSpeed)
        {
            int multiplier = (SellIn >= 1) ? 1 : 2; // If past SellDate --> quality drops twice as fast.
            int newQuality = Quality - NormalQualityIncrement * DegrationSpeed * multiplier;
            return (newQuality < 0) ? 0 : newQuality; // Quality cannot be negative.
        }

        /*These methodes are separated because Good Wine and Backstage Passes do not neccesarily lose or gain quaility in a multiplying manner.
         The only information I have is that the quality is incremented by a fix number.*/
        public Item UpdateGoodWine(Item Wine)
        {
            if (Wine.Quality < 50)
            {
                ++Wine.Quality;
            }
            Wine.SellIn = SetSellDate(Wine.SellIn);
            return Wine;
        }
        public Item UpdateBackstagePass(Item Pass)
        {
            //No need to use SetQuality() here since the checks on SellIn are needed either way
            ////and the Quality only increments until it is set to 0.
            if (Pass.SellIn > 10)
            {
                ++Pass.Quality;
            }
            else if(Pass.SellIn <= 10 && Pass.SellIn > 5)
            {
                    Pass.Quality = Pass.Quality + 2;
            }
            else if(Pass.SellIn <= 5 && Pass.SellIn > 0)
            {
                Pass.Quality = Pass.Quality + 3;
            }
            else
            {
                Pass.Quality = 0;
            }
            if(Pass.Quality > 50)
            {
                Pass.Quality = 50;
            }
            Pass.SellIn = SetSellDate(Pass.SellIn);
            return Pass;
        }
        public Item UpdateSmellyItem(Item Smelly)
        {
            Smelly.Quality = SetQuality(Smelly.Quality, Smelly.SellIn, 2);
            Smelly.SellIn = SetSellDate(Smelly.SellIn);
            return Smelly;
        }
        public Item UpdateNormalItem(Item Item)
        {
            Item.Quality = SetQuality(Item.Quality, Item.SellIn, 1);
            Item.SellIn = SetSellDate(Item.SellIn);
            return Item;
        }

        public Item mappFunction(Item Item)
        {
            if (Item.Quality >= MinQuality && Item.Quality <= MaxQuality)
            {
                if (Item.Name.StartsWith("Good Wine")) { return UpdateGoodWine(Item); }
                if (Item.Name.StartsWith("Backstage passes")) { return UpdateBackstagePass(Item); }
                if(Item.Name.StartsWith("Duplicate Code") || Item.Name.StartsWith("Long Methods") || Item.Name.StartsWith("Ugly Variable Names")) { return UpdateSmellyItem(Item); }
                else { return UpdateNormalItem(Item); }
            }
            return Item;
        }

        public void UpdateQuality()
        {
            Item[] mappedItems = Items.Select(item => mappFunction(item)).ToArray();
        }
    }
}
