using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Table", menuName = "Scriptable Object/Item Table", order = int.MinValue)]
public class ItemTable : ScriptableObject
{
    [SerializeField] ItemStaticData[] itemTable;
    public ItemStaticData[] Table
    {
        get { return itemTable; }
    }
}
