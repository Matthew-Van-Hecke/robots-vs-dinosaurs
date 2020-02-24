using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotsVsDinosaurs
{
    static class UserInterface
    {
        static public void PressKeyToContinue()
        {
            PrintDivider();
            Console.WriteLine("Hit any key to continue.");
            Console.ReadKey();
            Console.WriteLine();
            PrintDivider();
        }
        static public void PrintDivider()
        {
            Console.WriteLine("----------------------------");
        }
    }
}
