using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotsVsDinosaurs
{
    class Herd
    {
        public Dinosaur fred;
        public Dinosaur george;
        public Dinosaur ron;
        List<Dinosaur> dinosaurList = new List<Dinosaur>();

        public Herd()
        {
            fred = new Dinosaur("Fred", 100, 25, 5);
            george = new Dinosaur("George", 100, 25, 5);
            ron = new Dinosaur("Ron", 100, 20, 25);
        }

        public List<Dinosaur> GenerateDinosaurList()
        {
            dinosaurList.Add(fred);
            dinosaurList.Add(george);
            dinosaurList.Add(ron);
            return dinosaurList;
        }
    }
}
