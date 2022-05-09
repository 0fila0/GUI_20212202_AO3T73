using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    public class Tank : Units
    {
        public Tank()
        {
            this.UnitType = UnitType.Tank;
            this.MaxHp = UnitsConfig.Controllable.TankConfig.MaxHp;
            this.MaxMove = UnitsConfig.Controllable.TankConfig.MaxMove;
            this.Vision = UnitsConfig.Controllable.TankConfig.Vision;
            this.AttackValue = UnitsConfig.Controllable.TankConfig.Damage;
            this.DyingImage = UnitsConfig.Controllable.TankConfig.DyingImage;
            this.CanAttack = true;
            this.CanFly = false;
            this.CanHeal = false;

            if (this.Hp == 0)
            {
                this.Hp = this.MaxHp;
            }
        }
    }
}
