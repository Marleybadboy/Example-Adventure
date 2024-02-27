using System;


namespace HCC.GameObjects
{/* The StaticEvents class defines static events for taking damage, killing enemies, and loading a
game. */

    public static class StaticEvents 
    {
      public static void OnTakeDamage(object o, int damage) { onTakeDamage?.Invoke(o, damage);}
      public static event EventHandler<int> onTakeDamage;
      public static void OnEnemyKill(object o, int count) { onEnemyKill?.Invoke(o, count); }
      public static event EventHandler<int> onEnemyKill;
      public static void OnLoadGame(object o) { onLoadGame?.Invoke(o, EventArgs.Empty); }
      public static event EventHandler onLoadGame;


    }
}
