using System;
using System.IO;

namespace HarciKalapacs.Repository.GameElements
{
    public enum Team
    {
        player = 1, enemy = 2, natural = 3
    }

    public abstract class Units
    {
        int maxHp;
        int hp;
        int xPos;
        int yPos;
        Team team;
        string idleImage1;
        string dyingImage;

        public int MaxHp { get => maxHp; set => maxHp = value; }
        public int Hp { get => hp; set => hp = value; }
        public int YPos { get => xPos; set => xPos = value; }
        public int XPos { get => yPos; set => yPos = value; }
        public string DyingImage { get => dyingImage; set => dyingImage = value; }
        public Team Team { get => team; set => team = value; }
        public string IdleImage1
        {
            get => idleImage1;
            set
            {
                string type = this.GetType().Name;
                if (this.team == Team.player)
                {
                    this.idleImage1 = Directory.GetCurrentDirectory() + @"\Images" + @"\Units" + @"\Player\player" + type + "Idle1.png";
                }
                else if (this.team == Team.enemy)
                {
                    this.idleImage1 = Directory.GetCurrentDirectory() + @"\Images" + @"\Units" + @"\Enemy\enemy" + type + "Idle1.png";
                }
                else
                {   
                    // Let's be an obstacle.
                    if (this.maxHp == UnitsConfig.Natural.Cover.FenceConfig.MaxHp)
                    {
                        this.idleImage1 = Directory.GetCurrentDirectory() + @"\Images" + @"\Units" + @"\Natural\Cover\fence" + type + "Idle1.png";
                    }
                    else if (this.maxHp == UnitsConfig.Natural.Cover.TreeConfig.MaxHp)
                    {
                        this.idleImage1 = Directory.GetCurrentDirectory() + @"\Images" + @"\Units" + @"\Natural\Cover\tree" + type + "Idle1.png";
                    }
                    else if (this.maxHp == UnitsConfig.Natural.Obstacle.HouseConfig.MaxHp)
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
    }
}