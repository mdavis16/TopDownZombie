using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = GetWeaponData();
    }

    private string GetWeaponData()
    {
       var w= inventory.GetEquipedWeapon();

        if (w.GetWeaponName() == "Pistol")
            return w.GetWeaponName() + " ";

        else
            return w.GetWeaponName() + " " + w.getCurrentAmmo();
    }
}
