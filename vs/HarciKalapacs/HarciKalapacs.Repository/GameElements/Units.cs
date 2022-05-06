using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository.GameElements
{
    public enum Team
    {
        player = 1, enemy = 2, natural = 3
    }

    public enum UnitType
    {
        Tank = 1,
        InfantryMan = 2,
        Truck = 3,
        Helicopter = 4
    }

    abstract public class Units : IMapItem
    {
        public UnitType UnitType { get; set; }
        public Team Team { get; set; }

        public string idleImage1;

        #region Abilities
        public bool CanSeeTrhoughAll { get; set; }
        public bool CanFly { get; set; }
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
        public string IdleImage1
        {
            get => idleImage1;
            set
            {
                string type = this.GetType().Name;
                if (this.Team == Team.player)
                {
                    this.idleImage1 = Directory.GetCurrentDirectory() + @"\Images" + @"\Units" + @"\Player\player" + type + "Idle1.png";
                }
                else if (this.Team == Team.enemy)
                {
                    this.idleImage1 = Directory.GetCurrentDirectory() + @"\Images" + @"\Units" + @"\Enemy\enemy" + type + "Idle1.png";
                }
                else
                {
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
        }
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

        //  -- Not tested
        public string GenerateHashForSave()
        {
            return this.UnitType.ToString()+"."+this.Team.ToString()+ "."+this.XPos+"."+this.YPos+"."+this.Hp;
        }

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
            //target.Hp = target.Hp - (this.AttackValue - target.ArmorValue);
            //if(target.Hp <= 0)
            //{
            //    target.
            //}
        }

        public void SwitchVerticalPosition()
        {
            if (this.CanFly)
            {
                this.IsInTheAir = true;
            }
        }
        public override bool Equals(object obj)
        {
            if(obj is Units)
            {
                Units comp = obj as Units;
                if(comp.UnitType == this.UnitType && comp.XPos == this.XPos && comp.YPos == this.YPos && comp.Hp == this.Hp)
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
