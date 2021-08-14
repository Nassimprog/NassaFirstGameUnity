using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{


    public GameObject item;
    public int number;
    public bool isTrigger;
    public bool NeedCrowbar;
    public ParticleSystem DestroyEffect;
    private Rigidbody2D rigidbody2d;
    


    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Explode(Vector2 position)
    {
        Instantiate(DestroyEffect, position, Quaternion.identity);
    }

    // Update is called once per frame
    public void Break()
    {
        if (item != null)
        {
            for (int i = 0; i < number; i++)
            {
                //create object
                Instantiate(item, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
                
            }
        }



        Instantiate(DestroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);


    }

   

}
