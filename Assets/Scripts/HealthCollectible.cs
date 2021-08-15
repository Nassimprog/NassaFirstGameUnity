using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    public int Heal;

    void OnCollisionStay2D(Collision2D other)
    {
        RubyController controller = other.gameObject.GetComponent<RubyController>();

        if (controller != null)
        {

            if (RubyController.HasHealthSatchel && controller.health == controller.maxHealth)
            {
                controller.AddExtraHealth(Heal);
                ExtraHealthDisplay.instance.SetExtraHealth(Heal);
                controller.PlaySound(collectedClip);
                Destroy(gameObject);
            }

            if (controller.health < controller.maxHealth)
            {
                controller.ChangeHealth(Heal);
                Destroy(gameObject);

                controller.PlaySound(collectedClip);
                
            }

            
        }
    }
}