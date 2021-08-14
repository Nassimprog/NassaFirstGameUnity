using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public ParticleSystem DestroyEffect;
    float timer;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
        
    }

    void Update()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Explosion()
    {
        Instantiate(DestroyEffect, rigidbody2d.position, Quaternion.identity);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController e = other.collider.GetComponent<EnemyController>();
        NonPlayerCharacter n = other.collider.GetComponent<NonPlayerCharacter>();
        Box b = other.collider.GetComponent<Box>();

        if (e != null) //if cog hits enemy
        {
            e.ChangeHealth(-1);
            if (e.health == 0)
                e.Fix();
            
            
        }

        if (n != null)
        {
            if (n.NPCType == 6)
            {
                n.DisplayDialog();
                RubyController.ammo += 1;
                AmmoDisplay.instance.SetAmmo(RubyController.ammo);
            }
        }
        
        if (b != null && b.NeedCrowbar == false) // if cog hits box also if it needs a crowbar then it can only be opened with crowbar
        {
            b.Break();
            
        }
        //Explosion();
        Destroy(gameObject);
    }
}