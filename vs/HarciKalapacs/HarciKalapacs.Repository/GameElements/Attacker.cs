using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    public abstract class Attacker : Controllable
    {
        int damage;

        public int Damage { get => damage; set => damage = value; }

        void Attack(Units target)   
        {
            target.Hp -= this.damage;
        }
    }
}
