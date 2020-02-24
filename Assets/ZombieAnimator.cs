using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimator : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator ani;
    private Zombie z;

    // Start is called before the first frame update
    void Start()
    {
        z = GetComponent<Zombie>();
    }

    // Update is called once per frame
    void Update()
    {
       
        ani.SetFloat("Speed", rb.velocity.magnitude);
    }

    public void Attack()
    {
        ani.SetTrigger("Attack");
        
    }
}
