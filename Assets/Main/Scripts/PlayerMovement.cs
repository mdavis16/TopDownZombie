using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerMovement : MonoBehaviour
{
    private Animator ani;
    public Animator feetAni;
    

    public Joystick joystick;

    public float moveSpeed = 20f;
    [SerializeField]
    float horizontalMove = 0f;
    float verticalMove = 0f;

    public float moveLimiter = 0.7f;
    private Rigidbody2D rb;

    private float angle;
    bool slowDown;

    private PlayerAttackHandler attack;

    public bool autoAim = false;

    bool moving = false;

    bool touchAim = false;
 
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attack = GetComponent<PlayerAttackHandler>();

    }

    // Update is called once per frame
    void Update()
    {


        horizontalMove = joystick.Horizontal * moveSpeed;
       verticalMove = joystick.Vertical * moveSpeed;

        ani.SetFloat("Speed", rb.velocity.magnitude);
        feetAni.SetFloat("Speed", rb.velocity.magnitude);


        if (horizontalMove != 0 && verticalMove != 0) // Check for diagonal movement
        {
            moving = true;

        }
        else
        {

            moving = false;
        }
        
    }



    private void FixedUpdate()
    {
  
            if (moving)
            {


                rb.velocity = new Vector2(horizontalMove * moveSpeed, verticalMove * moveSpeed) * Time.deltaTime;

                if (autoAim)
                {
                    if (attack.GetNearByZombies().Count >= 1)
                        attack.AimAtNearZombie();
                    else
                    {
                        if (touchAim)
                        {

                        }

                        else
                        {
                            angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                            transform.eulerAngles = new Vector3(0, 0, angle);
                        }
                    }
                }
                else
                {
                    angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                    transform.eulerAngles = new Vector3(0, 0, angle);
                }

            }
            else
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0;

                if (autoAim)
                {
                    attack.AimAtNearZombie();
                }
            }
        
    }
    



  public IEnumerator SlowDown(float time,float slowDownRate)
    {
        if (!slowDown)
        {
            slowDown = true;
            moveSpeed -= slowDownRate;
            yield return new WaitForSeconds(time);
            moveSpeed += slowDownRate;
            slowDown = false;
        }
    }
}
