using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed;
    public int health = 1;
    public float changeTime = 3.0f;
    public bool Vertical = false;
    public int damage = 1;
    public float timeInvincible = 0.0f;
    bool flip = false;
    bool isInvincible;
    float invincibleTimer;
    public ParticleSystem smokeEffect;

    new Rigidbody2D rigidbody2D; // new added to remove warning
    float timer;
    int horizontal = 1;
    bool broken = true;

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
            if(flip)
            {
                horizontal = -horizontal;
                timer = changeTime;
                Vertical = !Vertical;
                flip = false;
            }
            else
            {
                
                timer = changeTime;
                Vertical = !Vertical;
                //Launch();
                flip = true;
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

        
        if(Vertical)
        {
            position.y = position.y + Time.deltaTime * speed * horizontal;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", horizontal);
            
        }
        
        
        else
        {
            position.x = position.x + Time.deltaTime * speed * horizontal;
            animator.SetFloat("Move X", horizontal);
            animator.SetFloat("Move Y", 0);
            
            
        }

        rigidbody2D.MovePosition(position);



    }

    void OnCollisionStay2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-3);
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
