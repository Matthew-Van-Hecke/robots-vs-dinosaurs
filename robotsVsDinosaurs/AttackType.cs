using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace robotsVsDinosaurs
{
    class AttackType
    {
        //Member Variables (Has a)
        public string attackTypeName;
        public int attackTypeAttackPower;
        //Constructor (Spawner)
        public AttackType(string type, int attackPower)
        {
            attackTypeName = type;
            attackTypeAttackPower = attackPower;
        }
        //Member Methods (Can do)

    }
}
