using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotsVsDinosaurs
{
    class Dinosaur
    {
        //Member Variables (Has a)
        public string dinosaurName;
        public int dinosaurHealth;
        public int dinosaurEnergy;
        public int dinosaurAttackPower;
        bool validSelection = false;
        public AttackType attackType;
        public List<AttackType> attackTypes = new List<AttackType> { new AttackType("Basic Confrontation", 0), new AttackType("Attack From Behind", 3), new AttackType("Attack while sleeping", 5), new AttackType("Snake/Nija approach", 15)};

        //Constructor (Spawner)
        public Dinosaur(string name, int health, int energy, int attackPower)
        {
            dinosaurName = name;
            dinosaurHealth = health;
            dinosaurEnergy = energy;
            dinosaurAttackPower = attackPower;
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
                    Console.WriteLine(i + " " + attackTypes[i].attackTypeName + "    Power: " + attackTypes[i].attackTypeAttackPower + "    Cost: " + attackTypeCost + " health.");
                }
                string stringAttackTypeChoice = Console.ReadLine();
                int intAttackTypeChoice;
                isInteger = int.TryParse(stringAttackTypeChoice, out intAttackTypeChoice);
                validSelection = isInteger && intAttackTypeChoice >= 0 && intAttackTypeChoice < attackTypes.Count;
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
                            attackTypeCost = (attackTypes[i].attackTypeAttackPower) * 3;
                            dinosaurHealth -= attackTypeCost;
                            attackType = attackTypes[i];
                            break;
                        }
                    }
                }
            } while (!validSelection);
        }
    }
}
