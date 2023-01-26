using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitBox : MonoBehaviour
{
    public GameObject attacker;
    public float UsedStatValue { protected get; set; }

    protected abstract void Hit(Victim target);
}
