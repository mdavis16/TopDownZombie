using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{

    private GameObject player;

    [SerializeField]
    private string WeaponName;
    private LootManager lootManager;
    PickUpItemDisplayManager displayManager;


    // Start is called before the first frame update
    void Start()
    {
        lootManager = GameObject.FindGameObjectWithTag("LootManager").GetComponent<LootManager>();
        displayManager = GameObject.FindGameObjectWithTag("DisplayManager").GetComponent<PickUpItemDisplayManager>();
        player = GameObject.FindGameObjectWithTag("Player");   
    }

   public void SetWeapon(string str)
    {
        WeaponName = str;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (!player.GetComponent<Inventory>().HasWeapon(WeaponName))
            {
                player.GetComponent<Inventory>().AddWeaponToPlayer(WeaponName);
            }

            else
            player.GetComponent<Inventory>().AddAmmoToPlayer(WeaponName);


            displayManager.AddMessage("Picked up" + " " + WeaponName,Color.green);
            lootManager.RemoveLoot(this.gameObject);
            Destroy(gameObject);
        }

    }
}
