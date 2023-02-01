using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Accssorie Data",menuName = "Scriptable Object/Accssorie Data",order =int.MinValue)]
public class AccssoriesStaticData : ItemStaticData
{
    public override ItemType ItemType
    {
        get { return ItemType.Accessories; }
    }

    [SerializeField] UpgradeStatWithValue[] maxUpgradeStatList;
    public UpgradeStatWithValue[] MaxUpgradeStatList
    {
        get { return maxUpgradeStatList; }
    }
}
