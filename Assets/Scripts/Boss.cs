using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed;
    public int health = 1;
    public float changeTime = 3.0f;
    public bool Vertical = false;
    public int damage = 3;
    public float timeInvincible = 0.0f;
    bool flip = false;
    bool isInvincible;
    float invincibleTimer;
    public ParticleSystem smokeEffect;

    new Rigidbody2D rigidbody2D; // new added to remove warning
    float timer;
    int direction = 1;
    bool broken = true;
    public Transform target;
    Animator animator;

    Rigidbody2D rigidbody2d;
    Vector2 lookDirection = new Vector2(1, 0);
    public GameObject ProjectilePrefab;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();

    }




    void Update()
    {
        //remember ! inverse the test, so if broken is true !broken will be false and return won’t be executed.
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (health >= 7)
            {
                direction = -direction;
                timer = changeTime;
            }

            if (health < 7)
            {
                speed = 12;
                damage = 4;
                changeTime = 1;
                if (flip)
                {
                    direction = -direction;
                    timer = changeTime;
                    Vertical = !Vertical;
                    flip = false;
                }
                else
                {

                    timer = changeTime;
                    Vertical = !Vertical;
                    flip = true;
                }
            }
            if (health < 4)
            {
                damage = 2;
                speed = 4;
            }
                

        }

            

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            isInvincible = false;
        }

        
    }
        

    void FixedUpdate()
    {
        //remember ! inverse the test, so if broken is true !broken will be false and return won’t be executed.
        if (!broken)
        {
            return;
        }

        Vector2 position = rigidbody2D.position;

        if (health > 3)
        {
            if (Vertical)
            {
                position.y = position.y + Time.deltaTime * speed * direction;
                animator.SetFloat("Move X", 0);
                animator.SetFloat("Move Y", direction);

            }


            else
            {
                position.x = position.x + Time.deltaTime * speed * direction;
                animator.SetFloat("Move X", direction);
                animator.SetFloat("Move Y", 0);


            }


        }

        else
        {
            damage = 2;
            speed = 4;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            if (transform.position.x < target.position.x)
            {
                animator.SetFloat("Move Y", 0);
                animator.SetFloat("Move X", 1);
            }

            if (transform.position.x > target.position.x)
            {
                animator.SetFloat("Move Y", 0);
                animator.SetFloat("Move X", -1);
            }


            if (transform.position.y < target.position.y)
            {
                animator.SetFloat("Move X", 0);
                animator.SetFloat("Move Y", 1);
            }

            if (transform.position.y > target.position.y)
            {
                animator.SetFloat("Move X", 0);
                animator.SetFloat("Move Y", -1);
            }

            

        }
        rigidbody2D.MovePosition(position);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-damage);
        }
    }


    public void ChangeHealth(int amount)
    {

        if (amount < 0)
        {
            if (isInvincible)
                return;

            health += amount;





            isInvincible = true;
            invincibleTimer = timeInvincible;
        }


    }

    //Public because we want to call it from elsewhere like the projectile script
    public void Fix()
    {

        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        RubyController.FixedRobots += 1;
        FixedCounter.instance.SetFixedRobots(RubyController.FixedRobots);
        smokeEffect.Stop();


    }

   // void Launch()
   // {
   //         GameObject projectileObject = Instantiate(ProjectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

   //         Projectile projectile = projectileObject.GetComponent<Projectile>();
   //         projectile.Launch(lookDirection, 300);
   // }

}
