using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] float spawnCoolTime = 120;
    [SerializeField] GameObject monsterPrefab;

    GameObject monster;
    float timer;

    private void Update()
    {
        if (monsterPrefab == null)
        {
            timer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<InteractionManager>() == null) return;
        if(monster == null && timer <= 0)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, Vector3.down, out hit, 5);
            monster = Instantiate(monsterPrefab, hit.point, Quaternion.identity);
            timer = spawnCoolTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<InteractionManager>() == null) return;
        if(monster != null)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, Vector3.down, out hit, 5);
            monster.transform.position = hit.point;
        }
    }
}
