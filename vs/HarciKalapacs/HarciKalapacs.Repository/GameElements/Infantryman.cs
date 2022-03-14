using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    class Infantryman : Attacker
    {
        public Infantryman()
        {
            this.MaxHp = UnitsConfig.Controllable.InfantryConfig.MaxHp;
            this.MaxMove = UnitsConfig.Controllable.InfantryConfig.MaxMove;
            this.IdleImage1 = UnitsConfig.Controllable.InfantryConfig.IdleImage1;
            this.IdleImage2 = UnitsConfig.Controllable.InfantryConfig.IdleImage2;
            this.IdleImage3 = UnitsConfig.Controllable.InfantryConfig.IdleImage3;
            this.Vision = UnitsConfig.Controllable.InfantryConfig.Vision;
            this.Damage = UnitsConfig.Controllable.InfantryConfig.Damage;
            this.DyingImage = UnitsConfig.Controllable.InfantryConfig.DyingImage;

            if (this.Hp == 0)
            {
                this.Hp = this.MaxHp;
            }
        }
    }
}
