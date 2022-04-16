using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    abstract public class Terrain :IMapItem
    {
        public bool IsOverWalkable { get; set; }
        public bool IsSeeThrough { get; set; }
        public bool IsOccupiable { get; set; }
        public bool IsDestructable { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int YPos { get; set; }
        public int XPos { get; set; }
        public string IdleImage { get; set; }
        public string DyingImage { get; set; }
    }
}
