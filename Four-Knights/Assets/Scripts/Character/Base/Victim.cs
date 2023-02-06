using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victim : MonoBehaviour
{
    StatManager statManager;

    public int MaxHp
    {
        get { return (int)statManager.GetStat(StatType.Health); }
    }

    int hp;
    public int Hp
    {
        get { return hp; }
        protected set
        {
            hp = Mathf.Clamp(value, 0, MaxHp);
        }
    }

    void Start()
    {
        statManager = GetComponent<StatManager>();
        Hp = (int)statManager.GetStat(StatType.Health);
    }

    public void TakeDamage(int damage)
    {
        if (statManager.GetBuff(BuffType.Barrier))
        {
            if (damage > statManager.GetBuffValue(BuffType.Barrier))
            {
                damage -= statManager.GetBuffValue(BuffType.Barrier);
                statManager.ClearBuff(BuffType.Barrier);
            }
            else
            {
                statManager.SetBuffValue(BuffType.Barrier, statManager.GetBuffValue(BuffType.Barrier) - damage);
                if(statManager.GetBuffValue(BuffType.Barrier) <= 0)
                {
                    statManager.ClearBuff(BuffType.Barrier);
                }
                damage = 0;
            }
        }
        float damageDown = (statManager.GetStat(StatType.Defence) / (statManager.GetStat(StatType.Defence) + (float)GetComponent<StatManager>().Level * 5f + 500f));
        Hp -= (int)(damage * (1f - damageDown));
        if (Hp == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
