using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitBox : MonoBehaviour
{
    public GameObject attacker { protected get; set; }
    public float UsedStatValue { protected get; set; }

    protected virtual void Hit(Victim target)
    {
        if(attacker == target.gameObject)
        {
            return;
        }
        target.TakeDamage((int)UsedStatValue);
        attacker.GetComponent<Attacker>().ReceiveVictimObject(target.gameObject);
    }
}
