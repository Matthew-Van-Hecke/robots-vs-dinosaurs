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
        List<Dinosaur> dinosaurList;

        public Herd()
        {
            fred = new Dinosaur("Fred", 100, 300, 5);
            george = new Dinosaur("George", 100, 300, 5);
            ron = new Dinosaur("Ron", 90, 300, 8);
            dinosaurList = new List<Dinosaur>();
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
