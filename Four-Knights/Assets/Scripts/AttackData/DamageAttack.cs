using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAttack : HitBox
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Victim>() != null)
        {
            Hit(other.GetComponent<Victim>());
        }
    }
}
