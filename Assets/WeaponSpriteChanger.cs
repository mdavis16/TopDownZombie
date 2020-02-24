using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpriteChanger : MonoBehaviour
{
    public Sprite[] weaponSprites;
    private SpriteRenderer ren;
    
    // Start is called before the first frame update
    void Start()
    {
        ren = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeWeaponSprite(string spriteName) 
    {


        

        if (spriteName == "Pistol" || spriteName == "Shotgun") // This line is required because the idle sprite for the player has a pistol or shotgun the sprites are just added on top to reduce complexity and animations
        {
            ren.sprite = null;
            return;
        }

        for (int i = 0; i < weaponSprites.Length; i++)
        {
            if (weaponSprites[i].name == spriteName)
            {
                ren.sprite = weaponSprites[i];
                print(weaponSprites[i].name);
            }
        }
    }
}
