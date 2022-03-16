﻿using HarciKalapacs.Repository.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Repository
{
    public interface IRepository
    {
        public IEnumerable<int> MapSize { get; }

        public IEnumerable<Units> AllUnits { get; }

        /// <summary>
        /// Reads one of map.json file. Fills AllUnits list with units.
        /// </summary>
        /// <param name="level">Level of the map.</param>
        /// <returns>True if the loading was successful.</returns>
        public bool LoadMap(int level);

        /// <summary>
        /// Reads the savedGame.json file. Fills AllUnits list with units.
        /// </summary>
        /// <returns>True if the loading was successful.</returns>
        public bool LoadSavedGame();

        /// <summary>
        /// Saves the game.
        /// </summary>
        /// <returns>True if the saving was successful.</returns>
        public bool SaveGame();
    }
}
