using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    public int numOfRoundsSurvived { get; set; }
    public int numOfZombiesKilled { get; set; }
    public int score{ get; set; }
    public int multi { get; set; } = 1;
    public int pointsPerBasicZombie = 200;
    public int pointsPerSpecialZombie = 2000;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiText;
    public Inventory inventory;
    public PickUpItemDisplayManager displayManager;
    private List<int> currentUpgrades = new List<int>();
    public Bullet[] bullets;

    public bool reduceMulti = false;

    public float timer = 2;

    // Start is called before the first frame update
    void Start()
    {
        ResetStats();


    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "" +score;
        multiText.text = "x" + multi;


        if (multi > 1)
            reduceMulti = true;

         if(multi == 1)
            reduceMulti = false;

        if (reduceMulti == true)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            multi--;
            timer = 2;
        }
    }

    public void AddPoints(string zombieType)
    {
        int points = 0;

        if (zombieType == "Basic")
            points += pointsPerBasicZombie;
        else
            points += pointsPerSpecialZombie;

        score += (points * multi);

        multi++;
        timer = 2;
        CheckForUpgrades();

        
   
    }


    private void ResetStats()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i].doubleDamage = false;
        }
       inventory.GetWeaponFromAllWepons("Uzi").rateOfFire = 0.2f;
       inventory.GetWeaponFromAllWepons("Uzi").maxAmmo = 100;
    }

    private void CheckForUpgrades()
    {
        switch (multi)
        {
            case 2:
                if (!currentUpgrades.Contains(2))
                {
                    var pis = inventory.GetWeaponFromInventory("Pistol").GetComponent<Weapon>().rateOfFire = 0.3f;
                    displayManager.AddMessage("UNLOCKED: PISTOL QUICKFIRE!",Color.blue);
                    currentUpgrades.Add(2);
                }
            break;

            case 5:
                if (!currentUpgrades.Contains(5))
                {
                    inventory.AddWeaponToPlayer("Uzi");
                    displayManager.AddMessage("UNLOCKED: UZI!", Color.blue);
                    currentUpgrades.Add(5);
                }
                break;
            case 8:
                if (!currentUpgrades.Contains(8))
                {
                    inventory.GetWeaponFromInventory("Pistol").GetBullet().GetComponent<Bullet>().doubleDamage = true;
                    displayManager.AddMessage("UNLOCKED: PISTOL X2 DAMAGE!", Color.blue);
                    currentUpgrades.Add(8);
                }
                break;
            case 10:
                if (!currentUpgrades.Contains(10))
                {
                    inventory.AddWeaponToPlayer("Shotgun");
                    displayManager.AddMessage("UNLOCKED: Shotgun", Color.blue);
                    currentUpgrades.Add(10);
                }
                break;
            case 13:
                if (!currentUpgrades.Contains(13))
                {
                    var uzi = inventory.GetWeaponFromAllWepons("Uzi").rateOfFire = 0.08f;
                    if (inventory.HasWeapon("Uzi"))
                    {
                        inventory.GetWeaponFromInventory("Uzi").rateOfFire = 0.08f;
                    }
                    displayManager.AddMessage("UNLOCKED: UZI QUICKFIRE!", Color.blue);
                    currentUpgrades.Add(13);
                }
                break;
            case 15:
                if (!currentUpgrades.Contains(15))
                {
                    inventory.AddWeaponToPlayer("Barrel");
                    displayManager.AddMessage("UNLOCKED: Barrel", Color.blue);
                    currentUpgrades.Add(15);
                }
                break;
            case 17:
                if (!currentUpgrades.Contains(17))
                {
                    var uzi = inventory.GetWeaponFromAllWepons("Uzi");
                    uzi.maxAmmo = uzi.getMaxAmmo() * 2;
                    displayManager.AddMessage("UNLOCKED: UZI DOUBLEAMMO", Color.blue);
                    currentUpgrades.Add(17);
                }
                break;
        }
    }
}
