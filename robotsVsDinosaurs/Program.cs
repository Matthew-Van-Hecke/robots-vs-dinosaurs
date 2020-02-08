using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotsVsDinosaurs
{
    class Program
    {
        static void Main(string[] args)
        {
            Herd herd = new Herd();
            Battlefield battlefield = new Battlefield();
            battlefield.BuildDinosaurList();
            Dinosaur fred = battlefield.dinosaurList[0];
            Fleet fleet = new Fleet();
            Robot brutus = fleet.robotArray[0];
            foreach (Dinosaur dino in battlefield.dinosaurList)
            {
                Console.WriteLine(dino.dinosaurName);
            }

            //battlefield.DinosaurAttacksRobot(fred, brutus);
            Console.ReadLine();
        }
    }
}
