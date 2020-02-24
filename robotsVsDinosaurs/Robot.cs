using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotsVsDinosaurs
{
    class Robot : Contestant
    {
        //Member Variables (Has a)
        public bool validSelection;
        public Weapon weapon;
        public List<Weapon> weapons;
        private Random random;

        //Constructor (Spawner)
        public Robot(string name, int health, int energy, Random random)
        {
            this.name = name;
            this.health = health;
            this.energy = energy;
            weapon = weapons[0];
            weapons = new List<Weapon> { new Weapon("Default - Bare Hands", 5), new Weapon("Sword", 8), new Weapon("Pistol", 10), new Weapon("Light Saber", 20) };
            this.random = random;
        }

        //Member Methods (Can Do)
        public void GetNewWeapon ()
        {
            bool isInteger;
            int weaponCost = 0;
            do
            {
                Console.WriteLine("Please select a weapon for " + this.name + " by typing the number next to it and hitting enter.");
                
                for (int i = 0; i < weapons.Count; i++)
                {
                    weaponCost = (weapons[i].attackPower - weapon.attackPower) * 3;
                    if (weaponCost <= health)
                    {
                        Console.WriteLine(i + " " + weapons[i].type + "    Power: " + weapons[i].attackPower + "    Cost: " + weaponCost + " health.");
                    }
                }
                string stringWeaponChoice = Console.ReadLine();
                int intWeaponChoice;
                isInteger = int.TryParse(stringWeaponChoice, out intWeaponChoice);
                weaponCost = (weapons[intWeaponChoice].attackPower - weapon.attackPower) * 3;
                validSelection = isInteger && intWeaponChoice >= 0 && intWeaponChoice < weapons.Count && weaponCost<=health;
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
                            health -= weaponCost;
                            weapon = weapons[i];
                            break;
                        }
                    }
                }
            } while (!validSelection);
        }
        public void RobotAttacksDinosaur(Dinosaur dinosaur)
        {
            //Prompt user to pick up a weapon to upgrade their attack power since they aren't being caught by surprise
            GetNewWeapon();
            PrintRobotStats(robots.IndexOf(this), this);
            UserInterface.PressKeyToContinue();
            bool attackAgain = false;
            do
            {
                //Roll two dice for attacker and two for defender
                int attackerDiceRoll = random.Next(2, diceRollMaxValue);
                int defenderDiceRoll = random.Next(2, diceRollMaxValue);
                //Create attack and defense values (weighted by attack power and current energy/power level and multiplied by dice roll)
                int attackValue = attackerDiceRoll * robot.weapon.weaponAttackPower * robot.robotPowerLevel;
                int defenseValue = defenderDiceRoll * dinosaur.dinosaurAttackPower * dinosaur.dinosaurEnergy;
                Console.WriteLine($"Robot scores {attackValue}!");
                Console.WriteLine($"Dinosaur scores {defenseValue}!");
                Console.WriteLine();

                //See whose fight value wins the battle. Tie goes to the defender. Loser loses 5 health points. Both lose 2 energy points.
                if (attackValue > defenseValue)
                {
                    dinosaur.dinosaurHealth -= healthIncrement;
                    Console.WriteLine("Robot " + robot.robotName + " wins the battle!");
                }
                else
                {
                    robot.robotHealth -= healthIncrement;
                    Console.WriteLine("Dinosaur " + dinosaur.dinosaurName + " wins the battle!");
                }
                robot.robotPowerLevel -= energyIncrement;
                dinosaur.dinosaurEnergy -= energyIncrement / 2;

                //Check if both contestants are still alive. If not, remove the one with zero health from the game, end the attack, and prompt the user to pick from the remaining robots and dinosaurs.
                robotDead = IsRobotDead(currentRobot);
                dinosaurDead = IsDinosaurDead(currentDinosaur);
                if (robotDead || dinosaurDead)
                {
                    robotDead = false;
                    dinosaurDead = false;
                    break;
                }

                if (currentDinosaur.dinosaurEnergy <= 0 || energy <= 0)
                {
                    Console.WriteLine("One of the contestants has become completely exhausted, and a break has been called for.");
                    break;
                }

                Console.WriteLine($"{dinosaur.name} has {dinosaur.health} health and {dinosaur.energy} energy remaining");
                Console.WriteLine($"{name} has {health} health and {energy} power remaining");
                //Ask user if robot wants to attack again.
                Console.WriteLine($"To have {name} attack again, press enter. To have him back down, hit any other key. A rest will cause both participants to regain {energyIncrement} energy.");
                ConsoleKeyInfo keyInput = Console.ReadKey();
                Console.WriteLine();
                switch (keyInput.Key)
                {
                    case ConsoleKey.Enter:
                        attackAgain = true;
                        break;
                    default:
                        attackAgain = false;
                        break;
                }
            } while (attackAgain && currentRobot.robotPowerLevel > 0 && currentDinosaur.dinosaurEnergy > 0);
            IncrementRobotPowerLevel(currentRobot);
            IncrementDinosaurEnergy(currentDinosaur);

            weapon = weapons[0];
            Console.WriteLine();
        }
    }
}
