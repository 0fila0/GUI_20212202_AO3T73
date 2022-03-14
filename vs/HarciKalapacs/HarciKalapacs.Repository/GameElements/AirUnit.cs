using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    class AirUnit : Attacker
    {
        bool isInTheAir;

        public bool IsInTheAir { get => isInTheAir; set => isInTheAir = value; }

        void SwitchVerticalPosition()
        {
            this.isInTheAir = !this.isInTheAir;
        }
    }
}
