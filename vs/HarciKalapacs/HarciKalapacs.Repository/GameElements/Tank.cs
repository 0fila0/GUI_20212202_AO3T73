﻿using System;
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
