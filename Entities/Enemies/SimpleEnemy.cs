using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vampire_Survivor.Entities.Enemies
{
    public class SimpleEnemy : Enemy

    {
        public SimpleEnemy(Player target, int initHealth, int damage) : base(target, initHealth, damage)
        {
        }
    }
}
