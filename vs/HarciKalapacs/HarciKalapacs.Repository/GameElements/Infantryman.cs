using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    public class Infantryman : Units
    {
        public Infantryman()
        {
            this.MaxHp = UnitsConfig.Controllable.InfantryConfig.MaxHp;
            this.MaxMove = UnitsConfig.Controllable.InfantryConfig.MaxMove;
            this.Vision = UnitsConfig.Controllable.InfantryConfig.Vision;
            this.AttackValue = UnitsConfig.Controllable.InfantryConfig.Damage;
            this.DyingImage = UnitsConfig.Controllable.InfantryConfig.DyingImage;

            if (this.Hp == 0)
            {
                this.Hp = this.MaxHp;
            }
        }
    }
}
