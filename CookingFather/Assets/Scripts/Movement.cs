using System;
using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Rigidbody rb;
    private float _walkspeed = 7f;
    
    private List<Vector3> _inlist;

    
    private Rigidbody _rb;

    [SerializeField] private float walkspeed;
    [SerializeField] private float runspeed;
    
    private Vector3 _inputs;
    

    
    [SerializeField] private bool isRunning;
    [SerializeField] private bool isGrounded;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
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
        if (isRunning && isGrounded)
        {
            Run(x,z);
        }
        else if (!isRunning && isGrounded)
        {
            Walk(x,z);
        }
    }

    private void Run(float x, float z)
    {
        Vector3 input = new Vector3(x, 0, z);

        float Runspeed(float horizontal)
        {
            return 0;
        }

        Vector3 rmove = transform.forward * Runspeed(x);
        rmove *= Time.fixedDeltaTime;
        _rb.MovePosition(transform.position + rmove);
        Quaternion rotation = Quaternion.LookRotation(input.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime * 10);
        
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