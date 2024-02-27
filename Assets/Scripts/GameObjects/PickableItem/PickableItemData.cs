using HCC.GameObjects.PickableItem;
using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName ="Pickable Data/Pickable", fileName = "Pickable Object")]
public class PickableItemData : ScriptableObject
{
    public enum PickableType {HEALTHPOTION, AMMOCRATE}

    public GameObject _PickablePrefab;
    public PickableType _PickableType;

    [SerializeReference] public PickableItem _ItemBehviour;

    

    public GameObject CreatePickableItem() 
    { 
        if(_PickablePrefab.GetComponent<PickableItem>() == null) 
        { 
            GameObject obj = Instantiate(_PickablePrefab);
            _ItemBehviour = GetPickable(_PickableType);
            PickableItem item = (PickableItem)obj.AddComponent(_ItemBehviour.GetType());
            return obj;
        
        }
        else 
        {
            GameObject obj = Instantiate(_PickablePrefab);
            return obj;
        }
    }

    private PickableItem GetPickable(PickableType type) 
    { 
        switch(type) 
        { 
            case PickableType.HEALTHPOTION:
                return new HealthPotion();
            case PickableType.AMMOCRATE:
                return new AmmoCrate();
            default: 
                return new HealthPotion();
        }
    
    }
}
