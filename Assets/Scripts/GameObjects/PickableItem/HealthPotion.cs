using HCC.GameObjects.GUI;
using HCC.GameObjects.Player;
using System;
using UnityEngine;

namespace HCC.GameObjects.PickableItem
{
    [Serializable]
  /* The HealthPotion class is a subclass of PickableItem that assigns an action to restore the
  player's health and update the player's GUI. */
    public class HealthPotion : PickableItem
    {
        public override void AssignAction()
        {
            PlayerStats.PlayerHealth = GameManager.Instance._PlayerHealth;
            PlayerGUIManager.instance?.CalulateHealth();
            base.AssignAction();
        }
    }
}
