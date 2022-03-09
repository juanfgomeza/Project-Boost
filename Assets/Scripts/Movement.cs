using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    [SerializeField] float mainThrust = 1f;
    [SerializeField] float rotationThrust = 1f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProccessThrust();
        ProcessRotation();        
    }

    void ProccessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            //Debug.Log("Pressed SPACE - Thrusting");           
            rb.AddRelativeForce(Time.deltaTime * mainThrust * Vector3.up);
            
        } 
    }
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.D))
        {
            //Debug.Log("Pressed D - Move right");
            ApplyRotation(rotationThrust);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            //Debug.Log("Pressed A - Move left");
            ApplyRotation(-rotationThrust);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;   // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.back * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;  // unfreezing so that physics system can take over
    }
}
