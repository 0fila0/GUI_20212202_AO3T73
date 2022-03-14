using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    class Helicopter : AirUnit
    {
        public Helicopter()
        {
            this.MaxHp = UnitsConfig.Controllable.HelicopterConfig.MaxHp;
            this.MaxMove = UnitsConfig.Controllable.HelicopterConfig.MaxMove;
            this.IdleImage1 = UnitsConfig.Controllable.HelicopterConfig.IdleImage1;
            this.IdleImage2 = UnitsConfig.Controllable.HelicopterConfig.IdleImage2;
            this.IdleImage3 = UnitsConfig.Controllable.HelicopterConfig.IdleImage3;
            this.Vision = UnitsConfig.Controllable.HelicopterConfig.Vision;
            this.Damage = UnitsConfig.Controllable.HelicopterConfig.Damage;
            this.DyingImage = UnitsConfig.Controllable.HelicopterConfig.DyingImage;

            if (this.Hp == 0)
            {
                this.Hp = this.MaxHp;
            }
        }
    }
}
