using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAttack : HitBox
{
    [SerializeField] float destroyTimer = 1;

    private void Start()
    {
        StartCoroutine(DestroyCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Victim>() != null)
        {
            Hit(other.GetComponent<Victim>());
        }
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(destroyTimer);
        Destroy(gameObject);
    }
}
