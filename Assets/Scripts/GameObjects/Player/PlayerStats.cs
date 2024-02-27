



namespace HCC.GameObjects.Player
{
/* The PlayerStats class contains properties for player health, killed enemies, enemy level
multiplayer, current level, level map index, and available ammo. */
    public static class PlayerStats 
    {
        public static int PlayerHealth { get;  set; }
        public static int KilledEnemies { get; set;}

        public static int EnemyLevelMultiplayer { get; set; }

        public static int CurrentLevel { get; set; }

        public static int LevelMapIndex { get; set;}

        public static int AvailableAmmo { get; set; }
    }
}
