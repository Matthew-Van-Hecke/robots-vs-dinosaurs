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
        bool dinosaurDead;
        bool robotDead;
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
            bool dinosaurDead = false;
            bool robotDead = false;
        }

        //Member Methods (Can do)
        public void PlayGame()
        {

            

            while (dinosaurs.Count > 0 && robots.Count > 0)
            {
                PickAttacker();
                PrintDivider();

                if (currentAttacker == "dinosaur")
                {
                    PickDinosaur();
                    PickRobot();
                    DinosaurAttacksRobot(currentDinosaur, currentRobot);
                    PrintDivider();
                }
                else if (currentAttacker == "robot")
                {
                    PickRobot();
                    PickDinosaur();
                    RobotAttacksDinosaur(currentRobot, currentDinosaur);
                    PrintDivider();
                }
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

        public void PickAttacker()
        {
            bool repeatLoop = false;
            do
            {
                if (repeatLoop)
                {
                    Console.WriteLine("Invalid response. Please try again.");
                }
                repeatLoop = false;
                Console.WriteLine("Who will attack? Robot(r) or Dinosaur(d)?");
                ConsoleKeyInfo userChoice = Console.ReadKey();
                Console.WriteLine();
                switch (userChoice.Key)
                {
                    case (ConsoleKey.R):
                        currentAttacker = "robot";
                        break;
                    case (ConsoleKey.D):
                        currentAttacker = "dinosaur";
                        break;
                    default:
                        repeatLoop = true;
                        break;
                }
            } while (repeatLoop);
            Console.WriteLine(currentAttacker);
        }
        public void PickDinosaur()
        {
            string stringDinosaurChoice = "";
            int intDinosaurChoice = 0;
            bool validSelection = true;

            Console.WriteLine("Please select your dinosaur of typing the number next to it and hittting enter: ");
            
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
                    
                    Console.WriteLine();
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
            
            PrintDivider();
            Console.WriteLine("Hit any key to continue.");
            Console.ReadLine();
            PrintDivider();
        }
        public void PickRobot()
        {
            string stringRobotChoice = "";
            int intRobotChoice = 0;
            bool validSelection = true;
            Console.WriteLine("Please select your robot of typing the number next to it and hittting enter: ");
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
            currentRobot.GetNewWeapon();
            PrintRobotStats(robots.IndexOf(currentRobot), currentRobot);
            PrintDivider();
            Console.WriteLine("Hit any key to continue.");
            Console.ReadLine();
            PrintDivider();
        }
        public void DinosaurAttacksRobot(Dinosaur dinosaur, Robot robot)
        {
            bool attackAgain = false;
            do
            {
                //Roll two dice for attacker and two for defender
                int attackerDiceRoll = random.Next(2, diceRollMaxValue);
                int defenderDiceRoll = random.Next(2, diceRollMaxValue);
                //Create attack and defense values (weighted by attack power and current energy/power level and multiplied by dice roll)
                int attackValue = attackerDiceRoll * dinosaur.dinosaurAttackPower * dinosaur.dinosaurEnergy;
                int defenseValue = defenderDiceRoll * robot.weapon.weaponAttackPower * robot.robotPowerLevel;
                Console.WriteLine($"Dinosaur scores {attackValue}!");
                Console.WriteLine($"Robot scores {defenseValue}!");
                
                //See whose fight value wins the battle. Tie goes to the defender. Loser loses 5 health points. Both lose 2 energy points.
                if (attackValue > defenseValue)
                {
                    robot.robotHealth -= 5;
                    Console.WriteLine("Dinosaur " + dinosaur.dinosaurName + " wins the battle!");
                }
                else
                {
                    dinosaur.dinosaurHealth -= 5;
                    Console.WriteLine("Robot " + robot.robotName + " wins the battle!");
                }
                dinosaur.dinosaurEnergy -= 2;
                robot.robotPowerLevel -= 2;

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
                //Ask user if dinosaur wants to attack again.
                Console.WriteLine($"To have {dinosaur.dinosaurName} attack again, press enter. To have him back down, hit any other key. A rest will cause both participants to regain 2 energy.");
                ConsoleKeyInfo keyInput = Console.ReadKey();
                switch (keyInput.Key)
                {
                    case ConsoleKey.Enter:
                        attackAgain = true;
                        break;
                    default:
                        dinosaur.dinosaurEnergy += 2;
                        robot.robotPowerLevel += 2;
                        attackAgain = false;
                        break;
                }
            } while (attackAgain);
        }

        public void RobotAttacksDinosaur(Robot robot, Dinosaur dinosaur)
        {
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
                
                //See whose fight value wins the battle. Tie goes to the defender. Loser loses 5 health points. Both lose 2 energy points.
                if (attackValue > defenseValue)
                {
                    dinosaur.dinosaurHealth -= 5;
                    Console.WriteLine("Robot " + robot.robotName + " wins the battle!");
                }
                else
                {
                    robot.robotHealth -= 5;
                    Console.WriteLine("Diniosaur " + dinosaur.dinosaurName + " wins the battle!");
                }
                robot.robotPowerLevel -= 2;
                dinosaur.dinosaurEnergy -= 2;

                //Check if both contestants are still alive. If not, remove the one with zero health from the game, end the attack, and prompt the user to pick from the remaining robots and dinosaurs.
                robotDead = IsRobotDead(currentRobot);
                dinosaurDead = IsDinosaurDead(currentDinosaur);
                if (robotDead||dinosaurDead)
                {
                    robotDead = false;
                    dinosaurDead = false;
                    break;
                }

                Console.WriteLine($"{dinosaur.dinosaurName} has {dinosaur.dinosaurHealth} health and {dinosaur.dinosaurEnergy} energy remaining");
                Console.WriteLine($"{robot.robotName} has {robot.robotHealth} health and {robot.robotPowerLevel} power remaining");
                //Ask user if robot wants to attack again.
                Console.WriteLine($"To have {robot.robotName} attack again, press enter. To have him back down, hit any other key. A rest will cause both participants to regain 2 energy.");
                ConsoleKeyInfo keyInput = Console.ReadKey();
                switch (keyInput.Key)
                {
                    case ConsoleKey.Enter:
                        attackAgain = true;
                        break;
                    default:
                        robot.robotPowerLevel += 2;
                        dinosaur.dinosaurEnergy += 2;
                        attackAgain = false;
                        break;
                }
            } while (attackAgain);
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
                Console.WriteLine("Dinosaur " + dinosaur.dinosaurName + " died. Please pick another dinosaur to take up the fight.");
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
                Console.WriteLine("Robot " + robot.robotName + " died. Please pick another robot to take up the fight.");
                robots.Remove(robot);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
