using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    public Weapon equipedWeapon;
    public Inventory inventory;
    public Transform firePoint;
    private bool canShoot = true;
    public WeaponSpriteChanger spriteChanger;
    private Animator ani;
    private bool shootButton;

    public float aimSpeed;
    bool canRotate;
    public float scanRadius;
    public Transform sightPoint;

    GameObject z;

    private Rigidbody2D rb;
    [SerializeField]
    private List<GameObject> zombiesNearBy = new List<GameObject>();

    public GameObject barrel;
    public GameObject wall;
    public GameObject turret;
  
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        equipedWeapon = inventory.GetWeaponFromInventory("Pistol");
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        if (equipedWeapon.getCurrentAmmo() <= 0)
        {
            inventory.RemoveWeapon(equipedWeapon.GetWeaponName());
        }

        if (Input.GetKey(KeyCode.Space))
        {

                Shoot();
        }


        if (shootButton)
        {
            Shoot();
        }
        

        if (Input.GetKeyDown(KeyCode.P))
        {
            inventory.CycleWeapons(1);

        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            inventory.CycleWeapons(-1);

        }


    }

    public void Shoot()
    {
        if (canShoot)
        {
            if (equipedWeapon.getCurrentAmmo() > 0)
            {

                if (equipedWeapon.weaponName != "Barrel" && equipedWeapon.weaponName != "Turret" && equipedWeapon.weaponName != "Wall")
                {
                    var b = Instantiate(equipedWeapon.GetBullet(), firePoint.position, Quaternion.identity);


                    if (equipedWeapon.weaponName == "Shotgun")
                    {

                        for (int i = 0; i < b.transform.childCount; i++)
                        {
                            var bchild = b.transform.GetChild(i).GetComponent<Bullet>();
                            bchild.GetComponent<Bullet>().SetMaxRange(equipedWeapon.range);
                            bchild.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
                            bchild.GetComponent<Rigidbody2D>().gravityScale = Random.Range(-5, 5);
                        }

                    }

                    if (b.GetComponent<Bullet>()) // pistol, uzi
                    {
                        b.GetComponent<Bullet>().SetMaxRange(equipedWeapon.range);
                        b.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
                        StartCoroutine(ResetCanShoot(equipedWeapon.rateOfFire));
                    }

                    StartCoroutine(ResetCanShoot(equipedWeapon.rateOfFire));

                    if (equipedWeapon.GetWeaponName().Equals("Pistol") == false)
                        equipedWeapon.SetAmmo(equipedWeapon.getCurrentAmmo() - 1);
                }

                else {
                    TryToPlace();


                }
            }

            else ///... OUT OF AMMO 
            {
                print(equipedWeapon.getCurrentAmmo() + " " + equipedWeapon.GetWeaponName());
                inventory.RemoveWeapon(equipedWeapon.GetWeaponName());

                return;
            }
            
        }

    }

    private IEnumerator ResetCanShoot(float t)
    {
        canShoot = false;
        yield return new WaitForSeconds(t);
        canShoot = true;
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        equipedWeapon = newWeapon;
        ani.SetInteger("Idle", newWeapon.idleType);
        spriteChanger.ChangeWeaponSprite(newWeapon.weaponName);
     
    }

    public void EnableShootButton()
    {
        shootButton = true;
    }

    public void DisableShootButton()
    {
        shootButton = false;
    }


    /// <summary>
    /// ///////////////////////////////////////////////////////////////////////
    /// </summary>
    public void AimAtNearZombie()
    {
       ScanForZombies();

        if (zombiesNearBy.Count > 0)
        {
            if (z == null)
                ChangeTarget();


           
            var diff = z.transform.position - transform.position;
            var angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            var rot = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, aimSpeed);
           

        }
        
    }

    public void ChangeTarget()
    {
        if(zombiesNearBy.Count >0)
        z = FindClosestZombie();
    }

    public void ScanForZombies()
    {

        RaycastHit2D [] hit = Physics2D.CircleCastAll(sightPoint.position, scanRadius, Vector2.up,1);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null)
            {
                if (hit[i].collider.tag == "Zombie")
                {
                    if (!zombiesNearBy.Contains(hit[i].collider.gameObject))
                    {
                        zombiesNearBy.Add(hit[i].collider.gameObject);
                    }
                }
            }
        }


    }

 

    public GameObject FindClosestZombie()
    {
        GameObject closestZombie = null;

        if (zombiesNearBy.Count > 0)
        {
            closestZombie = zombiesNearBy[0];
            float minDis = Vector2.Distance(transform.position, zombiesNearBy[0].transform.position);

            for (int i = 0; i < zombiesNearBy.Count; i++)
            {

                float distance = Vector2.Distance(transform.position, zombiesNearBy[i].transform.position);

                if (distance < minDis)
                {

                    minDis = distance;
                    closestZombie = zombiesNearBy[i];
                  

                }


            }
        }
        return closestZombie;
    }

    public List<GameObject> GetNearByZombies()
    {
        return zombiesNearBy;
    }

    private void TryToPlace()
    {
        RaycastHit2D[] hit = Physics2D.CircleCastAll(firePoint.position, 1, Vector2.up, 1);
        bool canPlace = true;
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null)
            {
                if (hit[i].collider.tag == "Barrel")
                {
                    canPlace = false;
                }
            }
        }

        if (canPlace)
        {
            switch (equipedWeapon.weaponName)
            {
                case "Barrel":
                    Instantiate(barrel, firePoint.position, Quaternion.identity);
                    break;
            }
            equipedWeapon.SetAmmo(equipedWeapon.getCurrentAmmo() - 1);
            StartCoroutine(ResetCanShoot(0.5f));
        }
    }
    
}
