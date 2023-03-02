using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victim : MonoBehaviour
{
    public StatManager StatManager { get; protected set; }

    public int MaxHp
    {
        get { return (int)StatManager.GetStat(StatType.Health); }
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
        StatManager = GetComponent<StatManager>();
        Hp = (int)StatManager.GetStat(StatType.Health);
    }

    public void TakeDamage(int damage)
    {
        if (StatManager.GetBuff(BuffType.Barrier))
        {
            int barrier = (int)StatManager.GetBuffValue(BuffType.Barrier);
            if (damage > barrier)
            {
                damage -= barrier;
                StatManager.ClearBuff(BuffType.Barrier);
            }
            else
            {
                StatManager.SetBuffValue(BuffType.Barrier, barrier - damage);
                if(barrier <= 0)
                {
                    StatManager.ClearBuff(BuffType.Barrier);
                }
                damage = 0;
            }
        }
        //float damageDown = (statManager.GetStat(StatType.Defence) / (statManager.GetStat(StatType.Defence) + (float)GetComponent<StatManager>().Level * 5f + 500f));
        Hp -= (int)(damage);
        Debug.Log(damage);
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
