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
        public bool validSelection;
        public Weapon weapon;
        public List<Weapon> weapons = new List<Weapon> { new Weapon("Default - Bare Hands", 5), new Weapon("Sword", 8), new Weapon("Pistol", 10), new Weapon("Light Saber", 20) };

        //Constructor (Spawner)
        public Robot(string name, int health, int powerLevel)
        {
            robotName = name;
            robotHealth = health;
            robotPowerLevel = powerLevel;
            weapon = weapons[0];
        }

        //Member Methods (Can Do)
        public void GetNewWeapon (string name)
        {
            bool isInteger;
            int weaponCost = 0;
            do
            {
                Console.WriteLine("Please select a weapon for " + name + " by typing the number next to it and hitting enter.");
                
                for (int i = 0; i < weapons.Count; i++)
                {
                    weaponCost = (weapons[i].weaponAttackPower - weapon.weaponAttackPower) * 3;
                    if (weaponCost <= robotHealth)
                    {
                        Console.WriteLine(i + " " + weapons[i].weaponType + "    Power: " + weapons[i].weaponAttackPower + "    Cost: " + weaponCost + " health.");
                    }
                }
                string stringWeaponChoice = Console.ReadLine();
                int intWeaponChoice;
                isInteger = int.TryParse(stringWeaponChoice, out intWeaponChoice);
                validSelection = isInteger && intWeaponChoice >= 0 && intWeaponChoice < weapons.Count;
                if (!validSelection)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid selection. Please try again.");
                    continue;
                }
                else
                {
                    for (int i = 0; i<weapons.Count; i++ )
                    {
                        if (i == intWeaponChoice)
                        {
                            weaponCost = (weapons[i].weaponAttackPower - weapon.weaponAttackPower) * 3;
                            robotHealth -= weaponCost;
                            weapon = weapons[i];
                            break;
                        }
                    }
                }
            } while (!validSelection);
        }
    }
}
