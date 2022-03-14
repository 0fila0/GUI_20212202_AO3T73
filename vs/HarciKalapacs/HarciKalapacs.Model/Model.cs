using HarciKalapacs.Repository;
using HarciKalapacs.Repository.GameElements;
using System;
using System.Collections.Generic;

namespace HarciKalapacs.Model
{
    public class Model : IModel
    {
        readonly IRepository repository;
        IEnumerable<Units> allUnits;

        public Model(IRepository repository)
        {
            this.repository = repository;
            this.AllUnits = new List<Units>();
        }

        public IEnumerable<Units> AllUnits { get => allUnits; set => allUnits = value; }

        public bool LoadMap(int level)
        {
            bool success = this.repository.LoadMap(level);
            this.AllUnits = this.repository.AllUnits;
            return success;
        }
    }
}
