using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    [SerializeField] CharacterData characterData;

    BuffWithTime[] buffList = new BuffWithTime[System.Enum.GetValues(typeof(BuffType)).Length];

    protected CharacterData CharacterData
    {
        get { return characterData; }
        set
        {
            characterData = value;
        }
    }

    int level;
    public int Level
    {
        get { return level; }
        protected set
        {
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

    protected virtual void Awake()
    {
        for (int i = 0; i < buffList.Length; i++)
        {
            buffList[i] = new BuffWithTime { buff = (BuffType)i, time = 0, value = 0 };
        }
        //buffList[(int)BuffType.Barrier] = new BuffWithTime { buff = BuffType.Barrier, time = 5, value = 1000 };
    }

    protected virtual void Update()
    {
        for (int i = 0; i < buffList.Length; i++)
        {
            //Debug.Log(buffList[i].time);
            buffList[i].time = Mathf.Max(buffList[i].time - Time.deltaTime, 0);
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
        float value = 0;

        //각종 단순 스탯버프 구현란
        switch (type)
        {

        }

        foreach (UpgradeStatWithValue upgradeStat in CharacterData.CharacterStat.levelUpStat)
        {
            if (upgradeStat.UpgradeStatType == type)
            {
                return upgradeStat.value * Level + value;
            }
        }
        return value;
    }

    public bool GetBuff(BuffType buffType)
    {
        return buffList[(int)buffType].time > 0;
    }

    public int GetBuffValue(BuffType buffType)
    {
        if (GetBuff(buffType))
        {
            foreach (BuffWithTime buff in buffList)
            {
                if (buff.buff == buffType)
                {
                    return buff.value;
                }
            }
        }
        return 0;
    }

    public void AddBuff(BuffWithTime buff)
    {
        if (buffList[(int)buff.buff].time < buff.time)
        {
            buffList[(int)buff.buff] = buff;
        }
    }

    public void ClearBuff(BuffType buff)
    {
        buffList[(int)buff] = new BuffWithTime { buff = buff, value = 0, time = 0 };
    }

    public void SetBuffValue(BuffType buffType, int value)
    {
        foreach (BuffWithTime buff in buffList)
        {
            if (buff.buff == buffType)
            {
                buffList[(int)buffType].value = value;
            }
        }
    }
}
