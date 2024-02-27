using HCC.GameObjects.GUI;
using HCC.GameObjects.Player;
using System;

namespace HCC.GameObjects.PickableItem
{
    [Serializable]
  /* The AmmoCrate class is a subclass of PickableItem that assigns the player's available ammo and
  initializes the ammo GUI. */
    public class AmmoCrate : PickableItem
    {
        public override void AssignAction()
        {
            PlayerStats.AvailableAmmo = GameManager.Instance._PlayerAmmo;
            PlayerGUIManager.instance?.InitializeAmmo(GameManager.Instance._PlayerAmmo);
            base.AssignAction();
        }
    }
}
