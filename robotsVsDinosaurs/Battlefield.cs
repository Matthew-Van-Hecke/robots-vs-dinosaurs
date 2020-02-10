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
        List<Dinosaur> dinosaurs;      
        public List<Robot> robots;
        Dinosaur currentDinosaur;
        Robot currentRobot;
        string currentAttacker;
        int diceRollMaxValue = 13;
        //public List<Dinosaur> dinosaurList = new List<Dinosaur>();

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
        public void PlayGame ()
        {
            bool attackerPicked = true;
            do
            {
                //Console.WriteLine("Who wants to attack? Dinosaur(d) or Robot(r)");
                //ConsoleKeyInfo attacker = Console.ReadKey();
                //switch (attacker.Key)
                //{

                //    case ConsoleKey.D:
                PickAttacker();
                PickDinosaur();
                PickRobot();
                if (currentAttacker == "dinosaur")
                {
                    DinosaurAttacksRobot(currentDinosaur, currentRobot);
                }
                else if (currentAttacker == "robot")
                {
                    RobotAttacksDinosaur(currentRobot, currentDinosaur);
                }
                //}


            } while (!attackerPicked);
        }
        public void PickAttacker()
        {
            bool repeatLoop = false;
            do
            {
                Console.WriteLine("Who will attack first? Robot(r) or Dinosaur(d)?");
                ConsoleKeyInfo userChoice = Console.ReadKey();
                switch (userChoice.Key)
                {
                    case (ConsoleKey.R):
                        currentAttacker = "robot";
                        break;
                    case (ConsoleKey.D):
                        currentAttacker = "dinosaur";
                        break;
                    default:
                        break;
                }
            } while (repeatLoop);
            Console.WriteLine(currentAttacker);
        }
        public void PickDinosaur ()
        {
            Console.WriteLine("Pick your dinosaur: ");
            for (int i = 0; i < dinosaurs.Count; i++)
            {
                Console.WriteLine(i + " " + dinosaurs[i].dinosaurName);
            }
            ConsoleKeyInfo dinosaurChoice = Console.ReadKey();
            Console.WriteLine();
            for (int i = 0; i < dinosaurs.Count; i++)
            {
                if (i == int.Parse(dinosaurChoice.KeyChar.ToString()))
                {
                    currentDinosaur = dinosaurs[i];
                    Console.WriteLine(dinosaurs[i].dinosaurName);
                }
            }
        }
        public void PickRobot()
        {
            Console.WriteLine("Pick your robot: ");
            for (int i = 0; i < robots.Count; i++)
            {
                Console.WriteLine(i + " " + robots[i].robotName);
            }
            ConsoleKeyInfo robotChoice = Console.ReadKey();
            Console.WriteLine();
            for (int i = 0; i < robots.Count; i++)
            {
                if (i == int.Parse(robotChoice.KeyChar.ToString()))
                {
                    currentRobot = robots[i];
                    Console.WriteLine(robots[i].robotName);
                }
            }
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
                Console.WriteLine($"{dinosaur.dinosaurName} has {dinosaur.dinosaurHealth} health and {dinosaur.dinosaurEnergy} energy remaining");
                Console.WriteLine($"{robot.robotName} has {robot.robotHealth} health and {robot.robotPowerLevel} power remaining");
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
        //public void BuildDinosaurList()
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
                Console.WriteLine($"{dinosaur.dinosaurName} has {dinosaur.dinosaurHealth} health and {dinosaur.dinosaurEnergy} energy remaining");
                Console.WriteLine($"{robot.robotName} has {robot.robotHealth} health and {robot.robotPowerLevel} power remaining");
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
    }
}
