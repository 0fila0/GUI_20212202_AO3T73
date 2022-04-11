using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    public class Terrain :ITerrain
    {
        bool isOverWalkable { get; set; }
        bool isSeeThrough { get; set; }
        bool isOccupiable { get; set; }
        bool isDestructable { get; set; }
        int hp { get; set; }
        int maxHp { get; set; }
        int yPos { get; set; }
        int xPos { get; set; }
        string idleImage { get; set; }
        string dyingImage { get; set; }

        public bool IsDestructable { get => isDestructable; set => isDestructable = value; }
        public bool IsOverWalkable { get => isOverWalkable; set => isOverWalkable = value; }
        public bool IsSeeThrough { get => isSeeThrough; set => isSeeThrough = value; }
        public bool IsOccupiable { get => isOccupiable; set => isOccupiable = value; }
        public int MaxHp { get => maxHp; set => maxHp = value; }
        public int YPos { get => yPos; set => maxHp = yPos; }
        public int XPos { get => xPos; set => xPos = value; }
        public int Hp { get => hp; set => hp = value; }

        public string IdleImage { get => idleImage; set => idleImage = value; }
        public string DyingImage { get => dyingImage; set => dyingImage = value; }
    }
}
