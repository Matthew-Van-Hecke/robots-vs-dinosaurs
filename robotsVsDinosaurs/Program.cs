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
            Dinosaur fred = herd.dinosaurArray[0];
            Fleet fleet = new Fleet();
            Robot brutus = fleet.robotArray[0];
            Battlefield battlefield = new Battlefield();
            battlefield.DinosaurAttacksRobot(fred, brutus);
            Console.ReadLine();
        }
    }
}
