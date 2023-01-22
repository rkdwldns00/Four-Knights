using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CharacterStat
{
    public int health;
    public int attack;
    public float defense;
    public float criticalPercent;
    public float criticalDamage;
    public UpgradeStatWithValue[] levelUpStat;
    public WeaponType weaponType;
}
