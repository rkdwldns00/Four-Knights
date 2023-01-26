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

    public float GetStat(StatType stat)
    {
        CharacterStat value;
        value = characterData.CharacterStat;
        switch (stat)
        {
            case StatType.Attack:
                return (int)((float)(value.attack + (int)FindUpgradeStat(UpgradeStatType.Attack)) * (1f + FindUpgradeStat(UpgradeStatType.AttackPercent)));
            case StatType.Health:
                return (int)((float)(value.health + (int)FindUpgradeStat(UpgradeStatType.Health)) * (1f + FindUpgradeStat(UpgradeStatType.HealthPercent)));
            case StatType.Defence:
                return (int)((float)(value.defense + (int)FindUpgradeStat(UpgradeStatType.Defence)) * (1f + FindUpgradeStat(UpgradeStatType.DefensePercent)));
            case StatType.UltimateCharge:
                return FindUpgradeStat(UpgradeStatType.UltimateCharge);
            default:
                Debug.LogError("GetStat 함수가 잘못된타입을 검색하였습니다.");
                return -1;
        }

        value.criticalPercent = characterData.CharacterStat.criticalPercent + FindUpgradeStat(UpgradeStatType.CriticalPercent);
        value.criticalDamage = characterData.CharacterStat.criticalDamage + FindUpgradeStat(UpgradeStatType.CriticalDamage);
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
