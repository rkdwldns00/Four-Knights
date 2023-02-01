using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Weapon Data",menuName ="Scriptable Object/Weapon Data",order = int.MinValue)]
public class WeaponStaticData : ItemStaticData
{
    public override ItemType ItemType {
        get
        {
            return ItemType.Weapon;
        }
    }



    [SerializeField] int attack;
    public int Attack
    {
        get { return attack; }
    }

    [SerializeField] UpgradeStatWithValue[] upgradeStat;
    public UpgradeStatWithValue[] UpgradeStat
    {
        get { return upgradeStat; }
    }

    [SerializeField]WeaponType weaponType;
    public WeaponType WeaponType
    {
        get { return weaponType; }
    }
}
