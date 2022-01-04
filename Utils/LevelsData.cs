﻿using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Color_Breaker
{
    public sealed class LevelsData : ILevels
    {
        List<LevelData> _levels;

        public LevelsData()
        {
            _levels = new List<LevelData>();
            Services.RegisterService<ILevels>(this);
        }

        public void LoadLevels(string directory)
        {
            MemoryStream stream;
            string[] fileEntries = Directory.GetFiles(directory);
            for (int i = 0; i < fileEntries.Length; i++)
            {
                stream = new MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText(fileEntries[i])));
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(LevelData));
                _levels.Add((LevelData)ser.ReadObject(stream));
                Debug.WriteLine(fileEntries[i]);
            }
        }

        public LevelData GetLevel(int levelNumber)
        {
            return _levels[levelNumber];
        }
    }



    [DataContract]
    public class LevelData
    {
        [DataMember]
        public List<List<int>> bricks { get; private set; }
    }

}