using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Etc Item Data", menuName = "Scriptable Object/Etc Item Data", order = int.MinValue)]
public class ItemStaticData : ScriptableObject
{
    [SerializeField] string itemName;
    public string ItemName
    {
        get { return itemName; }
    }

    [SerializeField] Sprite sprite;
    public Sprite Sprite
    {
        get { return sprite; }
    }

    public ItemType ItemType
    {
        get { return ItemType.Etc; }
    }
}