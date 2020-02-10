using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotsVsDinosaurs
{
    class Robot
    {
        //Member Variables (Has a)
        public string robotName;
        public int robotHealth;
        public int robotPowerLevel;
        public Weapon weapon;
        public Weapon sword = new Weapon("Sword", 8);
        public Weapon pistol = new Weapon("Pistol", 10);
        public Weapon lightSaber = new Weapon("Light Saber", 20);

        //Constructor (Spawner)
        public Robot(string name, int health, int powerLevel)
        {
            robotName = name;
            robotHealth = health;
            robotPowerLevel = powerLevel;
            weapon = new Weapon("Default - Bare Hands", 5);
        }

        //Member Methods (Can Do)
        public void GetNewWeapon ()
        {
            bool repeatLoop = false;
            do
            {
                repeatLoop = false;
                Console.WriteLine("Give your robot a weapon: (Options: Sword(s), Pistol(p), Light Saber(l)");
                ConsoleKeyInfo newWeapon = Console.ReadKey();
                Console.WriteLine();
                switch (newWeapon.Key)
                {
                    case ConsoleKey.S:
                        weapon = sword;
                        break;
                    case ConsoleKey.P:
                        weapon = pistol;
                        break;
                    case ConsoleKey.L:
                        weapon = lightSaber;
                        break;
                    default:
                        repeatLoop = true;
                        break;
                }
            } while (repeatLoop == true);
        }
    }
}
