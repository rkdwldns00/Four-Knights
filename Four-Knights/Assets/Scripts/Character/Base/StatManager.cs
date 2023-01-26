using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    [SerializeField] CharacterData characterData;
    protected CharacterData CharacterData
    {
        get { return characterData; }
        set
        {
            characterData = value;
        }
    }

    int level;
    public int Level {
        get { return level; }
        protected set { 
            level = value;
        }
    }

    public AttackData[] BasicAttacks
    {
        get
        {
            return CharacterData.BasicAttacks;
        }
    }

    public AttackData Skill
    {
        get
        {
            return CharacterData.Skill;
        }
    }

    public AttackData UltimateSkill
    {
        get
        {
            return CharacterData.Skill;
        }
    }

    public float GetStat(StatType stat)
    {
        CharacterStat value;
        value = CharacterData.CharacterStat;
        switch (stat)
        {
            case StatType.Attack:
                return ((float)value.attack + FindUpgradeStat(UpgradeStatType.Attack)) * (1f + FindUpgradeStat(UpgradeStatType.AttackPercent));
            case StatType.Health:
                return ((float)value.health + FindUpgradeStat(UpgradeStatType.Health)) * (1f + FindUpgradeStat(UpgradeStatType.HealthPercent));
            case StatType.Defence:
                return ((float)value.defense + FindUpgradeStat(UpgradeStatType.Defence)) * (1f + FindUpgradeStat(UpgradeStatType.DefensePercent));
            case StatType.UltimateCharge:
                return FindUpgradeStat(UpgradeStatType.UltimateCharge);
            case StatType.CriticalDamage:
                return value.criticalDamage + FindUpgradeStat(UpgradeStatType.CriticalDamage);
            case StatType.CriticalPercent:
                return value.criticalPercent + FindUpgradeStat(UpgradeStatType.CriticalPercent);
            default:
                Debug.LogError("GetStat 함수가 잘못된타입을 검색하였습니다.");
                return -1;
        }
    }

    protected virtual float FindUpgradeStat(UpgradeStatType type)
    {
        foreach (UpgradeStatWithValue upgradeStat in CharacterData.CharacterStat.levelUpStat)
        {
            if (upgradeStat.UpgradeStatType == type)
            {
                return upgradeStat.value * Level;
            }
        }
        return 0;
    }
}
