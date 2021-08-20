using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Paddle")
        {
            this.ApplyEffect();
        }

        if(other.tag == "Paddle" || other.tag == "Finish")
        {
            Destroy(this.gameObject);
        }
    }

    protected abstract void ApplyEffect();
}
