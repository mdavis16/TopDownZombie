using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float hp =100;
    private bool hurt;
    private Rigidbody2D rb;
    public GameObject freezeSfx;
    public GameObject GameOverMenu;
    bool canDie=true;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
     
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            if(canDie)
            {
                Die();
                canDie = false;
            }
        }
         
    }


    private void Die()
    {
        var  point = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,0));
        Instantiate(GameOverMenu, point, Quaternion.identity,GameObject.FindGameObjectWithTag("Canvas").transform);
        Time.timeScale = 0;
       

    }

    public float GetHp()
    {
        return hp;
    }

    public void SetHp( float newHp)
    {
        hp += newHp;


       
    }

    private IEnumerator ReactToHit(float time,Vector2 dir)
    {
        hurt = true;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerAttackHandler>().enabled = false;
        rb.velocity = Vector2.zero;
        rb.AddForce(-dir * 3, ForceMode2D.Impulse);
        yield return new WaitForSeconds(time);
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerAttackHandler>().enabled = true;
        hurt = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "AttackHitBox")
        {

            if(!hurt)
            StartCoroutine(ReactToHit(0.5f,col.gameObject.transform.position - transform.position));
            

            float dam = col.gameObject.GetComponent<ZombieAttack>().GetAttackDamage();
            hp -= dam;
            print("Hit" + " hp :" +hp);


            Vector2 hitDirection = col.transform.position - transform.position;
            float angle = Mathf.Atan2(hitDirection.y, hitDirection.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
        }



    }

    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "FireBall")
        {

            if (!hurt)
                StartCoroutine(ReactToHit(0.5f, col.gameObject.transform.position - transform.position));

            hp -= 10;

            Vector2 hitDirection = col.transform.position - transform.position;
            float angle = Mathf.Atan2(hitDirection.y, hitDirection.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
            Destroy(col.gameObject);

        }

        if (col.gameObject.tag == "IceBall")
        {
            if (!hurt)
                StartCoroutine(ReactToHit(0.5f, col.gameObject.transform.position - transform.position));

            hp -= 10;

            Vector2 hitDirection = col.transform.position - transform.position;
            float angle = Mathf.Atan2(hitDirection.y, hitDirection.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
            Destroy(col.gameObject);
            Instantiate(freezeSfx, transform.position, Quaternion.identity, transform);
            GetComponent<PlayerMovement>().StartCoroutine(GetComponent<PlayerMovement>().SlowDown(1f,10f));
        }
    }
  
}
