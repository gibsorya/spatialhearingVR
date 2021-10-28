using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonNumber : MonoBehaviour, BalloonController
{
    public int balloonNumber { get => getNumber(); set => setNumber(number); }
    [SerializeField]
    private int number;
    // Start is called before the first frame update
    private int getNumber()
    {
       return number = this.balloonNumber;
    }

    public int setNumber(int number)
    {
        return this.balloonNumber = number;
    }

    // Update is called once per frame
    
}
