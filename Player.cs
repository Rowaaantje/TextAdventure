using System;
using Zuul;


namespace Zuul
{
    public class Player
    {
        private int health;

        public Room CurrentRoom { get; set; }
        public int Health
        {
            get { return health; }
        }
        public Player()
        {
            CurrentRoom = null;

            health = 100;
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
    }
}

