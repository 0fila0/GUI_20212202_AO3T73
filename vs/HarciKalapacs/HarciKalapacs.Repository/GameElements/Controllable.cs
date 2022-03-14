using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    abstract class Controllable : Units
    {
        int vision;
        int maxMove;
        string idleImage2;
        string idleImage3;
        string attackImage;

        void Move(int x, int y)
        {
            this.XPos = x;
            this.YPos = y;
        }
    }
}
