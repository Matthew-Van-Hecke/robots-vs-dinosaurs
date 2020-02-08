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
        Herd myHerd = new Herd();
        Fleet fleet = new Fleet();
        Random random = new Random();
        
        Robot[] robotArray;
        int diceRollMaxValue = 13;
        public List<Dinosaur> dinosaurList = new List<Dinosaur>();
        
        ///FIX THIS
        //Constructor (Spawner)
        //Member Methods (Can do)
        public void PlayGame ()
        {
            Console.WriteLine("Pick your starting dinosaur: Fred(f), George(g), or Ron(r)");
            ConsoleKeyInfo dinosaurChoice = Console.ReadKey();
            //switch ()
        }
        public void DinosaurAttacksRobot(Dinosaur dinosaur, Robot robot)
        {
            bool attackAgain = false;
            do
            {
                //Roll two dice for attacker and two for defender
                int attackerDiceRoll = random.Next(1, diceRollMaxValue);
                int defenderDiceRoll = random.Next(1, diceRollMaxValue);
                //Create attack and defense values (weighted by attack power and current energy/power level and multiplied by dice roll)
                int attackValue = attackerDiceRoll * dinosaur.dinosaurAttackPower * dinosaur.dinosaurEnergy;
                int defenseValue = defenderDiceRoll * robot.weapon.weaponAttackPower * robot.robotPowerLevel;
                Console.WriteLine(attackValue);
                Console.WriteLine(defenseValue);
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
        public void BuildDinosaurList()
        {
            dinosaurList = myHerd.GenerateDinosaurList();
        }
    }
}
