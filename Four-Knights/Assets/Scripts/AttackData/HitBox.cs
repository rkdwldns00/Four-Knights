using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitBox : MonoBehaviour
{
    public GameObject attacker;

    protected abstract void Hit(Victim target);
}
