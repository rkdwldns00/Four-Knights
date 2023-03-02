using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    public StatManager StatManager { get; protected set; }
    AnimatorManager animator;

    float basicCool;
    float skillCool;
    float ultimateSkillCool;

    int attackIndex;

    protected virtual void Start()
    {
        StatManager = GetComponent<StatManager>();
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
          /*  if (animator.IsIdle)
            {
                attackIndex = 0;
            }*/
            basicCool = StatManager.BasicAttacks[attackIndex].CoolTime;
            InstantiateAttack(StatManager.BasicAttacks[attackIndex]);
            animator.UseAttack(attackIndex, StatManager.BasicAttacks[attackIndex].StunTime);
            if (attackIndex == StatManager.BasicAttacks.Length - 1)
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
            skillCool = StatManager.Skill.CoolTime;
            Instantiate(StatManager.Skill);
            animator.UseSkill(StatManager.Skill.StunTime);
        }
    }

    public void UseUltimateSkill()
    {
        if (ultimateSkillCool < 0)
        {
            ultimateSkillCool = StatManager.UltimateSkill.CoolTime;
            Instantiate(StatManager.UltimateSkill);
            animator.UseUltimateSkil(StatManager.UltimateSkill.StunTime);
        }
    }

    public virtual void ReceiveVictimObject(GameObject victim)
    {

    }

    void InstantiateAttack(AttackData data)
    {
        GameObject prefab = Instantiate(data.Prefab,transform.position,transform.rotation);
        prefab.GetComponent<HitBox>().attacker = this;
        float damage = StatManager.GetStat(data.UsedStatType);
        prefab.GetComponent<HitBox>().UsedStatValue = damage;
    }
}
