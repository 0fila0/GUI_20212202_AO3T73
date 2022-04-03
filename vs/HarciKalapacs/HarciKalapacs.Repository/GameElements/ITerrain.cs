using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    interface ITerrain : IMapItem
    {
        bool isOverWalkable{ get; set; }
        bool isSeeThrough { get; set; }
        bool isOccupiable { get; set; }

        public bool IsOverWalkable { get => isOverWalkable; set => isOverWalkable = value; }
        public bool IsSeeThrough { get => isSeeThrough; set => isSeeThrough = value; }
        public bool IisSeeThrough { get => isSeeThrough; set => isSeeThrough = value; }

    }
}
