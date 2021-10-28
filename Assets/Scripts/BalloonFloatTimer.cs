using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonFloatTimer : MonoBehaviour
{
    float currentTime = 0;
    float startingTime = 360;
    bool startTimer = false;

    public void timer()
    {
        startTimer = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;

        if(currentTime<=0 && startTimer)
        {
            FindObjectOfType<ArenaController>().balloonsToFloat.RemoveAt(FindObjectOfType<ArenaController>().randomNum);
            //balloonsToFloat.RemoveAt(randomNum);
        }
    }
}
