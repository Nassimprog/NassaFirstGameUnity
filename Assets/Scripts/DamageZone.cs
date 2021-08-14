using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damage = 3;

  
    void OnTriggerStay2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            
            controller.ChangeHealth(-damage);
            

                     
        }
    }

    private void WaitForSecondsRealtime(int v)
    {
        throw new NotImplementedException();
    }
}