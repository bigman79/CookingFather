using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    private float _walkspeed = 7f;
    
    private Vector3 _inputs;
    
    private List<Vector3> Inputs;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Inputs = new List<Vector3>();

    }

    void Update()
    {
        Inputs.Add(_inputs);
        
    }
    
    void FixedUpdate()
    {
        listinnit();
        Walk();
    }

    private void listinnit()
    {
        float xraw = Input.GetAxisRaw("Horizontal");
        float zraw = Input.GetAxisRaw("Vertical");
        
        _inputs = new Vector3(xraw, 0, zraw);
        if(Inputs.Count == 10)
        {
            Vector3 first = Inputs[Inputs.Count];
            Vector3 last = Inputs[Inputs.Count - 1];
            if (first == -last)
            {
                //turned 180
                print("turned 180");
            }
            if(last == Vector3.zero && first != Vector3.zero)
            {
               //moved
               print("moved");
            }
        }
        
        // Remove the first element in the list
        if (Inputs.Count > 10)
        {
            Inputs.RemoveAt(0);
        }
        
    }

    private void Walk()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = (transform.right * x + transform.forward * z) * _walkspeed;
        move *= Time.fixedDeltaTime;
        rb.MovePosition(transform.position + move);
    }
}
