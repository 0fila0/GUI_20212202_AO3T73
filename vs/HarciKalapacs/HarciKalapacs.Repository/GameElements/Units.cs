namespace HarciKalapacs.Repository.GameElements
{
    enum Team
    {
        player = 1, enemy = 2
    }

    abstract class Units
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
        public int XPos { get => xPos; set => xPos = value; }
        public int YPos { get => yPos; set => yPos = value; }
        public string IdleImage1 { get => idleImage1; set => idleImage1 = value; }
        public string DyingImage { get => dyingImage; set => dyingImage = value; }
        public Team Team { get => team; set => team = value; }
    }
}
