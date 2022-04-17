using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{

    public interface IMapItem
    {
        int Hp { get; set; }
        int MaxHp { get; set; }
        int YPos { get; set; }
        int XPos { get; set; }       
        string IdleImage { get; set; }
        string DyingImage { get; set; }

        string GenerateHashForSave();

        /*public int Hp { get=>hp; set => hp = value; }
        public int MaxHp { get => maxHp; set => maxHp = value; }
        public int YPos { get => yPos; set => maxHp = yPos; }
        public int XPos { get => xPos; set => xPos = value; }
        
        public string IdleImage { get => idleImage; set => idleImage = value; }
        public string DyingImage { get => dyingImage; set => dyingImage = value; }*/
        //override ToHash();
        //BulidFromHash(Hash);
    }
}
