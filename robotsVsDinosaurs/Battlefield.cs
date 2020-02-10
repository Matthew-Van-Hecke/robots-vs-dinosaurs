using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotsVsDinosaurs
{
    class Battlefield
    {
        //Member Variables (Has a)
        Herd herd;
        Fleet fleet;
        Random random;
        public List<Dinosaur> dinosaurs;
        public List<Robot> robots;
        Dinosaur currentDinosaur;
        Robot currentRobot;
        string currentAttacker;
        bool dinosaursTurn = false;
        bool dinosaurDead;
        bool robotDead;
        int energyIncrement = 10;
        int healthIncrement = 5;
        int energyCapacity = 30;
        int diceRollMaxValue = 13;

        //Constructor (Spawner)
        public Battlefield()
        {
            herd = new Herd();
            fleet = new Fleet();
            random = new Random();
            dinosaurs = herd.GenerateDinosaurList();
            robots = fleet.GenerateRobotList();
            currentAttacker = "";
        }

        //Member Methods (Can do)
        public void PlayGame()
        {
            bool robotRecharged = false;
            bool dinosaurNapped = false;
            

            while (dinosaurs.Count > 0 && robots.Count > 0)
            {
                dinosaurNapped = false;
                robotRecharged = false;
                PrintDivider();

                if (dinosaursTurn)
                {
                    Console.WriteLine("Dinosaur's turn.");
                    PrintDivider();
                    PickDinosaur();
                    if (currentDinosaur.dinosaurEnergy < energyCapacity)
                    {
                        dinosaurNapped = DinosaurNap(currentDinosaur);
                    }
                    if (!dinosaurNapped)
                    {
                        PickRobot();
                        DinosaurAttacksRobot(currentDinosaur, currentRobot);
                    }
                }
                else if (!dinosaursTurn)
                {
                    Console.WriteLine("Robot's Turn.");
                    PrintDivider();
                    PickRobot();
                    if (currentRobot.robotPowerLevel < 30)
                    {
                        robotRecharged = RobotRecharge(currentRobot);
                    }
                    if (!robotRecharged)
                    {
                        PickDinosaur();
                        RobotAttacksDinosaur(currentRobot, currentDinosaur);
                    }
                }
                dinosaursTurn = !dinosaursTurn;
            }
            //When one of the lists becomes empty, other team wins.
            if (dinosaurs.Count>0)
            {
                Console.WriteLine("The dinosaurs win!");
            }
            else
            {
                Console.WriteLine("The robots win!");
            }
        }

        public void PickDinosaur()
        {
            string stringDinosaurChoice = "";
            int intDinosaurChoice = 0;
            bool validSelection = true;

            Console.WriteLine("Please select your dinosaur by typing the number next to it and hittting enter: ");
            
            //Using a do while loop, take in the user input, make sure it's a valid integer and a valid index of list of remaining dinosaurs. Set the dinosaur they select as current dinosaur.

                do
                {
                    //Print remaining dinosaur options
                    for (int i = 0; i < dinosaurs.Count; i++)
                    {
                        PrintDinosaurStats(i, dinosaurs[i]);
                    }

                    stringDinosaurChoice = Console.ReadLine();
                    validSelection = int.TryParse(stringDinosaurChoice, out intDinosaurChoice) && intDinosaurChoice>=0 && intDinosaurChoice<dinosaurs.Count;
                    if (!validSelection)
                    {
                        PrintDivider();
                        Console.WriteLine("Invalid response. Please type the number next to your choice, and hit enter.");
                    }

                    PrintDivider();
                } while (!validSelection);
                for (int i = 0; i < dinosaurs.Count; i++)
                {
                    if (i == intDinosaurChoice)
                    {
                        currentDinosaur = dinosaurs[i];
                        Console.Write("You picked ");
                        PrintDinosaurStats(i, dinosaurs[i]);
                    }
                }

            PressKeyToContinue();
        }
        public void PickRobot()
        {
            string stringRobotChoice = "";
            int intRobotChoice = 0;
            bool validSelection = true;
            Console.WriteLine("Please select your robot by typing the number next to it and hittting enter: ");
            //Using a do while loop, take in the user input, make sure it's a valid integer and a valid index of list of remaining robots. Set the robot they select as current robot.
            do
            {
                for (int i = 0; i < robots.Count; i++)
                {
                    PrintRobotStats(i, robots[i]);
                }

                stringRobotChoice = Console.ReadLine();
                validSelection = int.TryParse(stringRobotChoice, out intRobotChoice) && intRobotChoice >= 0 && intRobotChoice < robots.Count;

                if (!validSelection)
                {
                    PrintDivider();
                    Console.WriteLine("Invalid response. Please type the number next to your choice, and hit enter.");
                }

                Console.WriteLine();
                PrintDivider();
            } while (!validSelection);

            for (int i = 0; i < robots.Count; i++)
            {
                if (i == intRobotChoice)
                {
                    currentRobot = robots[i];
                    Console.Write("You picked ");
                    PrintRobotStats(i, robots[i]);
                }
            }

            PressKeyToContinue();
        }
        public void DinosaurAttacksRobot(Dinosaur dinosaur, Robot robot)
        {
            //Prompt user to pick an attack type.
            currentDinosaur.SelectAttackType(currentDinosaur.dinosaurName);
            PrintDinosaurStats(dinosaurs.IndexOf(currentDinosaur), currentDinosaur);
            PressKeyToContinue();
            bool attackAgain = false;
            do
            {
                //Roll two dice for attacker and two for defender
                int attackerDiceRoll = random.Next(2, diceRollMaxValue);
                int defenderDiceRoll = random.Next(2, diceRollMaxValue);
                //Create attack and defense values (weighted by attack power and current energy/power level and multiplied by dice roll)
                int attackValue = attackerDiceRoll * (dinosaur.dinosaurAttackPower + dinosaur.attackType.attackTypeAttackPower) * dinosaur.dinosaurEnergy;
                int defenseValue = defenderDiceRoll * robot.weapon.weaponAttackPower * robot.robotPowerLevel;
                Console.WriteLine($"Dinosaur scores {attackValue}!");
                Console.WriteLine($"Robot scores {defenseValue}!");
                Console.WriteLine();
                
                //See whose fight value wins the battle. Tie goes to the defender. Loser loses 5 health points. Both lose 2 energy points.
                if (attackValue > defenseValue)
                {
                    robot.robotHealth -= healthIncrement;
                    Console.WriteLine("Dinosaur " + dinosaur.dinosaurName + " wins the battle!");
                }
                else
                {
                    dinosaur.dinosaurHealth -= healthIncrement;
                    Console.WriteLine("Robot " + robot.robotName + " wins the battle!");
                }
                dinosaur.dinosaurEnergy -= energyIncrement;
                robot.robotPowerLevel -= energyIncrement / 2;

                Console.WriteLine($"{dinosaur.dinosaurName} has {dinosaur.dinosaurHealth} health and {dinosaur.dinosaurEnergy} energy remaining");
                Console.WriteLine($"{robot.robotName} has {robot.robotHealth} health and {robot.robotPowerLevel} power remaining");
                //Check if both contestants have health remaining. If either one does not, remove them from the game, and break out of attack loop.
                robotDead = IsDinosaurDead(currentDinosaur);
                dinosaurDead = IsRobotDead(currentRobot);
                if (dinosaurDead||robotDead)
                {
                    dinosaurDead = false;
                    robotDead = false;
                    break;
                }
                if (currentDinosaur.dinosaurEnergy <= 0 || currentRobot.robotPowerLevel <= 0)
                {
                    Console.WriteLine("One of the contestants has become completely exhausted, and a break has been called for.");
                    break;
                }
                //Ask user if dinosaur wants to attack again.
                Console.WriteLine($"To have {dinosaur.dinosaurName} attack again, press enter. To have him back down, hit any other key. A rest will cause both participants to regain {energyIncrement} energy.");
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
            } while (attackAgain && currentDinosaur.dinosaurEnergy>0 && currentRobot.robotPowerLevel>0);
            dinosaur.dinosaurEnergy += energyIncrement;
            robot.robotPowerLevel += energyIncrement;
            robot.weapon = robot.weapons[0];
            Console.WriteLine();
        }

        public void RobotAttacksDinosaur(Robot robot, Dinosaur dinosaur)
        {
            //Prompt user to pick up a weapon to upgrade their attack power since they aren't being caught by surprise
            currentRobot.GetNewWeapon(currentRobot.robotName);
            PrintRobotStats(robots.IndexOf(currentRobot), currentRobot);
            PressKeyToContinue();
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
                dinosaur.dinosaurEnergy -= energyIncrement/2;

                //Check if both contestants are still alive. If not, remove the one with zero health from the game, end the attack, and prompt the user to pick from the remaining robots and dinosaurs.
                robotDead = IsRobotDead(currentRobot);
                dinosaurDead = IsDinosaurDead(currentDinosaur);
                if (robotDead||dinosaurDead)
                {
                    robotDead = false;
                    dinosaurDead = false;
                    break;
                }

                if (currentDinosaur.dinosaurEnergy<=0 || currentRobot.robotPowerLevel<=0)
                {
                    Console.WriteLine("One of the contestants has become completely exhausted, and a break has been called for.");
                    break;
                }

                Console.WriteLine($"{dinosaur.dinosaurName} has {dinosaur.dinosaurHealth} health and {dinosaur.dinosaurEnergy} energy remaining");
                Console.WriteLine($"{robot.robotName} has {robot.robotHealth} health and {robot.robotPowerLevel} power remaining");
                //Ask user if robot wants to attack again.
                Console.WriteLine($"To have {robot.robotName} attack again, press enter. To have him back down, hit any other key. A rest will cause both participants to regain {energyIncrement} energy.");
                ConsoleKeyInfo keyInput = Console.ReadKey();
                switch (keyInput.Key)
                {
                    case ConsoleKey.Enter:
                        attackAgain = true;
                        break;
                    default:
                        attackAgain = false;
                        break;
                }
            } while (attackAgain && currentRobot.robotPowerLevel>0 && currentDinosaur.dinosaurEnergy>0);
            robot.robotPowerLevel += energyIncrement;
            dinosaur.dinosaurEnergy += energyIncrement;
            robot.weapon = robot.weapons[0];
            Console.WriteLine();
        }
        public void PrintRobotStats(int index, Robot robot)
        {
            Console.WriteLine(index + " " + robot.robotName);
            Console.WriteLine("    Health: " + robot.robotHealth);
            Console.WriteLine("    Power Level: " + robot.robotPowerLevel);
            Console.WriteLine("    Weapon: " + robot.weapon.weaponType + " with an Attack Power of " + robot.weapon.weaponAttackPower);
            Console.WriteLine();
        }
        public void PrintDinosaurStats(int index, Dinosaur dinosaur)
        {
            Console.WriteLine(index + " " + dinosaur.dinosaurName);
            Console.WriteLine("    Health: " + dinosaur.dinosaurHealth);
            Console.WriteLine("    Energy: " + dinosaur.dinosaurEnergy);
            Console.WriteLine("    Attack Power: " + dinosaur.dinosaurAttackPower);
            Console.WriteLine("    Attack Type (added to attack power): " + dinosaur.attackType.attackTypeAttackPower);
            Console.WriteLine();
        }
        public void PrintDivider()
        {
            Console.WriteLine("----------------------------");
        }

        public bool IsDinosaurDead(Dinosaur dinosaur)
        {
            if (dinosaur.dinosaurHealth <= 0)
            {
                Console.WriteLine("Dinosaur " + dinosaur.dinosaurName + " died.");
                dinosaurs.Remove(dinosaur);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsRobotDead(Robot robot)
        {
            if (robot.robotHealth <= 0)
            {
                Console.WriteLine("Robot " + robot.robotName + " died.");
                robots.Remove(robot);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DinosaurNap(Dinosaur dinosaur)
        {
            Console.WriteLine("If you would like to allow " + dinosaur.dinosaurName + " to recharge through napping, press N or ENTER. To instead continue into an attack, press any other key.");
            Console.WriteLine("A nap will increase " + dinosaur.dinosaurName + "'s energy by 10 with a total energy cap of 30");
            ConsoleKeyInfo decision = Console.ReadKey();
            Console.WriteLine();
            if (decision.Key == ConsoleKey.N || decision.Key == ConsoleKey.Enter)
            {
                if (dinosaur.dinosaurEnergy < 200)
                {
                    dinosaur.dinosaurEnergy += 100;
                }
                else
                {
                    dinosaur.dinosaurEnergy = energyCapacity;
                }
                Console.WriteLine(dinosaur.dinosaurName + " has taken a nap. New stats:");
                PrintDinosaurStats(0, dinosaur);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool RobotRecharge(Robot robot)
        {
            Console.WriteLine("If you would like to allow " + robot.robotName + " to recharge through plugging in, press P or ENTER. To instead continue into an attack, press any other key.");
            Console.WriteLine("A recharge will increase " + robot.robotName + "'s energy by 10 with a full battery capacity being 30");
            ConsoleKeyInfo decision = Console.ReadKey();
            Console.WriteLine();
            if (decision.Key == ConsoleKey.P || decision.Key == ConsoleKey.Enter)
            {
                if (robot.robotPowerLevel < 200)
                {
                    robot.robotPowerLevel += 100;
                }
                else
                {
                    robot.robotPowerLevel = energyCapacity;
                }
                Console.WriteLine(robot.robotName + " recharged. New stats:");
                PrintRobotStats(0, robot);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void PressKeyToContinue()
        {
            PrintDivider();
            Console.WriteLine("Hit any key to continue.");
            Console.ReadKey();
            PrintDivider();
        }
    }
}
