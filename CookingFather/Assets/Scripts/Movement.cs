
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
<<<<<<< HEAD
    public Rigidbody rb;
    private float _walkspeed = 7f;
    
    private Vector3 _inputs;
    
    private List<Vector3> Inputs;
=======
<<<<<<< Updated upstream
=======
    private List<Vector3> _inlist;
>>>>>>> Desktop
    
    private Rigidbody _rb;

    [SerializeField] private float walkspeed;
    [SerializeField] private float runspeed;
    
    private Vector3 _inputs;
    
>>>>>>> Stashed changes
    
    [SerializeField] private bool isRunning;
    [SerializeField] private bool isGrounded;
    
    void Start()
    {
<<<<<<< Updated upstream
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
<<<<<<< HEAD
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = (transform.right * x + transform.forward * z) * _walkspeed;
        move *= Time.fixedDeltaTime;
        rb.MovePosition(transform.position + move);
=======
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        isGrounded = true;
=======
        _inlist = new List<Vector3>();
        _rb = GetComponent<Rigidbody>();
>>>>>>> Desktop
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
