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

        public int UpdateSellDate(int Date)
        {
            return -- Date;
        }
        public int UpdateQuality(int Quality, int SellIn, int DegrationSPeed)
        {
            if (SellIn >= 1) { 
                if(Quality - NormalQualityIncrement * DegrationSPeed < 0)
                {
                    return 0;
                }
                return Quality - NormalQualityIncrement * DegrationSPeed; }
            else { 
                if(Quality - NormalQualityIncrement * DegrationSPeed * 2 < 0)
                {
                    return 0;
                }
                return Quality - NormalQualityIncrement * DegrationSPeed * 2; }
        }
        public Item UpdateGoodWine(Item Wine)
        {
            ++Wine.Quality;
            if(Wine.Quality > 50) {Wine.Quality = 50;}
            Wine.SellIn = UpdateSellDate(Wine.SellIn);
            return Wine;
        }
        public Item UpdateBackstagePass(Item Pass)
        {
            
            if(Pass.SellIn <= 10 && Pass.SellIn > 5)
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
            Pass.SellIn = UpdateSellDate(Pass.SellIn);
            return Pass;
        }
        public Item UpdateSmellyItem(Item Smelly)
        {
            Smelly.Quality = UpdateQuality(Smelly.Quality, Smelly.SellIn, 2);
            Smelly.SellIn = UpdateSellDate(Smelly.SellIn);
            return Smelly;
        }
        public Item UpdateNormalItem(Item Item)
        {
            Item.Quality = UpdateQuality(Item.Quality, Item.SellIn, 1);
            Item.SellIn = UpdateSellDate(Item.SellIn);
            return Item;
        }

        public void UpdateQuality()
        {
            
            
            //for(var i = 0; i < Items.Count; i++)
            //{
            //    var item = Items[i];
            //    //cheking if == 80 not needed.
            //    if (item.Quality >= MinQuality && item.Quality <= MaxQuality)  
            //    {
            //        if(item.Name.StartsWith("Good Wine") ) { item = UpdateGoodWine(item); }
            //        if(item.Name.StartsWith("Backstage passes")) { item = UpdateBackstagePass(item); }

            //    }
            //}

            /*for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name != "Good Wine" 
                    && Items[i].Name != "Backstage passes for Re:factor"
                    && Items[i].Name != "Backstage passes for HAXX")
                {
                    if (Items[i].Quality > 0)
                    {
                        if (Items[i].Name != "B-DAWG Keychain")
                        {
                            Items[i].Quality = Items[i].Quality - 1;
                        }
                    }
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1;

                        if (Items[i].Name == "Backstage passes for Re:factor"
                        || Items[i].Name == "Backstage passes for HAXX")
                        {
                            if (Items[i].SellIn < 11)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }

                            if (Items[i].SellIn < 6)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (Items[i].Name != "B-DAWG Keychain")
                {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }

                if (Items[i].SellIn < 0)
                {
                    if (Items[i].Name != "Good Wine")
                    {
                        if (Items[i].Name != "Backstage passes for Re:factor"
                            && Items[i].Name != "Backstage passes for HAXX")
                        {
                            if (Items[i].Quality > 0)
                            {
                                if (Items[i].Name != "B-DAWG Keychain")
                                {
                                    Items[i].Quality = Items[i].Quality - 1;
                                }
                            }
                        }
                        else
                        {
                            Items[i].Quality = Items[i].Quality - Items[i].Quality;
                        }
                    }
                    else
                    {
                        if (Items[i].Quality < 50)
                        {
                            Items[i].Quality = Items[i].Quality + 1;
                        }
                    }
                }
            }*/

        }
    }
}
