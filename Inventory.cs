using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zuul
{
    public class Inventory 
    {


        private int maxWeight;
        private Dictionary<string, Item> items;
        public Inventory(int maxWeight)
        {
            this.maxWeight = maxWeight;
            this.items = new Dictionary<string, Item>();
        }
        public bool Put(string itemName, Item item)
        {
            //check if items wil fit
            if (TotalWeight() + item.Weight > maxWeight)
            {
                return false;
            }
            items.Add(itemName, item);
            return true;
        }
        public Item Get(string itemName)
        {
            if (items.ContainsKey(itemName))
            {
                Item item = items[itemName];
                items.Remove(itemName);
                return item;
            }
            return null;
        }
        //show item 
        public string Show()
        {
            string str = " ";
            if (!IsEmpty())
            {
                foreach (string itemName in items.Keys)
                {
                    Item item = items[itemName];
                    str += " - [" + itemName + "] : " + item.Description + " (" + item.Weight + " kg)\n ";
                }
            }
            return str;
        }
        public int TotalWeight()
        {
            int total = 0;
            foreach (string itemName in items.Keys)
            {
                total += items[itemName].Weight;
            }
            return total;

        }
        public bool IsEmpty()
        {
            return items.Count == 0;
        }

        public int freeWeight()
        {
            return maxWeight - TotalWeight();
        }

    }
}
