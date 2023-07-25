using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class scrCenterMass : MonoBehaviour
{
    public Vector3 CenterOfMass;
    public bool Awake;
    protected Rigidbody r;
    void Start()
    {
        r = GetComponent<Rigidbody>();
    }

    void Update()
    {
        r.centerOfMass = CenterOfMass;
        r.WakeUp();
        Awake = !r.IsSleeping();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * CenterOfMass, 0.5f);
    }
}
