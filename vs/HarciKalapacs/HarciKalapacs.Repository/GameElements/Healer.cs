using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    public abstract class Healer : Controllable
    {
        int heal;

        public int Heal { get => heal; set => heal = value; }

        void HealUnit(Units target)
        {
            target.Hp += this.heal;
        }
    }
}
