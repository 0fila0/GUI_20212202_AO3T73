using HarciKalapacs.Repository;
using HarciKalapacs.Repository.GameElements;
using System;
using System.Collections.Generic;

namespace HarciKalapacs.Model
{
    public class Model : IModel
    {
        readonly IRepository repository;
        int mapWidth;
        int mapHeight;
        IEnumerable<Units> allUnits;
        int playerTurn;
        int playerGold;
        // int enemyGold;
        int round;

        public Model(IRepository repository)
        {
            this.repository = repository;
            this.AllUnits = new List<Units>();
        }

        public IEnumerable<Units> AllUnits { get => allUnits; set => allUnits = value; }
        public int MapWidth { get => mapWidth; set => mapWidth = value; }
        public int MapHeight { get => mapHeight; set => mapHeight = value; }
        public int PlayerTurn { get => playerTurn; set => playerTurn = value; }

        public bool LoadMap(int level)
        {
            (this.AllUnits as List<Units>).Clear();
            bool success = this.repository.LoadMap(level);
            this.AllUnits = this.repository.AllUnits;
            this.ModifyAirUnitsVision();
            this.mapWidth = (this.repository.MapSize as List<int>)[0];
            this.mapHeight = (this.repository.MapSize as List<int>)[1];
            this.PlayerTurn = 1;
            return success;
        }

        /// <summary>
        /// It is called only once, only when start a new game or load the saved game.
        /// </summary>
        private void ModifyAirUnitsVision()
        {
            foreach (Units units in this.allUnits)
            {
                if (units is AirUnit)
                {
                    if ((units as AirUnit).IsInTheAir)
                    {
                        (units as AirUnit).Vision = Repository.UnitsConfig.Controllable.HelicopterConfig.Vision;
                    }
                    else
                    {
                        (units as AirUnit).Vision = Repository.UnitsConfig.Controllable.HelicopterConfig.GroundVision;
                    }
                }
            }
        }
    }
}
