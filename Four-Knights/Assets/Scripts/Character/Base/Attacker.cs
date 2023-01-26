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

    protected void Update()
    {
        for(int i = 0; i < basicCool.Length; i++)
        {
            basicCool[i] -= Time.deltaTime;
        }
        skillCool-=Time.deltaTime;
        ultimateSkillCool-=Time.deltaTime;
    }

    public void UseAttack(int attackIndex)
    {
        if (basicCool[attackIndex] <= 0 ) {
            basicCool[attackIndex] = statManager.BasicAttacks[attackIndex].CoolTime;
            InstantiateAttack(statManager.BasicAttacks[attackIndex]);
        }
    }

    public void UseSkill()
    {
        if (skillCool < 0)
        {
            skillCool = statManager.Skill.CoolTime;
            Instantiate(statManager.Skill);
        }
    }

    public void UseUltimateSkill()
    {
        if (ultimateSkillCool < 0)
        {
            ultimateSkillCool = statManager.UltimateSkill.CoolTime;
            Instantiate(statManager.UltimateSkill);
        }
    }

    public virtual void ReciveVictimObject(GameObject victim)
    {

    }

    void InstantiateAttack(AttackData data)
    {
        GameObject prefab = Instantiate(data.Prefab);
        prefab.GetComponent<HitBox>().attacker = gameObject;
        prefab.GetComponent<HitBox>().UsedStatValue = statManager.GetStat(data.UsedStatType);
    }
}
