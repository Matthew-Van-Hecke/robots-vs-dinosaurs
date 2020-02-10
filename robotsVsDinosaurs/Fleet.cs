using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotsVsDinosaurs
{
    class Fleet
    {
        //Member Variables (Has a)
        Robot brutus;
        Robot cassius;
        Robot caesar;
        List<Robot> robotList;
        //Constructor (Spawner)
        public Fleet()
        {
            brutus = new Robot("Brutus", 100, 30);
            cassius = new Robot("Cassius", 100, 30);
            caesar = new Robot("Julius Caesar", 90, 30);
            robotList = new List<Robot>();
        }
        //Member Methods (Can Do)
        public List<Robot> GenerateRobotList()
        {
            robotList.Add(brutus);
            robotList.Add(cassius);
            robotList.Add(caesar);
            return robotList;
        }
    }
}
