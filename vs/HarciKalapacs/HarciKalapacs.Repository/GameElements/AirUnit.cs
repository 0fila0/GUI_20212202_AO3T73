using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    public abstract class AirUnit : Attacker//=>Merged with units
    {
        bool isInTheAir;

        public bool IsInTheAir { get => isInTheAir; set => isInTheAir = value; }

        public void SwitchVerticalPosition()
        {
            this.isInTheAir = !this.isInTheAir;
            if (this.IsInTheAir)
            {
                this.Vision = UnitsConfig.Controllable.HelicopterConfig.Vision;
            }
            else
            {
                this.Vision = UnitsConfig.Controllable.HelicopterConfig.GroundVision;
            }
        }
    }
}
