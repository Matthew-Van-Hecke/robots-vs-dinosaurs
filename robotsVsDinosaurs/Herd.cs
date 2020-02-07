using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotsVsDinosaurs
{
    class Herd
    {
        public Dinosaur fred = new Dinosaur("Fred", 100, 25, 5);
        public Dinosaur george = new Dinosaur("George", 100, 25, 5);
        public Dinosaur ron =  new Dinosaur("Ron", 100, 20, 25);
        List<Dinosaur> dinosaurList = new List<Dinosaur>();

        public List<Dinosaur> GenerateDinosaurList()
        {
            dinosaurList.Add(fred);
            dinosaurList.Add(george);
            dinosaurList.Add(ron);
            return dinosaurList;
        }
    }
}
