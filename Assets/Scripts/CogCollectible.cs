using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    public int ammunition;

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            controller.AddAmmo(ammunition);
            AmmoDisplay.instance.SetAmmo(ammunition);
            Destroy(gameObject);
            controller.PlaySound(collectedClip);
        }

    }
}
