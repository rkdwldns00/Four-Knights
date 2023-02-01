using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : Interactable
{
    [SerializeField] DropTable dropTable;

    public override void Interaction(GameObject eventPlayer)
    {
        foreach (DropTagData dropTag in dropTable.dropTags)
        {
            float percentValue = dropTag.dropPercent;
            while (percentValue > 0)
            {
                if (percentValue > 100)
                {
                    percentValue -= 100;
                }
                else if (Random.Range(0, 100) < percentValue)
                {
                    percentValue = 0;
                }
                else
                {
                    break;
                }

                float sum = 0;
                foreach (DropItemWitPercent drop in dropTag.items)
                {
                    sum += drop.percent;
                }
                float random = Random.Range(0, sum);
                sum = 0;
                foreach (DropItemWitPercent drop in dropTag.items)
                {
                    sum += drop.percent;
                    if (random <= sum)
                    {
                        GameObject prefab = Instantiate(GameManager.DropedItemPrefab, transform.position, Quaternion.identity);
                        prefab.GetComponent<DropedItem>().item = drop.item;
                        break;
                    }
                }
            }
        }
        Destroy(gameObject);
    }
}
