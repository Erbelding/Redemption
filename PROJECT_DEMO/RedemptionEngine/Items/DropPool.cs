using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Reflection;
using Microsoft.Xna.Framework.Content;

namespace RedemptionEngine.Items
{
    public class DropPool
    {
        private List<Item> pool;
        private Dictionary<Item, int> weight;
        private int totalWeight;
        private ContentManager content;


        public DropPool(ContentManager content)
        {
            pool = new List<Item>();
            weight = new Dictionary<Item, int>();
            this.content = content;
        }

        public void AddItem(Item item, int weight)
        {
            totalWeight += weight;
            this.weight.Add(item, totalWeight);
            pool.Add(item);
        }

        public Item RandomItem(float chance)
        {
            MathHelper.Clamp(chance, 0, 1);

            if (DropItems.Rand.NextDouble() > chance) return null;

            int num = DropItems.Rand.Next(totalWeight);
            Item drop = null;
            int curWeight = int.MaxValue;
            
            foreach (Item item in pool)
            {
                if (weight[item] > num && weight[item] < curWeight)
                {
                    curWeight = weight[item];
                    drop = item;
                }
            }
            if (drop != null)
            {
                Type t = drop.GetType();
                ConstructorInfo cInfo = t.GetConstructor(new[] { typeof(ContentManager) });
                object obj = cInfo.Invoke(new object[] { content });
                drop = (obj as Item);
            }

            return drop;
        }
    }
}
