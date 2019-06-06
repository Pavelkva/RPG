using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    class EnemyNpc: Fighter
    {
        public EnemyNpc(string name, int maxHp=0, int maxEnergy=0, int strength=10, int agility=10, int intellect=10, int armor=0): base(name,maxHp,maxEnergy,strength,agility,intellect,armor)
        {
        }

        public void PlayTurn(Player enemy)
        {
            Attack(enemy);
        }
    }
}
