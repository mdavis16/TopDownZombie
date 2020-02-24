using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 10f;
    private Vector2 orginPos;
    private float maxRange =20;
    public float damage;
    public bool electric = false;
    public GameObject shockEffect;
    public bool doubleDamage = false;

    // Start is called before the first frame update
    void Start()
    {

        
        if (doubleDamage)
            damage = damage*2;
        
    
        orginPos = transform.position;
        rb = GetComponent<Rigidbody2D>();


        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);



    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(orginPos,transform.position)>=maxRange)
        {

            Destroy(gameObject);
        }
    }

    public void SetMaxRange(float range)
    {
        maxRange = range;
    }

    public float getDamage()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {

            if (electric)
            {
                RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, 5, Vector2.up, 1);

                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].collider != null)
                    {
                        if (hit[i].collider.tag == "Zombie")
                        {
                            hit[i].collider.gameObject.GetComponent<Zombie>().Shock();
                            Instantiate(shockEffect, hit[i].transform.position, Quaternion.identity, hit[i].transform);
                        }
                    }
                }
            }
            Destroy(gameObject);

        }

        
    }
}
