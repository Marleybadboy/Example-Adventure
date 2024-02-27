using HCC.GameObjects.Player;
using HCC.GameObjects.Save;
using System;

[Serializable]
public class PlayerStatsSave : ISaveData
{
    public PlayerStatData Data;
    public void Deserialize()
    {
        PlayerStats.PlayerHealth = Data.PlayerHealth;
        PlayerStats.KilledEnemies = Data.KilledEnemies;
        PlayerStats.EnemyLevelMultiplayer = Data.EnemyLevelMultiplayer;
        PlayerStats.CurrentLevel = Data.CurrentLevel;
        PlayerStats.LevelMapIndex = Data.LevelMapIndex;
        PlayerStats.AvailableAmmo = Data.PlayerAmmo;
    }

    public void Serialize()
    {
        Data = new PlayerStatData
        {
            PlayerHealth = PlayerStats.PlayerHealth,
            KilledEnemies = PlayerStats.KilledEnemies,
            EnemyLevelMultiplayer = PlayerStats.EnemyLevelMultiplayer,
            CurrentLevel = PlayerStats.CurrentLevel,
            LevelMapIndex = PlayerStats.LevelMapIndex,
            PlayerAmmo = PlayerStats.AvailableAmmo

        };
    }
}

[Serializable]
public struct PlayerStatData 
{
    public int PlayerHealth;
    public int KilledEnemies;
    public int EnemyLevelMultiplayer;
    public int CurrentLevel;
    public int LevelMapIndex;
    public int PlayerAmmo;

}
