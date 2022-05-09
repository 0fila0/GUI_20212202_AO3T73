using HarciKalapacs.Repository;
using HarciKalapacs.Repository.GameElements;
using System.Collections.Generic;
using System.Linq;

namespace HarciKalapacs.Model
{
    public class Model : IModel
    {
        readonly IRepository repository;
        int mapWidth;
        int mapHeight;
        int mapNumber;
        ICollection<IMapItem> allUnits;
        int playerTurn;
        int maxSteps;
        int leftSteps;
        int playerGold;
        int enemyGold;
        int round;

        public Model(IRepository repository)
        {
            this.repository = repository;
            this.AllUnits = new List<IMapItem>();
        }

        public ICollection<IMapItem> AllUnits { get => allUnits; set => allUnits = value; }
        public int MapWidth { get => mapWidth; set => mapWidth = value; }
        public int MapHeight { get => mapHeight; set => mapHeight = value; }
        public int MapNumber { get => mapNumber; set => mapNumber = value; }
        public int PlayerTurn { get => playerTurn; set => playerTurn = value; }
        public int PlayerGold { get => playerGold; set => playerGold = value; }
        public int Round { get => round; set => round = value; }
        public int LeftSteps { get => leftSteps; set => leftSteps = value; }
        public int MaxSteps { get => maxSteps; set => maxSteps = value; }

        public bool LoadMap(int level)
        {
            (this.AllUnits as List<IMapItem>).Clear();
            bool success = this.repository.LoadMap(level);
            this.mapNumber = level;
            this.AllUnits = this.repository.AllUnits;
            this.ModifyAirUnitsVision();
            this.mapWidth = (this.repository.MapSize as List<int>)[0];
            this.mapHeight = (this.repository.MapSize as List<int>)[1];
            this.round = (this.repository.OtherDetails as List<int>)[0];
            this.playerTurn = (this.repository.OtherDetails as List<int>)[1];
            this.maxSteps = (this.repository.OtherDetails as List<int>)[2];
            this.leftSteps = (this.repository.OtherDetails as List<int>)[3];
            this.playerGold = (this.repository.OtherDetails as List<int>)[4];
            this.enemyGold = (this.repository.OtherDetails as List<int>)[5];
            return success;
        }

        /// <summary>
        /// It is called only once, only when start a new game or load the saved game.
        /// </summary>
        private void ModifyAirUnitsVision()
        {
            
            foreach (Units units in AllUnits.Where(x => x is Units))
            {
                if (units.CanFly)
                {
                    if (units.IsInTheAir)
                    {
                        units.Vision = Repository.UnitsConfig.Controllable.HelicopterConfig.Vision;
                    }
                    else
                    {
                        units.Vision = Repository.UnitsConfig.Controllable.HelicopterConfig.GroundVision;
                    }
                }
            }
        }
    }
}
