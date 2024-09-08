using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    
    
    private Rigidbody rb;
    
    private float speed = 10f; 
    private float stopfactor = 0.99f;
    private float increasefactor = 1f;
    private bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        walk();
        run();
        jump();
        crouch();
        
    }
    
    private void crouch()
    {
        
    }

    private void jump()
    {
        
    }

    private void run()
    {
        //change look speed depending on running
        
    }
    
    private void walk()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float rawhorizontal = Input.GetAxisRaw("Horizontal");
        float rawvertical = Input.GetAxisRaw("Vertical");
        
        Vector3 horizontalMovement = transform.right * horizontal;
        Vector3 verticalMovement = transform.forward * vertical;
        Vector3 movement = horizontalMovement + verticalMovement;
        Vector3 _velocity = (horizontalMovement + verticalMovement).normalized * (speed * increasefactor);
        Vector3 _stop = new Vector3(rb.velocity.x * stopfactor, rb.velocity.y, rb.velocity.z * stopfactor);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
        }

        //Acceleration
        increasefactor = Mathf.SmoothDamp(5,1,ref increasefactor,0.1f);
        rb.velocity += _velocity * Time.fixedDeltaTime;
        
        //checks if player stopped moving
        if(rawvertical == 0 && rawhorizontal == 0)
        {
            if (isGrounded == true)
            {
                rb.velocity = _stop * Time.fixedDeltaTime;
            }
        }
        
        
        //speed cap
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * .99f;
        }
        
    }

    private void OnCollisionExit(Collision other)
    {
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        isGrounded = true;
    }
}
