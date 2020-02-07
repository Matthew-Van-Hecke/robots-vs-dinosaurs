using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotsVsDinosaurs
{
    class Dinosaur
    {
        //Member Variables (Has a)
        public string dinosaurName;
        public int dinosaurHealth;
        public int dinosaurEnergy;
        public int dinosaurAttackPower;

        //Constructor (Spawner)
        public Dinosaur(string name, int health, int energy, int attackPower)
        {
            dinosaurName = name;
            dinosaurHealth = health;
            dinosaurEnergy = energy;
            dinosaurAttackPower = attackPower;
        }
    }
}
