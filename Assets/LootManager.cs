using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public GameObject lootBox;
    public Inventory playerInventory;
    public Transform[] spawnPoints;
    public List<GameObject> boxes;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBoxes();
        UpdateBoxes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBoxes()
    {
        string[] ammoPosibilities = new string[playerInventory.unlockedWeapons.Count];

        for (int i =0; i < ammoPosibilities.Length; i++)
        {
            ammoPosibilities[i] = playerInventory.unlockedWeapons[i];
        }

        for (int i = 0; i < boxes.Count; i++)
        {
            boxes[i].GetComponent<LootBox>().SetWeapon(ammoPosibilities[Random.Range(0,ammoPosibilities.Length)]);
        }


    }

    public void SpawnBoxes()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
           var box = Instantiate(lootBox, spawnPoints[i].transform.position,Quaternion.identity);

            if (!boxes.Contains(box))
            boxes.Add(box);
        }

    }

    public void SpawnFreshLoot()
    {
        var temp = boxes;

        for (int i = 0; i < temp.Count; i++)
        {
            Destroy(temp[i]);
        }

        boxes.Clear();

        SpawnBoxes();
        UpdateBoxes();
    }

    public void RemoveLoot(GameObject l)
    {
        if (boxes.Contains(l))
            boxes.Remove(l);
    }

}
