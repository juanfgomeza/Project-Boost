using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    Vector3 finalPosition;
    [SerializeField] Vector3 movementVector;
    //[SerializeField] [Range(0,1)] float movementFactor;
    float movementFactor;
    [SerializeField] float period = 2f;

    void Start()
    {
        startingPosition = transform.position;
        Debug.Log(startingPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return;}
        float cycles = Time.time / period;  // continually growing over time
        
        const float tau = Mathf.PI * 2; // constant value of 2*PI
        float rawSinWave = Mathf.Sin(cycles * tau);  // from  -1 to 1
        
        movementFactor = rawSinWave;

        Vector3 offset = movementVector * movementFactor;
        finalPosition = startingPosition + offset;
        transform.position = finalPosition;
        


    }
}
