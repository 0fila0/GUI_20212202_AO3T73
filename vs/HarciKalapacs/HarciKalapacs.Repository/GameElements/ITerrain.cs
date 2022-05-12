using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    interface ITerrain : IMapItem
    {
        bool IsOverWalkable{ get; set; }
        bool IsSeeThrough { get; set; }
        bool IsOccupiable { get; set; }
        bool IsDestructable { get; set; }

        /*
        public bool IsDestructable { get => isDestructable; set => isDestructable = value; }
        public bool IsOverWalkable { get => isOverWalkable; set => isOverWalkable = value; }
        public bool IsSeeThrough { get => isSeeThrough; set => isSeeThrough = value; }
        public bool IsOccupiable { get => isOccupiable; set => isOccupiable = value; }*/

    }
}
