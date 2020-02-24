using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotsVsDinosaurs
{
    class AttackType : Accessory
    {
        //Member Variables (Has a)
        
        //Constructor (Spawner)
        public AttackType(string type, int attackPower)
        {
            this.type = type;
            this.attackPower = attackPower;
        }
        //Member Methods (Can do)

    }
}
