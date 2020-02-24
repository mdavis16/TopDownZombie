using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceZombie : MonoBehaviour
{
    public GameObject iceBall;
    public Transform firePoint;
    public Zombie zombie;


    public void Attack()
    {
       var i= Instantiate(iceBall, firePoint.transform.position,Quaternion.identity);
        i.transform.eulerAngles = transform.eulerAngles;
    }
}
