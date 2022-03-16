using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    class Obstacle : Units
    {
        public Obstacle()
        {
            if (this.MaxHp == 0)
            {
                int rnd = new Random().Next(1, 3);
                if (rnd == 1)
                {
                    this.MaxHp = 100;
                }
                else
                {
                    this.MaxHp = 200;
                }
            }
        }
    }
}
