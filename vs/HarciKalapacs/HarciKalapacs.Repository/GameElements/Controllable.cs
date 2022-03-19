using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    public abstract class Controllable : Units
    {
        int vision;
        int maxMove;
        string idleImage2;
        string idleImage3;
        string attackImage;

        public int Vision { get => vision; set => vision = value; }
        public int MaxMove { get => maxMove; set => maxMove = value; }
        public string IdleImage2 { get => idleImage2; set => idleImage2 = value; }
        public string IdleImage3 { get => idleImage3; set => idleImage3 = value; }
        public string AttackImage { get => attackImage; set => attackImage = value; }

        void Move(int x, int y)
        {
            this.YPos = x;
            this.XPos = y;
        }
    }
}
