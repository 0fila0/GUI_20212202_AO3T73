using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{

    public enum TerrainType
    {
        Cover = 1,
        Fort = 2,
        Obstacle = 3
    }
    abstract public class Terrain :IMapItem
    {
        public TerrainType terrainType { get; set; }
        public bool IsOverWalkable { get; set; }
        public bool IsSeeThrough { get; set; }
        public bool IsOccupiable { get; set; }
        public bool IsDestructable { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int YPos { get; set; }
        public int XPos { get; set; }
        public string IdleImage1 { get; set; }
        public string DyingImage { get; set; }

        public string GenerateHashForSave()
        {
            return this.terrainType.ToString();
        }
    }
}
