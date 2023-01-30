using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedItemStatManager : ItemStatManager
{
    public override Item[] Inventory
    {
        get {
            return DataManager.instance.GetData().inventory; }
        protected set { DataManager.instance.SetInventoryData(value); }
    }
}
