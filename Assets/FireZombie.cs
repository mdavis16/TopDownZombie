using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireZombie : MonoBehaviour
{
    public GameObject fireBall;
    public Transform firePoint;
    public Zombie zombie;


    public void Attack()
    {
       var f = Instantiate(fireBall, firePoint.transform.position, Quaternion.identity);
        f.transform.eulerAngles = transform.eulerAngles;
    }
}
