using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Accssorie Data",menuName = "Scriptable Object/Accssorie Data",order =int.MinValue)]
public class AccssoriesStaticData : ItemStaticData
{
    [SerializeField] UpgradeStatWithValue[] upgradeStat;
    public UpgradeStatWithValue[] UpgradeStat
    {
        get { return upgradeStat; }
    }
}
