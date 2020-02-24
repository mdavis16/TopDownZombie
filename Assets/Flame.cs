using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public float ammo;
    public float maxFlameTime;
    Inventory inv;
    PlayerAttackHandler pa;

    // Start is called before the first frame update
    void Start()
    {
        inv = transform.root.transform.root.GetComponent<Inventory>();
        pa = transform.root.transform.root.GetComponent<PlayerAttackHandler>();
      
    }

    // Update is called once per frame
    void Update()
    {

       

        if (ammo >= maxFlameTime)
            Kill();


        if (Input.GetKeyDown(KeyCode.Space) || pa.equipedWeapon.weaponName == "Flamerthrower")
        {
            ammo++;
        }

        if (Input.GetKeyUp(KeyCode.Space )|| pa.equipedWeapon.weaponName !="Flamerthrower" )
        {
            Kill();
        }

      
    }

    public void Kill()
    {
       

        Destroy(gameObject);
    }
}
