using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float thrustSpeed = 100f;
    [SerializeField] private float rotation = 1f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        //Thrust
        if (Input.GetAxis("Vertical") > 0) {
            Vector3 thrust = Vector3.up * thrustSpeed * Time.deltaTime;
            rb.AddRelativeForce(thrust);
        }

        //Horizontal Movement
        Vector3 rotationThrust = Vector3.forward * rotation * Time.deltaTime;
        if (Input.GetAxis("Horizontal") > 0) {
            transform.Rotate(-rotationThrust);
        } else if (Input.GetAxis("Horizontal") < 0) {
            transform.Rotate(rotationThrust);
        }
    }
}
