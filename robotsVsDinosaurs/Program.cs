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
            Fleet fleet = new Fleet();
            Battlefield battlefield = new Battlefield();
            battlefield.PlayGame();
            Console.ReadLine();
        }
    }
}
