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
        Dinosaur currentDinosaur;
        Robot currentRobot;
        Random random = new Random();
        int diceRollMaxValue = 13;
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
            int attackerDiceRoll = random.Next(diceRollMaxValue);
            int defenderDiceRoll = random.Next(diceRollMaxValue);
            Console.WriteLine(attackerDiceRoll*dinosaur.dinosaurAttackPower);
            Console.WriteLine(defenderDiceRoll*robot.robotPowerLevel);
            if (attackerDiceRoll * dinosaur.dinosaurHealth > defenderDiceRoll * robot.robotHealth)
            {
                robot.robotHealth -= 5;
                Console.WriteLine("Dinosaur " + dinosaur.dinosaurName + " wins the battle!");
            }
            else
            {
                dinosaur.dinosaurHealth -= 5;
                Console.WriteLine("Robot" + robot.robotName);
            }
        }
    }
}
