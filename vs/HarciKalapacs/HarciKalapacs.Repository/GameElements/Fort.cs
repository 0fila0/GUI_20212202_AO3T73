using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    class Fort : Units
    {
        public Fort()
        {
            this.MaxHp = UnitsConfig.Base.FortConfig.MaxHp;
            this.IdleImage1 = UnitsConfig.Base.FortConfig.IdleImage1;

            if (this.Hp == 0)
            {
                this.Hp = this.MaxHp;
            }
        }
    }
}
