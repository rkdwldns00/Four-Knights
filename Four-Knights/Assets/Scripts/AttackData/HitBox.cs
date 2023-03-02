using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitBox : MonoBehaviour
{
    [SerializeField] float ratio = 100;
    [SerializeField] float ignoreDefenceRatio = 0f;

    public Attacker attacker { protected get; set; }
    public float UsedStatValue { protected get; set; }

    protected virtual void Hit(Victim target)
    {
        if (target.GetComponent<Attacker>() != null && attacker == target.GetComponent<Attacker>())
        {
            return;
        }
        target.TakeDamage((int)(AddCriticalDamage(attacker.StatManager,(int)UsedStatValue) * ratio / 100f * DefenceToRatio((int)target.StatManager.GetStat(StatType.Defence))));
        attacker.ReceiveVictimObject(target.gameObject);
    }

    protected virtual int AddCriticalDamage(StatManager statManager, int damage)
    {
        if (Random.value < (statManager.GetStat(StatType.CriticalPercent) / 100f))
        {
            damage = (int)(damage * (1f + statManager.GetStat(StatType.CriticalDamage) / 100));
        }
        return damage;
    }

    protected virtual float DefenceToRatio(int defence)
    {
        defence = (int)(defence * (1f - ignoreDefenceRatio / 100));
        float damageDown = (defence / (defence + (float)attacker.StatManager.Level * 5f + 500f));
        return 1f - damageDown;
    }
}
