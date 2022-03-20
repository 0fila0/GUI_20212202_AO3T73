using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Logic
{
    public interface IGeneralLogic
    {
        /// <summary>
        /// Loads a map.
        /// </summary>
        /// <param name="level">Level of the map.</param>
        /// <returns>True if the loading was successful.</returns>
        public bool StartNewGame(int level);

        /// <summary>
        /// Loads the saved game.
        /// </summary>
        /// <returns>True if the loading was successful.</returns>
        public bool LoadGame();

        /// <summary>
        /// Saves the game.
        /// </summary>
        /// <returns>True if the saving was successful.</returns>
        public bool SaveGame();
    }
}
