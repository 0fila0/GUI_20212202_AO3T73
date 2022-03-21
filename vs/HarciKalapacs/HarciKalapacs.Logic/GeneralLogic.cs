using HarciKalapacs.Model;
using System;

namespace HarciKalapacs.Logic
{
    public class GeneralLogic : IGeneralLogic
    {
        IModel model;

        public GeneralLogic(IModel model)
        {
            this.model = model;
        }

        public bool LoadGame()
        {
            throw new NotImplementedException();
        }

        public bool SaveGame()
        {
            throw new NotImplementedException();
        }

        public bool StartNewGame(int level)
        {
            return this.model.LoadMap(level);
        }
    }
}
