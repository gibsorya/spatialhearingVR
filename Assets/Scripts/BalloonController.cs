using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{

    
    public void OnHit()
    {
        Destroy(gameObject);
    }                
    public void OnTriggerEnter(Collider other)
    {
        if ((other.isTrigger == false))
        {
            other.isTrigger = true;
        }
        else
        {
            if(other.gameObject.tag == "projectile")
            {
                
                
                Destroy(gameObject);
            }
        }
    }
}
