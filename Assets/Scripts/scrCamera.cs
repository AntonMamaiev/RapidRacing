using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCamera : MonoBehaviour
{
    public GameObject Target;

    public GameObject T;

    public float speed = 1.5f;

    public int index;

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("CameraTarget");
        T = GameObject.FindGameObjectWithTag("Target");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.LookAt(Target.transform);
        float car_Move = Mathf.Abs(Vector3.Distance(this.transform.position, T.transform.position) * speed);
        this.transform.position = Vector3.MoveTowards(this.transform.position, T.transform.position, car_Move * Time.fixedDeltaTime);
    }
}
