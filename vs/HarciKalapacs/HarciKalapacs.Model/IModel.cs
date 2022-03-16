﻿using HarciKalapacs.Repository.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarciKalapacs.Model
{
    public interface IModel
    {
        /// <summary>
        /// Contains all units that can find on the actual map.
        /// </summary>
        IEnumerable<Units> AllUnits { get; set; }

        /// <summary>
        /// Fills AllUnits list with units.
        /// </summary>
        /// <param name="level">Level of the map.</param>
        /// <returns>True if the loading was successful.</returns>
        bool LoadMap(int level);
    }
}