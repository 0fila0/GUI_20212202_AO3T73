using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    class Tank : Attacker
    {
        public Tank()
        {
            this.MaxHp = UnitsConfig.Controllable.TankConfig.MaxHp;
            this.MaxMove = UnitsConfig.Controllable.TankConfig.MaxMove;
            this.IdleImage1 = UnitsConfig.Controllable.TankConfig.IdleImage1;
            this.IdleImage2 = UnitsConfig.Controllable.TankConfig.IdleImage2;
            this.IdleImage3 = UnitsConfig.Controllable.TankConfig.IdleImage3;
            this.Vision = UnitsConfig.Controllable.TankConfig.Vision;
            this.Damage = UnitsConfig.Controllable.TankConfig.Damage;
            this.DyingImage = UnitsConfig.Controllable.TankConfig.DyingImage;

            if (this.Hp == 0)
            {
                this.Hp = this.MaxHp;
            }
        }
    }
}
