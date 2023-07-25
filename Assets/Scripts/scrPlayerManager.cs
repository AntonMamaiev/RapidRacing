using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayerManager : MonoBehaviour
{
    [Header("Air Stats")]
    public float responsiveness = 10f;
    public GameState gameState;

    private float roll;
    private float pitch;
    public float Downforce = 50;


    public float raylength;


    private RaycastHit hit;
    private Vector3 dir = -Vector3.up;


    private float responseModifier
    {
        get
        {
            return (rb.mass / 10f) * responsiveness;
        }
    }

    Rigidbody rb;


    private void Awake()
    {
        rb = transform.root.GetComponent<Rigidbody>();
    }

    private void HandleInputs()
    {
        roll = Input.GetAxis("Roll");
        pitch = Input.GetAxis("Pitch");
    }

    private void Update()
    {
        if (gameState.RaceInProgress)
            HandleInputs();
    }
    void FixedUpdate()
    {
        ////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////// Поворот у повітрі /////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////

        if (Physics.Raycast(transform.position, dir * raylength, out hit))
        {
            rb.AddTorque(transform.right * pitch * responseModifier);
            rb.AddTorque(transform.up * roll * responseModifier);
            rb.maxAngularVelocity = 15f;
            rb.AddForce(-transform.up * Downforce * rb.velocity.magnitude);
        }
    }
}
