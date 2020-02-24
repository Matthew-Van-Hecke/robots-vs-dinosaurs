using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotsVsDinosaurs
{
    class Weapon : Accessory
    {
        //Member variables (Has a)

        //Constructor (Spawner)
        public Weapon(string type, int attackPower)
        {
            this.type = type;
            this.attackPower = attackPower;
        }

        //Member Methods (Can do)
    }
}
