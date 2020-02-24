using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour /// THIS SCRIPT IS EXECUTED BEFORE THE PlayerAttackHander SCRIPT IS 
{
    public Weapon[] allWeapons;
    public List<Weapon> playerWeapons;
    public List<string> unlockedWeapons;
    public int index;
    public PlayerAttackHandler attackHandler;
    // Start is called before the first frame update
    void Start()
    {
        AddWeaponToPlayer("Pistol");
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddWeaponToPlayer(string name)
    {
     
        for (int i = 0; i < allWeapons.Length; i++)
        {
            if (allWeapons[i].weaponName == name)
            {
                allWeapons[i].SetAmmo(allWeapons[i].getMaxAmmo());
                var g = Instantiate(allWeapons[i], transform.position, Quaternion.identity, transform);
                playerWeapons.Add(g);
                unlockedWeapons.Add(name);
            }
        }
    }

    public void AddAmmoToPlayer(string str)
    {

        for (int i = 0; i < playerWeapons.Count; i++)
        {
            if (playerWeapons[i].GetWeaponName().Equals(str))
            {
                playerWeapons[i].SetAmmo( playerWeapons[i].getMaxAmmo());                                           
            }
        }
    }

    public Weapon GetWeaponFromInventory(string name) 
    {
       
            for (int i = 0; i < playerWeapons.Count; i++)
            {
                if (playerWeapons[i].weaponName == name)
                {
                return playerWeapons[i];
                }
            }
        print("NO SUCH WEAPON EXSISTS IN PLAYERS INVENTORY");
        return null;
    }

    public Weapon GetWeaponFromAllWepons(string name)
    {

        for (int i = 0; i < allWeapons.Length; i++)
        {
            if (allWeapons[i].weaponName == name)
            {
                return allWeapons[i];
            }
        }
        print("NO SUCH WEAPON EXSISTS IN PLAYERS INVENTORY");
        return null;
    }

    public void RemoveWeapon(string name)
    {
        for (int i = 0; i < playerWeapons.Count; i++)
        {
            if (playerWeapons[i].weaponName == name)
            {
                playerWeapons.Remove(playerWeapons[i]);

                if (index > 0)
                {
                    index--;
                }

                else
                    index = 0;
             
                attackHandler.ChangeWeapon(playerWeapons[index]);
                
            }
        }
    }
    public Weapon GetEquipedWeapon()
    {
        return (playerWeapons[index]);
    }

    public void CycleWeapons(int num)
    {
      
        if (num == 1)
        {
            if ((index + 1) < playerWeapons.Count)
            {
                index++;
                attackHandler.ChangeWeapon(playerWeapons[index]);
            }
            else         
                print("INVENTORY INDEX: " + index + " CEASES TO EXIST");           
        }

        if (num == -1)
        {
            if ((index - 1) >= 0)
            {
                index--;
                attackHandler.ChangeWeapon(playerWeapons[index]);
            }
            else
                print("INVENTORY INDEX: " + index + " CEASES TO EXIST");

        }

    }


    public bool HasWeapon(string w)
    {
        for (int i = 0; i < playerWeapons.Count; i++)
        {
            if (playerWeapons[i].weaponName == w)
            {
                return true;

            }
        }

        return false;
    }


    public void SimpleChangeWeaponButton()
    {

        if ((index + 1) < playerWeapons.Count)
        {
            index++;
            attackHandler.ChangeWeapon(playerWeapons[index]);
        }
        else
        {
            index = 0;
            attackHandler.ChangeWeapon(playerWeapons[index]);

        }
    }   
        
}
