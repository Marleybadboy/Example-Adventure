

using HCC.GameObjects.Enemy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Unity.VisualScripting.FullSerializer;

namespace HCC.GameObjects.MainMenu
{
   /* The SaveEnemyData class provides methods to save and load enemy data to and from a file. */
    public static class SaveEnemyData
    {
        /* The line `public static List<EnemyData> EnemyDataBase = new List<EnemyData>();` is declaring
        a public static variable named `EnemyDataBase` of type `List<EnemyData>`. It is initializing
        this variable with a new instance of `List<EnemyData>`. This list will be used to store
        enemy data. */
        public static List<EnemyData> EnemyDataBase = new List<EnemyData>();
        private static string FolderName = "EnemyDataFiles/";
        private static string _FilePath { get => FolderName + "PlayerEnemyData" + ".data"; }

       /// <summary>
       /// The function `SaveData` saves the EnemyDataBase object to a file in JSON format.
       /// </summary>
        public static async Task SaveData()
        {
            CheckFolderExist();
            SaveEnemyDataBase saveData = new SaveEnemyDataBase { _SaveEnemyDataBase = EnemyDataBase };


            fsData data;
            fsSerializer fsSerializer = new fsSerializer();
            fsSerializer.TrySerialize(saveData, out data).AssertSuccessWithoutWarnings();
            string savejs = fsJsonPrinter.PrettyJson(data);
            using (StreamWriter writer = new StreamWriter(_FilePath))
            {
                await writer.WriteAsync(savejs + "_*.data");
                writer.Close();
            }

        }

       /// <summary>
       /// The function `LoadData` reads data from a file, deserializes it, and assigns it to the
       /// `EnemyDataBase` variable.
       /// </summary>
        public static async Task LoadData()
        {
            if (CheckFileExist())
            {
                fsSerializer fsSerializer = new fsSerializer();
                StreamReader reader = new StreamReader(_FilePath);
                string save = await reader.ReadToEndAsync();
                reader.Close();
                fsData datajs = fsJsonParser.Parse(save);
                SaveEnemyDataBase saveData = new SaveEnemyDataBase();
                fsSerializer.TryDeserialize(datajs, ref saveData).AssertSuccessWithoutWarnings();
                EnemyDataBase = saveData._SaveEnemyDataBase;
            }


        }

       /// <summary>
       /// The function checks if a folder exists and creates it if it doesn't.
       /// </summary>
        public static void CheckFolderExist()
        {
            if (!Directory.Exists(FolderName))
            {
                Directory.CreateDirectory(FolderName);
            }

        }

        public static bool CheckFileExist()
        {
            return File.Exists(_FilePath);
        }
    }
    [Serializable]
    public class SaveEnemyDataBase
    {
        public List<EnemyData> _SaveEnemyDataBase = new List<EnemyData>();

    }
}
