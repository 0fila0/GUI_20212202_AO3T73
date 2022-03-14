using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    class Truck : Healer
    {
        public Truck()
        {
            this.MaxHp = UnitsConfig.Controllable.TruckConfig.MaxHp;
            this.MaxMove = UnitsConfig.Controllable.TruckConfig.MaxMove;
            this.IdleImage1 = UnitsConfig.Controllable.TruckConfig.IdleImage1;
            this.IdleImage2 = UnitsConfig.Controllable.TruckConfig.IdleImage2;
            this.IdleImage3 = UnitsConfig.Controllable.TruckConfig.IdleImage3;
            this.Vision = UnitsConfig.Controllable.TruckConfig.Vision;
            this.Heal = UnitsConfig.Controllable.TruckConfig.Heal;
            this.DyingImage = UnitsConfig.Controllable.TruckConfig.DyingImage;

            if (this.Hp == 0)
            {
                this.Hp = this.MaxHp;
            }
        }
    }
}
