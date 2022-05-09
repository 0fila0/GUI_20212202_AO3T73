using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    public interface  Healer 
    {
        int heal { get; set; }

        public int Heal { get => heal; set => heal = value; }

        void HealUnit(Units target);
       /* {
            target.Hp += this.heal;
        }*/
    }
}
