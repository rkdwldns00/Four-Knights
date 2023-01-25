using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    [SerializeField] protected CharacterData characterData;

    public int Level { get; protected set; }

    public AttackData[] BasicAttacks
    {
        get
        {
            return characterData.BasicAttacks;
        }
    }

    public AttackData Skill
    {
        get
        {
            return characterData.Skill;
        }
    }

    public AttackData UltimateSkill
    {
        get
        {
            return characterData.Skill;
        }
    }

    public CharacterStat GetStat()
    {
        CharacterStat value;
        value = characterData.CharacterStat;
        value.health = (int)((float)(value.health + (int)FindUpgradeStat(UpgradeStatType.Health)) * (1f + FindUpgradeStat(UpgradeStatType.HealthPercent)));
        value.attack = (int)((float)(value.attack + (int)FindUpgradeStat(UpgradeStatType.Attack)) * (1f + FindUpgradeStat(UpgradeStatType.AttackPercent)));
        value.defense = (int)((float)(value.defense + (int)FindUpgradeStat(UpgradeStatType.Defence)) * (1f + FindUpgradeStat(UpgradeStatType.DefensePercent)));
        value.criticalPercent = characterData.CharacterStat.criticalPercent + FindUpgradeStat(UpgradeStatType.CriticalPercent);
        value.criticalDamage = characterData.CharacterStat.criticalDamage + FindUpgradeStat(UpgradeStatType.CriticalDamage);
        return value;
    }

    protected virtual float FindUpgradeStat(UpgradeStatType type)
    {
        foreach (UpgradeStatWithValue upgradeStat in characterData.CharacterStat.levelUpStat)
        {
            if (upgradeStat.UpgradeStatType == type)
            {
                return upgradeStat.value * Level;
            }
        }
        return -1;
    }
}
