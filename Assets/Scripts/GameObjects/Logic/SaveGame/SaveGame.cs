using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace HCC.GameObjects.Save
{
    [Serializable]
    public class SaveGame
    {
        public string FolderName;
        fsSerializer _Serializer = new fsSerializer();
        private string _FolderPath { get => Application.persistentDataPath + $"/{FolderName}/";}
        private string _FilePath { get => _FolderPath + "PlayerSave" + ".data";}


        public async Task Save() 
        {
            CheckFolderPath();
            SaveData saveData = new SaveData();
            saveData.Data.Add(new PlayerStatsSave());
            saveData.Data.ForEach(data => { data.Serialize(); });
        

            fsData data;
            _Serializer.TrySerialize(saveData, out data).AssertSuccessWithoutWarnings();
            string savejs = fsJsonPrinter.PrettyJson(data);
            using (StreamWriter writer = new StreamWriter(_FilePath))
            {
                await writer.WriteAsync(savejs + "_*.data");
                writer.Close();
            }

        }

        public async Task Load() 
        {
                StreamReader reader = new StreamReader(_FilePath);
                string save = await reader.ReadToEndAsync();
                reader.Close();
                fsData datajs = fsJsonParser.Parse(save);
                SaveData saveData = new SaveData();
                _Serializer.TryDeserialize(datajs, ref saveData).AssertSuccessWithoutWarnings();
                saveData.Data.ForEach(data => data.Deserialize());
        }

        private void CheckFolderPath()
        {
            if (!Directory.Exists(_FolderPath)) 
            { 
                Directory.CreateDirectory(_FolderPath);
            }

        }
    }

    [Serializable]
    public class SaveData
    {
       public List<ISaveData> Data = new List<ISaveData>();
    }
}
