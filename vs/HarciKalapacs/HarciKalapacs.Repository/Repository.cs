namespace HarciKalapacs.Repository
{
    using HarciKalapacs.Repository.GameElements;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Repository : IRepository
    {
        List<IMapItem> allUnits;
        List<int> mapSize;
        List<int> otherDetails;
        List<string[]> SavedGames;

        public Repository()
        {
            //lehet szar
            this.allUnits = new List<IMapItem>();

            // Looks at the SavedGames directory, and gets the name and the path of all folders and stores it in List<string[]> SavedGames
            this.SavedGames = new List<string[]>();
            string savedir = Directory.GetCurrentDirectory() + @"\Maps" + @"\SavedGames";
            var temp = Directory.GetFileSystemEntries(savedir).ToList();
            foreach(var item in temp)
            {
                string dirname = Path.GetFileName(item);
                string[] array = {dirname, savedir+@"\"+dirname };
                SavedGames.Add(array);
            }
        }

        public ICollection<IMapItem> AllUnits => allUnits;

        public IEnumerable<int> MapSize => mapSize;

        public IEnumerable<int> OtherDetails => otherDetails;

        public bool LoadMap(int level)
        {
            string json = string.Empty;
            List<Units> u = new List<Units>();
            try
            {
                string relativePath = Directory.GetCurrentDirectory() + @"\Maps\" + level.ToString() + @"\";

                string path = relativePath + @"\helicopter.json";
                json = ReadJsonFile(path);
                if (json != string.Empty)
                {
                    List<Helicopter> helicopters = JsonConvert.DeserializeObject<List<Helicopter>>(json);
                    //helicopters.ForEach(x => this.allUnits.Add(x));
                    foreach (var item in helicopters)
                    {
                        item.UnitType = UnitType.Helicopter;
                        this.allUnits.Add(item);
                    }
                }

                path = relativePath + @"\tank.json";
                json = ReadJsonFile(path);
                if (json != string.Empty)
                {
                    List<Tank> tanks = JsonConvert.DeserializeObject<List<Tank>>(json);
                    //tanks.ForEach(x => this.allUnits.Add(x));
                    foreach (var item in tanks)
                    {
                        item.UnitType = UnitType.Tank;
                        this.allUnits.Add(item);
                    }
                }

                path = relativePath + @"\infantry.json";
                json = ReadJsonFile(path);
                if (json != string.Empty)
                {
                    List<Infantryman> infantries = JsonConvert.DeserializeObject<List<Infantryman>>(json);
                    //infantries.ForEach(x => this.allUnits.Add(x));
                    foreach (var item in infantries)
                    {
                        item.UnitType = UnitType.InfantryMan;
                        this.allUnits.Add(item);
                    }
                }

                path = relativePath + @"\truck.json";
                json = ReadJsonFile(path);
                if (json != string.Empty)
                {
                    List<Truck> trucks = JsonConvert.DeserializeObject<List<Truck>>(json);
                    //trucks.ForEach(x => this.allUnits.Add(x));
                    foreach (var item in trucks)
                    {
                        item.UnitType = UnitType.Truck;
                        this.allUnits.Add(item);
                    }
                }

                path = relativePath + @"\obstacle.json";
                json = ReadJsonFile(path);
                if (json != string.Empty)
                {
                    List<Obstacle> obstacles = JsonConvert.DeserializeObject<List<Obstacle>>(json);
                    obstacles.ForEach(x => this.allUnits.Add(x));
                }

                path = relativePath + @"\cover.json";
                json = ReadJsonFile(path);
                if (json != string.Empty)
                {
                    List<Cover> covers = JsonConvert.DeserializeObject<List<Cover>>(json);
                    covers.ForEach(x => this.allUnits.Add(x));
                }

                path = relativePath + @"\fort.json";
                json = ReadJsonFile(path);
                if (json != string.Empty)
                {
                    List<Fort> forts = JsonConvert.DeserializeObject<List<Fort>>(json);
                    forts.ForEach(x => this.allUnits.Add(x));
                }

                //idle Image 1 to idleimage
                this.allUnits.ForEach(x => x.IdleImage1 = "");

                path = relativePath + @"\mapSize.json";
                json = ReadJsonFile(path);
                List<int> map = JsonConvert.DeserializeObject<List<int>>(json);
                this.mapSize = map;

                path = relativePath + @"\otherDetails.json";
                json = ReadJsonFile(path);
                List<int> details = JsonConvert.DeserializeObject<List<int>>(json);
                this.otherDetails = details;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Functionally same ad LoadMap, but gets items form a save directory, which can be found in this.SavedGames. Nulls mapSive, otherDetails and allUnits before doing anything.
        /// </summary>
        /// <param name="savedGamePath"> second item of string[] savedGames, first is a name to write out</param>
        /// <returns>True or Fase, according exception</returns>
        public bool LoadSavedGame(string savedGamePath)
        {
            this.mapSize.RemoveAll(x => x == x);
            this.otherDetails.RemoveAll(x => x == x);
            this.allUnits.RemoveAll(x => x == x);
            string json = string.Empty;
            List<Units> u = new List<Units>();
            try
            {
                string relativePath = savedGamePath + @"\";

                string path = relativePath + @"\helicopter.json";
                json = ReadJsonFile(path);
                if (json != string.Empty)
                {
                    List<Helicopter> helicopters = JsonConvert.DeserializeObject<List<Helicopter>>(json);
                    //helicopters.ForEach(x => this.allUnits.Add(x));
                    foreach (var item in helicopters)
                    {
                        item.UnitType = UnitType.Helicopter;
                        this.allUnits.Add(item);
                    }
                }

                path = relativePath + @"\tank.json";
                json = ReadJsonFile(path);
                if (json != string.Empty)
                {
                    List<Tank> tanks = JsonConvert.DeserializeObject<List<Tank>>(json);
                    //tanks.ForEach(x => this.allUnits.Add(x));
                    foreach (var item in tanks)
                    {
                        item.UnitType = UnitType.Tank;
                        this.allUnits.Add(item);
                    }
                }

                path = relativePath + @"\infantry.json";
                json = ReadJsonFile(path);
                if (json != string.Empty)
                {
                    List<Infantryman> infantries = JsonConvert.DeserializeObject<List<Infantryman>>(json);
                    //infantries.ForEach(x => this.allUnits.Add(x));
                    foreach (var item in infantries)
                    {
                        item.UnitType = UnitType.InfantryMan;
                        this.allUnits.Add(item);
                    }
                }

                path = relativePath + @"\truck.json";
                json = ReadJsonFile(path);
                if (json != string.Empty)
                {
                    List<Truck> trucks = JsonConvert.DeserializeObject<List<Truck>>(json);
                    //trucks.ForEach(x => this.allUnits.Add(x));
                    foreach (var item in trucks)
                    {
                        item.UnitType = UnitType.Truck;
                        this.allUnits.Add(item);
                    }
                }

                path = relativePath + @"\obstacle.json";
                json = ReadJsonFile(path);
                if (json != string.Empty)
                {
                    List<Obstacle> obstacles = JsonConvert.DeserializeObject<List<Obstacle>>(json);
                    obstacles.ForEach(x => this.allUnits.Add(x));
                }

                path = relativePath + @"\cover.json";
                json = ReadJsonFile(path);
                if (json != string.Empty)
                {
                    List<Cover> covers = JsonConvert.DeserializeObject<List<Cover>>(json);
                    covers.ForEach(x => this.allUnits.Add(x));
                }

                path = relativePath + @"\fort.json";
                json = ReadJsonFile(path);
                if (json != string.Empty)
                {
                    List<Fort> forts = JsonConvert.DeserializeObject<List<Fort>>(json);
                    forts.ForEach(x => this.allUnits.Add(x));
                }

                //idle Image 1 to idleimage
                this.allUnits.ForEach(x => x.IdleImage1 = "");

                path = relativePath + @"\mapSize.json";
                json = ReadJsonFile(path);
                List<int> map = JsonConvert.DeserializeObject<List<int>>(json);
                this.mapSize = map;

                path = relativePath + @"\otherDetails.json";
                json = ReadJsonFile(path);
                List<int> details = JsonConvert.DeserializeObject<List<int>>(json);
                this.otherDetails = details;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Uses the same save format ad LoadMap, created a save directory in tha Maps/Savedgames/ forectory, with the current Datetime, then deserializes all object in AllUnits, mapSize and otherDetails
        /// </summary>
        /// <returns>True ir false according to exception</returns>
        public  bool SaveGame()
        {
            try
            {
                string savedir = Directory.GetCurrentDirectory() + @"\Maps" + @"\SavedGames" + @"\Game_" + DateTime.Now.ToString().Replace(".","_").Replace(":","-").Replace(" ","");
                Directory.CreateDirectory(savedir);

                List<Tank> tankListToSerialize = new List<Tank>();
                foreach (Tank item in AllUnits.OfType<Tank>())
                {
                    tankListToSerialize.Add(item);
                }
                File.WriteAllText(savedir + @"\tank.json",JsonConvert.SerializeObject(tankListToSerialize));

                List<Infantryman> infantryListToSerialize = new List<Infantryman>();
                foreach (Infantryman item in AllUnits.OfType<Infantryman>())
                {
                    infantryListToSerialize.Add(item);
                }
                File.WriteAllText(savedir + @"\infantry.json", JsonConvert.SerializeObject(infantryListToSerialize)); 
                
                List<Helicopter> helicopterListToSerialize = new List<Helicopter>();
                foreach (Helicopter item in AllUnits.OfType<Helicopter>())
                {
                    helicopterListToSerialize.Add(item);
                }
                File.WriteAllText(savedir + @"\helicopter.json", JsonConvert.SerializeObject(helicopterListToSerialize)); 
                
                List<Truck> truckListToSerialize = new List<Truck>();
                foreach (Truck item in AllUnits.OfType<Truck>())
                {
                    truckListToSerialize.Add(item);
                }
                File.WriteAllText(savedir + @"\truck.json", JsonConvert.SerializeObject(truckListToSerialize)); 
                
                List<Cover> coverListToSerialize = new List<Cover>();
                foreach (Cover item in AllUnits.OfType<Cover>())
                {
                    coverListToSerialize.Add(item);
                }
                File.WriteAllText(savedir + @"\cover.json", JsonConvert.SerializeObject(coverListToSerialize)); 
                
                List<Fort> fortListToSerialize = new List<Fort>();
                foreach (Fort item in AllUnits.OfType<Fort>())
                {
                    fortListToSerialize.Add(item);
                }
                File.WriteAllText(savedir + @"\fort.json", JsonConvert.SerializeObject(fortListToSerialize));

                List<Obstacle> obstacleListToSerialize = new List<Obstacle>();
                foreach (Obstacle item in AllUnits.OfType<Obstacle>())
                {
                    obstacleListToSerialize.Add(item);
                }
                File.WriteAllText(savedir + @"\obstacle.json", JsonConvert.SerializeObject(obstacleListToSerialize));

                File.WriteAllText(savedir + @"\mapSize.json", JsonConvert.SerializeObject(this.mapSize));
                File.WriteAllText(savedir + @"\otherDetails.json", JsonConvert.SerializeObject(this.otherDetails));

                // Adding new save to SavedGames List
                string dirname = Path.GetFileName(savedir);
                string[] array = { dirname, savedir };
                SavedGames.Add(array);

                return true;
                
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// Read json file.
        /// </summary>
        /// <param name="path">File location.</param>
        /// <returns>Content of the file.</returns>
        private static string ReadJsonFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            string json = sr.ReadToEnd();
            sr.Close();

            return json;
        }
    }
}
