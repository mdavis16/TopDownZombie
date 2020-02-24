using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public float explosionRadius;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, 2, Vector2.up, 1);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null)
            {
                if (hit[i].collider.tag == "Player")
                {
                    GetComponent<CircleCollider2D>().isTrigger = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity); 
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
       
        if (col.gameObject.tag == "Bullet")
        {
            Destroy(col.gameObject);
            Explode();
        }

        if (col.gameObject.tag == "Explosion")
        {
            Explode();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GetComponent<CircleCollider2D>().isTrigger = false;

        }
    }
}
