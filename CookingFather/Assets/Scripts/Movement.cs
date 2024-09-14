
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
<<<<<<< Updated upstream
=======
    private List<Vector3> _inlist;
    
    private Rigidbody _rb;

    [SerializeField] private float walkspeed;
    [SerializeField] private float runspeed;
    
    private Vector3 _inputs;
    
>>>>>>> Stashed changes
    
    [SerializeField] private bool isRunning;
    [SerializeField] private bool isGrounded;
    
    
    private Rigidbody rb;
    
    private float speed = 10f; 
    private float stopfactor = 0.99f;
    private float increasefactor = 1f;
    private bool isGrounded;
    void Start()
    {
<<<<<<< Updated upstream
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
=======
        _inlist = new List<Vector3>();
        _rb = GetComponent<Rigidbody>();
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
>>>>>>> Stashed changes
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
