using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    interface Terrain : IMapItem
    {
        bool IsOverWalkable{ get; set; }
        bool IsSeeThrough { get; set; }
        bool IsOccupiable { get; set; }
    }
}
