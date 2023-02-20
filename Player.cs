using System;
using Zuul;


namespace Zuul
{
    public class Player
    {
        private int health;

        private Inventory inventory;
        public Inventory PlayerInventory
        {
            get { return inventory; }
        }

        public Room CurrentRoom { get; set; }

        public int Health
        {
            get { return health; }
        }
        public Player()
        {
            CurrentRoom = null;
            health = 100;

            inventory = new Inventory(100); //Max weight that player can carry 
        }

        public void Damage(int amount) //Substract Healt
        {
            health -= amount;
        }

        public void Heal(int amount) //Add Health
        {
            health += amount;
        }

        public bool IsAlive() //Check if Player is alive
        {
            return (health > 0);
        }

        public bool TakeFromChest(string itemName)
        {
            Item item = CurrentRoom.Chest.Get(itemName);
            if (item == null)
            {
                Console.WriteLine(" there is no " + itemName + " in this room ");
                return false;
            }
            if(inventory.Put(itemName, item))
            {
                Console.WriteLine(" you have " + itemName + " in your inventory ");
                return true;
            }
            CurrentRoom.Chest.Put(itemName, item);
            Console.WriteLine(" The " + itemName + " is too heavy for your backpack. ");
            return true;
        }
        public bool DropToChest(string itemName)
        {
            Item item = this.inventory.Get(itemName);

            if ( item == null)
            {
                Console.WriteLine("your inventory does not contain the specified item.. ");
                return false;
            }
            CurrentRoom.Chest.Put(itemName, item);
                Console.WriteLine(" You dropped " + itemName + " from your inventory");
                return true;
        }
    }
}
     

        

