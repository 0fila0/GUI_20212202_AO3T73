using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    class Cover : Units
    {
        int absorption;

        public Cover()
        {
            if (this.MaxHp == 0)
            {
                int rnd = new Random().Next(1, 3);
                if (rnd == 1)
                {
                    this.MaxHp = 36;
                }
                else
                {
                    this.MaxHp = 90;
                }
            }
        }

        public int Absorption { get => absorption; set => absorption = value; }
    }
}
