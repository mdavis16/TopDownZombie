using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public float rateOfFire;
    public int maxAmmo;
    private int currentAmmo;
    public string ammoType;
    public float range;
    public GameObject bullet;
    public int idleType; // 1 for hand guns // 2 for rifles && etc.. 

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        print("ff");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetBullet()
    {
        return bullet;
    }
    public int getMaxAmmo()
    {
        return maxAmmo;
    }

    public int getCurrentAmmo()
    {
        return currentAmmo;
    }

    public void SetAmmo(int num)
    {
        currentAmmo = num;
    }

    public string GetWeaponName()
    {
        return weaponName;
    }
}
