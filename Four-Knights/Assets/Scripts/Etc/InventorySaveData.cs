using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventorySaveData : SaveData
{
    public int[] level = new int[] { };
    public ItemWithUniqueIndex[] inventory = new ItemWithUniqueIndex[] { };
    public WeaponUniqueData[] weaponUniqueData = new WeaponUniqueData[] { };
    public AccessoriesUniqueData[] accessoriesUniqueData = new AccessoriesUniqueData[] { };
    public EtcUniqueData[] etcUniqueData = new EtcUniqueData[] { };
}
