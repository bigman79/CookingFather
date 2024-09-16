using System;
using System.Collections;

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private List<Vector3> _inlist;

    
    private Rigidbody _rb;

    [SerializeField] private float walkspeed;
    [SerializeField] private float runspeed;
    [SerializeField] private float lookspeed;
    
    private Vector3 _inputs;
    

    
    [SerializeField] private bool isRunning;
    [SerializeField] private bool isGrounded;

    void Start()
    {

        _rb = GetComponent<Rigidbody>();
        _inlist = new List<Vector3>();
    }

    void Update()
    {
        Listinnit();
    }
    private void Listinnit()
    {
        float xraw = Input.GetAxisRaw("Horizontal");
        float zraw = Input.GetAxisRaw("Vertical");
        _inlist.Add(new Vector3(xraw,0,zraw));
        _inlist.Capacity = 3;
        if (_inlist.Count == _inlist.Capacity)
        {
            _inlist.RemoveAt(0);
        }
    }

    void FixedUpdate()
    {
        InputManager();

    }

    private void InputManager()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        isRunning = Input.GetButton("Run");

        looks(x, z);
        if (isRunning && isGrounded)
        {
            Run(x,z);
        }
        else if (!isRunning && isGrounded)
        {
            Walk(x,z);
        }
    }

    private void looks(float x, float z)
    {
        Vector3 horizontalrot = transform.right * x;
        Vector3 verticalrot = transform.forward * Mathf.Pow(z,2);
        Vector3 rot = horizontalrot + verticalrot;
        if (rot != Vector3.zero)
        {
            transform.rotation =
                Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(rot), lookspeed * Time.fixedDeltaTime);
        }
        
    }

    private void Run(float x, float z)
    {

        float Runspeed(float z)
        {
            //turn it positive
            z = Mathf.Pow(z, 2) / z;
            
            float returnvalue = runspeed;
            //acceleration
            
            //maxspeed
            if (Mathf.Approximately(z, 1))
            {
                returnvalue = Mathf.Clamp(returnvalue, 0, runspeed);
            }

            //detects movement
            if(_inlist[1].x == 0)
                if (Mathf.Approximately(_inlist[0].z, 1))
                {
                    print("forward");
                }
            
            return returnvalue;
        }

        Vector3 rmove = transform.forward * (z * Runspeed(z));
        rmove *= Time.fixedDeltaTime;
        _rb.velocity += rmove;
    }

    private void Walk(float x, float z)
    {
        
        Vector3 move = (transform.right * x + transform.forward * z) * walkspeed;
        move *= Time.fixedDeltaTime;
        _rb.MovePosition(transform.position + move);
    }

    private void OnCollisionExit(Collision other)
    {
        isRunning = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        isGrounded = true;
    }
    
    
}