using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    abstract public class Units : IMapItem
    {

        public Team Team { get; set; }

        #region Abilities
        public bool CanSeeTrhoughAll { get; set; }
        public bool CanAttack { get; set; }
        public bool CanHeal { get; set; }
        public bool CanGetIntoBuilding { get; set; }
        #endregion
        
        #region Stats
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int YPos { get; set; }
        public int XPos { get; set; }
        public int Vision { get; set; }
        public int MaxMove { get; set; }
        public int AttackValue { get; set; }
        public int HealValue { get; set; }
        public int ArmorValue { get; set; }
        public int Movement { get; set; }
        public bool IsInTheAir { get; set; }
        #endregion

        #region Graphics
        // string idleImage1 { get; set; }
        public string IdleImage2 { get; set; }
        public string AttackImage { get; set; }
        public string IdleImage { get; set; }
        public string DyingImage { get; set; }
        #endregion

       // public Team Team { get => team; set => team = value; }
       // public int Movement { get => movement; set => movement = value; }
       // public bool CanSeeTrhoughAll { get => canSeeTrhoughAll; set => canSeeTrhoughAll = value; }
       // public bool CanAttack { get => canAttack; set => canAttack = value; }
       // public bool CanHeal { get => canHeal; set => canHeal = value; }
       // public int AttackValue { get => attackValue; set => attackValue = value; }
       // public int HealValue { get => healValue; set => healValue = value; }
       // public int ArmorValue { get => armorValue; set => armorValue = value; }
       // public bool CanGetIntoBuilding { get => canGetIntoBuilding; set => canGetIntoBuilding = value; }
       // public bool IsInTheAir { get => isInTheAir; set => isInTheAir = value; }

       // public int Hp { get => hp; set => hp = value; }
       // public int MaxHp { get => maxHp; set => maxHp = value; }
       // public int YPos { get => yPos; set => maxHp = yPos; }
       // public int XPos { get => xPos; set => xPos = value; }

       // public string IdleImage { get => idleImage; set => idleImage = value; }
       // public string DyingImage { get => dyingImage; set => dyingImage = value; }

       // public int Vision { get => vision; set => vision = value; }
       // public int MaxMove { get => maxMove; set => maxMove = value; }
       //// public string IdleImage { get => idleImage; set => idleImage2 = value; }
       // public string IdleImage2 { get => idleImage2; set => idleImage2 = value; }
       // public string AttackImage { get => attackImage; set => attackImage = value; }


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
