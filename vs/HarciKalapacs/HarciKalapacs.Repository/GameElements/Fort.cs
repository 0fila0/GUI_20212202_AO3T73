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

            if (this.Hp == 0)
            {
                this.Hp = this.MaxHp;
            }
        }
    }
}
