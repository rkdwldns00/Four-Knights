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

    [SerializeField] int exp;
    public int Exp
    {
        get { return exp; }
    }

    [SerializeField] UpgradeStatWithValue[] maxUpgradeStatList;
    public UpgradeStatWithValue[] MaxUpgradeStatList
    {
        get { return maxUpgradeStatList; }
    }

    [SerializeField] SetSkillType set;
    public SetSkillType Set
    {
        get { return set; }
    }
}
