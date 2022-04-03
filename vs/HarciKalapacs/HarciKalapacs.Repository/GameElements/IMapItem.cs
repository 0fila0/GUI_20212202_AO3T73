using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    public interface IMapItem
    {
        public int health { get; set; }
        public int MaxHealth { get; set; }
        public int YPos { get; set ; }
        public int XPos { get; set; }
        public bool IsDestructable { get; set; }

        //override ToHash();
        //BulidFromHash(Hash);
    }
}
