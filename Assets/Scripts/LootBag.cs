using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public int LootNum;
    public GameObject droppedItemPrefab;
    public List<Loot> lootList = new List<Loot>();

    /*    Loot GetDroppedItem()
        {
            int randomNumber = Random.Range(100,101);
            List<Loot> possibleItem = new List<Loot>();
            foreach (Loot item in lootList)
            {
                if(randomNumber<= item.dropChance)
                {
                    possibleItem.Add(item);
                }
            }
            if(possibleItem.Count > 0)
            {
                Loot droppedItem = possibleItem[Random.Range(0, possibleItem.Count)];
                return droppedItem;
            }
            return null;
        }*/
    List<Loot> GetDroppedItems(int valueFromChest)
    {
        List<Loot> droppedItems = new List<Loot>();

        for (int i = 0; i < valueFromChest; i++)
        {
            int randomNumber = Random.Range(0, 101); // Thay đổi khoảng giá trị ngẫu nhiên theo yêu cầu của bạn

            List<Loot> possibleItem = new List<Loot>();
            foreach (Loot item in lootList)
            {
                if (randomNumber <= item.dropChance)
                {
                    possibleItem.Add(item);
                }
            }

            if (possibleItem.Count > 0)
            {
                Loot droppedItem = possibleItem[Random.Range(0, possibleItem.Count)];
                droppedItems.Add(droppedItem);
            }
        }

        return droppedItems;
    }


    public void InstantiateLoot(Vector3 spawnPosition)
    {

        List<Loot> droppedItems = GetDroppedItems(LootNum);
        foreach (Loot droppedItem in droppedItems)
        {
/*            if (droppedItem != null)
            {
                GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
                lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;

                float dropForce = 2f;
                Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
            }*/
            if (droppedItem != null)
            {
                // Tạo vị trí rơi ngẫu nhiên xung quanh vị trí spawnPosition
                Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                Vector3 lootPosition = spawnPosition + randomOffset;

                GameObject lootGameObject = Instantiate(droppedItemPrefab, lootPosition, Quaternion.identity);
                lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;

                float dropForce = 2f;
                Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
            }

        }
    }
}


