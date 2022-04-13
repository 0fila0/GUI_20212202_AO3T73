using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    public class Units : IUnits
    {
        Team team { get; set; }



        //abilitys
        bool canSeeTrhoughAll { get; set; }
        bool canAttack { get; set; }
        bool canHeal { get; set; }
        bool canGetIntoBuilding { get; set; }
       




        //stats
        int hp { get; set; }
        int maxHp { get; set; }
        int yPos { get; set; }
        int xPos { get; set; }
        int vision { get; set; }
        int maxMove { get; set; }
        int attackValue { get; set; }
        int healValue { get; set; }
        int armorValue { get; set; }
        int movement { get; set; }
        bool isInTheAir { get; set; }


        //graphics
        // string idleImage1 { get; set; }
        string idleImage2 { get; set; }
        string attackImage { get; set; }
        string idleImage { get; set; }
        string dyingImage { get; set; }



        public Team Team { get => team; set => team = value; }
        public int Movement { get => movement; set => movement = value; }
        public bool CanSeeTrhoughAll { get => canSeeTrhoughAll; set => canSeeTrhoughAll = value; }
        public bool CanAttack { get => canAttack; set => canAttack = value; }
        public bool CanHeal { get => canHeal; set => canHeal = value; }
        public int AttackValue { get => attackValue; set => attackValue = value; }
        public int HealValue { get => healValue; set => healValue = value; }
        public int ArmorValue { get => armorValue; set => armorValue = value; }
        public bool CanGetIntoBuilding { get => canGetIntoBuilding; set => canGetIntoBuilding = value; }
        public bool IsInTheAir { get => isInTheAir; set => isInTheAir = value; }

        public int Hp { get => hp; set => hp = value; }
        public int MaxHp { get => maxHp; set => maxHp = value; }
        public int YPos { get => yPos; set => maxHp = yPos; }
        public int XPos { get => xPos; set => xPos = value; }

        public string IdleImage { get => idleImage; set => idleImage = value; }
        public string DyingImage { get => dyingImage; set => dyingImage = value; }

        public int Vision { get => vision; set => vision = value; }
        public int MaxMove { get => maxMove; set => maxMove = value; }
       // public string IdleImage { get => idleImage; set => idleImage2 = value; }
        public string IdleImage2 { get => idleImage2; set => idleImage2 = value; }
        public string AttackImage { get => attackImage; set => attackImage = value; }


        public void GetIntoBuilding()
        {
            throw new NotImplementedException();
        }

        public void GetOutOfBuilding()
        {
            throw new NotImplementedException();
        }

        public void Move(int x, int y)
        {
            this.YPos = x;
            this.XPos = y;
        }

        public void Attack(Units target)
        {
            throw new NotImplementedException();
        }

        public void SwitchVerticalPosition()
        {
            throw new NotImplementedException();
        }
    }
}
