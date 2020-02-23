using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotsVsDinosaurs
{
    class Dinosaur : Contestant
    {
        //Member Variables (Has a)
        public int attackPower;
        bool validSelection = false;
        public AttackType attackType;
        public List<AttackType> attackTypes;

        //Constructor (Spawner)
        public Dinosaur(string name, int health, int energy, int attackPower)
        {
            this.name = name;
            this.health = health;
            this.energy = energy;
            this.attackPower = attackPower;
            attackTypes = new List<AttackType> { new AttackType("Basic Confrontation", 0), new AttackType("Attack From Behind", 3), new AttackType("Attack while sleeping", 5), new AttackType("Snake/Nija approach", 15) };
            attackType = attackTypes[0];
        }
        //Member Methods (Can do)
        public void SelectAttackType(string name)
        {
            bool isInteger;
            int attackTypeCost = 0;
            do
            {
                Console.WriteLine("Please select an attack type for " + name + " by typing the number next to it and hitting enter.");

                for (int i = 0; i < attackTypes.Count; i++)
                {
                    attackTypeCost = (attackTypes[i].attackTypeAttackPower) * 3;
                    if (attackTypeCost <= health)
                    {
                        Console.WriteLine(i + " " + attackTypes[i].attackTypeName + "    Power: " + attackTypes[i].attackTypeAttackPower + "    Cost: " + attackTypeCost + " health.");
                    }
                }
                string stringAttackTypeChoice = Console.ReadLine();
                int intAttackTypeChoice;
                isInteger = int.TryParse(stringAttackTypeChoice, out intAttackTypeChoice);
                attackTypeCost = attackTypes[intAttackTypeChoice].attackTypeAttackPower * 3;
                validSelection = isInteger && intAttackTypeChoice >= 0 && intAttackTypeChoice < attackTypes.Count && attackTypeCost <= health;
                if (!validSelection)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid selection. Please try again.");
                    continue;
                }
                else
                {
                    for (int i = 0; i < attackTypes.Count; i++)
                    {
                        if (i == intAttackTypeChoice)
                        {
                            health -= attackTypeCost;
                            attackType = attackTypes[i];
                            break;
                        }
                    }
                }
            } while (!validSelection);
        }
    }
}
