using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    protected StatManager statManager;
    AnimatorManager animator;

    float basicCool;
    float skillCool;
    float ultimateSkillCool;

    int attackIndex;

    protected virtual void Start()
    {
        statManager = GetComponent<StatManager>();
        animator = GetComponent<AnimatorManager>();
    }

    protected void Update()
    {
        basicCool -= Time.deltaTime;
        skillCool -= Time.deltaTime;
        ultimateSkillCool -= Time.deltaTime;
    }

    public void UseAttack()
    {
        if (basicCool <= 0)
        {
            if (animator.IsIdle)
            {
                attackIndex = 0;
            }
            basicCool = statManager.BasicAttacks[attackIndex].CoolTime;
            InstantiateAttack(statManager.BasicAttacks[attackIndex]);
            if (attackIndex == 6)
            {
                attackIndex = 0;
            }
            else
            {
                attackIndex += 1;
            }
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

    public virtual void ReceiveVictimObject(GameObject victim)
    {

    }

    void InstantiateAttack(AttackData data)
    {
        GameObject prefab = Instantiate(data.Prefab,transform.position,transform.rotation);
        prefab.GetComponent<HitBox>().attacker = gameObject;
        float damage = statManager.GetStat(data.UsedStatType);
        if (Random.value < (statManager.GetStat(StatType.CriticalPercent) / 100f))
        {
            damage *= (1f + statManager.GetStat(StatType.CriticalDamage) / 100);
        }
        prefab.GetComponent<HitBox>().UsedStatValue = damage;
    }
}
