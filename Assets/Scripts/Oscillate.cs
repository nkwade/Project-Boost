using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillate : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 10f;

    Vector3 startPos;
    float movementFactor;

    // Start is called before the first frame update
    void Start()
    {
        startPos =  this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) {return;}
        const float tau = Mathf.PI * 2;
        float cycles = Time.time / period;
        float rawSin = Mathf.Sin(cycles * tau);

        movementFactor = (rawSin + 1f) / 2;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startPos + offset;
    }
}
