using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float hp;
    private GameObject player;
    private ZombieAnimator ani;
    public float attackDistance;
    Rigidbody2D rb;
    bool hurt;

    private Gore gore;
    public float impactForce;

    enum EnemyState { IDLE, MOVETOPLAYER, ATTACK, TAKEDAMAGE }

    public bool canAttack;
    public float minAttackTime;
    public float maxAttackTime;

    public float damageTime = 0.5f;
    public bool ReactToDamage = true;
    public bool shouldDamageStopAttack = true;

    private EnemyAI enemyAI;

    bool canFace;

    [SerializeField]
    float disFromPlayer;

    public float maxDisFromPlayer;

    [SerializeField]
    EnemyState currentState;

    public Color bloodColor = Color.white;

    ZombieSpawner spawner;

    public string zombieType;

    private Stats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("StatsCollection").GetComponent<Stats>();
        gore = GameObject.Find("GoreManager").GetComponent<Gore>();
        spawner = GameObject.FindGameObjectWithTag("ZombieSpawner").GetComponent<ZombieSpawner>();
        canFace = true;
        enemyAI = GetComponent<EnemyAI>();
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<ZombieAnimator>();
        currentState = EnemyState.MOVETOPLAYER;
    }

    // Update is called once per frame
    void Update()
    {
        HandleState();
        GetComponent<Animator>().SetBool("Hurt", hurt);

        disFromPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (disFromPlayer >= maxDisFromPlayer)
        {
            var z = player.GetComponent<PlayerAttackHandler>().GetNearByZombies();

            if (z.Contains(this.gameObject))
            {
                z.Remove(this.gameObject);
                player.GetComponent<PlayerAttackHandler>().ChangeTarget();
            }
        }

    }


    private void MoveForward()
    {
        if (currentState != EnemyState.ATTACK)
        {

            if (rb.velocity.magnitude > 1)
            {
                FaceVelocity();
            }
        }

        enemyAI.enabled = true;

        if (IsPlayerInAttackRange())
        {
            enemyAI.enabled = false;
            currentState = EnemyState.ATTACK;

        }
    }



    void FaceVelocity()
    {
        var angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;

        transform.localEulerAngles = new Vector3(0, 0, angle);

    }

    void FacePlayer()
    {
        var diff = player.transform.position - transform.position;
        var angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.localEulerAngles = new Vector3(0, 0, angle);
    }


    private void Attack()
    {
        rb.velocity = Vector2.zero;

        if (canAttack)
        {
            StartCoroutine(ResetAttack(Random.Range(minAttackTime, maxAttackTime)));

            switch (zombieType)
            {

                case ("Ice"):
                    GetComponent<IceZombie>().Attack();
                    break;

                case ("Devil"):
                    GetComponent<FireZombie>().Attack();
                    break;

                case ("Mutant"):
                    
                    break;

            }

         
            ani.Attack();
        }
    }


    private void HandleState()
    {
        switch (currentState)
        {

            case (EnemyState.MOVETOPLAYER):
                MoveForward();
                break;

            case (EnemyState.ATTACK):
                Attack();
                break;

            case (EnemyState.TAKEDAMAGE):

                break;

        }
    }


    public IEnumerator ResetAttack(float time)
    {

        FacePlayer();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        canAttack = false;
        yield return new WaitForSeconds(time);
        rb.constraints = RigidbodyConstraints2D.None;
        canAttack = true;

        if (!IsPlayerInAttackRange())
        {
            currentState = EnemyState.MOVETOPLAYER;
        }

    }

    private IEnumerator TakeDamage(float time, Vector2 dir)
    {
        enemyAI.enabled = false;
        currentState = EnemyState.TAKEDAMAGE;
        hurt = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(-dir * impactForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(time);
        enemyAI.enabled = true;
        hurt = false;
        rb.velocity = Vector2.zero;
        FacePlayer();
        currentState = EnemyState.MOVETOPLAYER;
    }

    private bool IsPlayerInAttackRange()
    {
        var disFromPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (disFromPlayer <= attackDistance)
            return true;


        return false;
    }



    public void Shock()
    {
        rb.velocity = Vector2.zero;

         if (ReactToDamage)
                {

                    if (shouldDamageStopAttack)
                    {
                        StartCoroutine(TakeDamage(3, transform.right));
                    }

                    else
                    {
                        if (currentState != EnemyState.ATTACK)
                        {
                            StartCoroutine(TakeDamage(3, transform.right));
                        }

                        else
                        {
                            print("Attacking...");
                        }
                    }


                }

                hp -= 50;

                if (hp <= 0)
                {
                    Die();
                }
            }

    public void Die()
    {

        ///REMOVE ZOMBIE FROM LISTS
        ///

        var z = player.GetComponent<PlayerAttackHandler>().GetNearByZombies();

        if (z.Contains(this.gameObject))
        {
            z.Remove(this.gameObject);
            player.GetComponent<PlayerAttackHandler>().ChangeTarget();
        }

        spawner.RemoveZombie(this.gameObject);
        stats.numOfZombiesKilled++;
        stats.AddPoints(zombieType);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            gore.SpawnBloodSplatter(transform.position, -col.transform.eulerAngles.z, bloodColor);

            if (ReactToDamage)
            {

                if (shouldDamageStopAttack)
                {
                    StartCoroutine(TakeDamage(damageTime, transform.right));
                }

                else
                {
                    if (currentState != EnemyState.ATTACK)
                    {
                        StartCoroutine(TakeDamage(damageTime, transform.right));
                    }

                    else
                    {
                        print("Attacking...");
                    }
                }


            }

            float dam = col.gameObject.GetComponent<Bullet>().getDamage();
            hp -= dam;

            if (hp <= 0)
            {
                Die();
            }

        }

        if (col.gameObject.tag == "Explosion")
        {
            StartCoroutine(TakeDamage(damageTime, transform.right));

            hp -= 100;
            gore.SpawnBloodSplatter(transform.position, -col.transform.eulerAngles.z, bloodColor);

            if (hp <= 0)
            {
                Die();
            }
        }

    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "FireBall")
        {
            if (zombieType != "Devil")
            {
              
                StartCoroutine(TakeDamage(damageTime, transform.right));

                hp -= 50;

                if (hp <= 0)
                {
                    Die();
                }
            }
        }


        if (col.gameObject.tag == "IceBall")
        {
            if (zombieType != "Ice")
            {
                

                StartCoroutine(TakeDamage(damageTime, transform.right*6));

                hp -= 50;

                if (hp <= 0)
                {
                    Die();
                }
            }
        }




    }
}
