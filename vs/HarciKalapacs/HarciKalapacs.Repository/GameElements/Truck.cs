using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    class Truck : Units, IHealer
    {
        public Truck()
        {
            this.UnitType = UnitType.Truck;
            this.MaxHp = UnitsConfig.Controllable.TruckConfig.MaxHp;
            this.MaxMove = UnitsConfig.Controllable.TruckConfig.MaxMove;
            this.Vision = UnitsConfig.Controllable.TruckConfig.Vision;
            this.HealValue = UnitsConfig.Controllable.TruckConfig.Heal;
            this.DyingImage = UnitsConfig.Controllable.TruckConfig.DyingImage;
            this.CanAttack = false;
            this.CanFly = false;
            this.CanHeal = true;

            if (this.Hp == 0)
            {
                this.Hp = this.MaxHp;
            }
        }

        public void HealUnit(Units target)
        {
            target.Hp += this.HealValue;
        }
    }
}
