using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    protected StatManager statManager;

    float[] basicCool;
    float skillCool;
    float ultimateSkillCool;

    protected virtual void Start()
    {
        statManager = GetComponent<StatManager>();
        basicCool = new float[statManager.BasicAttacks.Length];
    }

    public void UseAttack(int attackIndex)
    {
        InstantiateAttack(statManager.BasicAttacks[attackIndex]);
    }

    public void UseSkill()
    {
        Instantiate(statManager.Skill);
    }

    public void voidUseUltimateSkill()
    {
        Instantiate(statManager.UltimateSkill);
    }

    void InstantiateAttack(AttackData data)
    {
        GameObject prefab = Instantiate(data.Prefab);
        prefab.GetComponent<HitBox>().attacker = gameObject;
        prefab.GetComponent<HitBox>().UsedStatValue = statManager.GetStat(data.UsedStatType);
    }
}
