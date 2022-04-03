using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    public interface IMapItem
    {
        int hp { get; set; }
        int maxHp { get; set; }
        int yPos { get; set; }
        int xPos { get; set; }
        bool isDestructable { get; set; }
        string idleImage { get; set; }
        string dyingImage { get; set; }


        public int Hp { get=>hp; set => hp = value; }
        public int MaxHp { get => maxHp; set => maxHp = value; }
        public int YPos { get => yPos; set => maxHp = yPos; }
        public int XPos { get => xPos; set => xPos = value; }
        public bool IsDestructable { get => isDestructable; set => isDestructable = value; }
        public string IdleImage { get => idleImage; set => idleImage = value; }
        public string DyingImage { get => dyingImage; set => dyingImage = value; }
        //override ToHash();
        //BulidFromHash(Hash);
    }
}
