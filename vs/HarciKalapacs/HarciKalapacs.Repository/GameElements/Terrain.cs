using System;
using System.Collections.Generic;
using System.IO;
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
        public string idleImage1;

        public TerrainType terrainType { get; set; }
        public bool IsOverWalkable { get; set; }
        public bool IsSeeThrough { get; set; }
        public bool IsOccupiable { get; set; }
        public bool IsDestructable { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int YPos { get; set; }
        public int XPos { get; set; }
        public string IdleImage1
        {
            get => idleImage1;
            set
            {
                string type = this.GetType().Name;
                // Let's be an obstacle.
                if (this.MaxHp == UnitsConfig.Natural.Cover.FenceConfig.MaxHp)
                    {
                        this.idleImage1 = Directory.GetCurrentDirectory() + @"\Images" + @"\Units" + @"\Natural\Cover\fence" + type + "Idle1.png";
                    }
                    else if (this.MaxHp == UnitsConfig.Natural.Cover.TreeConfig.MaxHp)
                    {
                        this.idleImage1 = Directory.GetCurrentDirectory() + @"\Images" + @"\Units" + @"\Natural\Cover\tree" + type + "Idle1.png";
                    }
                    else if (this.MaxHp == UnitsConfig.Natural.Obstacle.HouseConfig.MaxHp)
                    {
                        this.idleImage1 = Directory.GetCurrentDirectory() + @"\Images" + @"\Units" + @"\Natural\Obstacle\house" + type + "Idle1.png";
                    }
                    else
                    {
                        this.idleImage1 = Directory.GetCurrentDirectory() + @"\Images" + @"\Units" + @"\Natural\Obstacle\mountain" + type + "Idle1.png";
                    }
            }
        }
        public string DyingImage { get; set; }

        public string GenerateHashForSave()
        {
            return this.terrainType.ToString();
        }

        public override bool Equals(object obj)
        {
            if(obj is Terrain)
            {
                Terrain comp = obj as Terrain;
                if(comp.terrainType == this.terrainType && comp.XPos == this.XPos && comp.YPos == this.YPos && this.Hp == comp.Hp)
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
