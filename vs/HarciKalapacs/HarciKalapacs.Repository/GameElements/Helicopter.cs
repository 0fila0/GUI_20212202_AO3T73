using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    class Helicopter : Units
    {
        public Helicopter()
        {
            this.MaxHp = UnitsConfig.Controllable.HelicopterConfig.MaxHp;
            this.MaxMove = UnitsConfig.Controllable.HelicopterConfig.MaxMove;
            this.AttackValue = UnitsConfig.Controllable.HelicopterConfig.Damage;
            this.DyingImage = UnitsConfig.Controllable.HelicopterConfig.DyingImage;
            this.CanFly = true;

            if (this.Hp == 0)
            {
                this.Hp = this.MaxHp;
            }
        }
    }
}
