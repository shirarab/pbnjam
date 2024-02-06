using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableToast : MonoBehaviour
{
    // disable object when colided with ball
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ball"))
        {
            gameObject.SetActive(false);
        }
    }
}
