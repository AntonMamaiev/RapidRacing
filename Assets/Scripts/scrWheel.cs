using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrWheel : MonoBehaviour
{
    private Rigidbody rb;

    public bool FR;
    public bool FL;
    public bool RR;
    public bool RL;


    //[Header("Speed")]
    //public float Speed;

    [Header("Suspension")]
    public float restLength;
    public float springTravel;
    public float springStiffness;
    public float damperStiffness;

    [Header("Wheel")]
    public float wheelRadius;
    public float steerAngle;
    public float wheelAngle;
    public float steerTime;


    private float springLength;
    private float minLength;
    private float maxLength;
    private float lastLength;
    private float springVelocity;
    private float springForce;
    private float damperForce;

    private Vector3 suspensionForce;
    private Vector3 wheelVelocity;
    private float ForceX;
    private float ForceY;

    void Start()
    {
        rb = transform.root.GetComponent<Rigidbody>();

        minLength = restLength - springTravel;
        maxLength = restLength + springTravel;
    }

    void Update()
    {
        wheelAngle = Mathf.Lerp(wheelAngle, steerAngle, steerTime * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(Vector3.up * wheelAngle);

        Debug.DrawRay(transform.position, -transform.up * (springLength + wheelRadius), Color.blue);
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLength + wheelRadius))
        {
            lastLength = springLength;

            springLength = hit.distance - wheelRadius;
            springLength = Mathf.Clamp(springLength, minLength, maxLength);

            springVelocity = (lastLength - springLength) / Time.fixedDeltaTime;

            springForce = springStiffness * (restLength - springLength);
            damperForce = damperStiffness * springVelocity;

            suspensionForce = (springForce + damperForce) * transform.up;

            wheelVelocity = transform.InverseTransformDirection(rb.GetPointVelocity(hit.point));
            ForceX = Input.GetAxis("Vertical") * 500 * 2f;
            //if (ForceX > 800)
            //    ForceX = 800;
            ForceY = wheelVelocity.x * springForce;


            rb.AddForceAtPosition(suspensionForce + (ForceX * transform.forward) + (ForceY * -transform.right), hit.point);
            //Debug.Log(suspensionForce + (/*ForceX **/ transform.forward) + (ForceY * -transform.right));
        }
    }
}