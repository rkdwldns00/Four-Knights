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
            foreach (KeyValuePair<Item, float> pair in dropTag.items)
            {
                sum += pair.Value;
            }
            float random = Random.Range(0, sum);
            sum = 0;
            foreach (KeyValuePair<Item, float> pair in dropTag.items)
            {
                sum += pair.Value;
                if (random < sum)
                {
                    GameObject prefab = Instantiate(GameManager.DropedItemPrefab);
                    prefab.GetComponent<DropedItem>().item = pair.Key;
                    break;
                }
            }
        }
    }
}
