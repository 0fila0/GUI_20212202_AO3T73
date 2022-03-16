namespace HarciKalapacs.Repository
{
    using HarciKalapacs.Repository.GameElements;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class Repository : IRepository
    {
        List<Units> allUnits;
        List<int> mapSize;

        public Repository()
        {
            this.allUnits = new List<Units>();
        }

        public IEnumerable<Units> AllUnits => allUnits;

        public IEnumerable<int> MapSize => mapSize;

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
                    helicopters.ForEach(x => this.allUnits.Add(x));
                }

                path = relativePath + @"\tank.json";
                json = ReadJsonFile(path);
                if (json != string.Empty)
                {
                    List<Tank> tanks = JsonConvert.DeserializeObject<List<Tank>>(json);
                    tanks.ForEach(x => this.allUnits.Add(x));
                }

                path = relativePath + @"\infantry.json";
                json = ReadJsonFile(path);
                if (json != string.Empty)
                {
                    List<Infantryman> infantries = JsonConvert.DeserializeObject<List<Infantryman>>(json);
                    infantries.ForEach(x => this.allUnits.Add(x));
                }

                path = relativePath + @"\truck.json";
                json = ReadJsonFile(path);
                if (json != string.Empty)
                {
                    List<Truck> trucks = JsonConvert.DeserializeObject<List<Truck>>(json);
                    trucks.ForEach(x => this.allUnits.Add(x));
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

                this.allUnits.ForEach(x => x.IdleImage1 = "");
                path = relativePath + @"\mapSize.json";
                json = ReadJsonFile(path);
                List<int> map = JsonConvert.DeserializeObject<List<int>>(json);
                this.mapSize = map;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool LoadSavedGame()
        {
            throw new NotImplementedException();
        }

        public bool SaveGame()
        {
            throw new NotImplementedException();
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
