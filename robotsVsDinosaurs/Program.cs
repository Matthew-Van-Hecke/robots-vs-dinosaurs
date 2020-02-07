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
            Dinosaur fred = herd.fred;
            Fleet fleet = new Fleet();
            Robot cassius = fleet.cassius;
            Battlefield battlefield = new Battlefield();
            battlefield.DinosaurAttacksRobot(fred, cassius);
            Console.ReadLine();
        }
    }
}
