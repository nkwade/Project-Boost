using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float thrustSpeed = 100f;
    [SerializeField] private float rotation = 1f;
    [SerializeField] private AudioClip engine;

    [SerializeField] private ParticleSystem leftBooster;
    [SerializeField] private ParticleSystem rightBooster;
    [SerializeField] private ParticleSystem mainBooster;

    private Rigidbody rb;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    private void ProcessInput()
    {
        //Thrust
        if (Input.GetAxis("Vertical") > 0) {
            Vector3 thrust = Vector3.up * thrustSpeed * Time.deltaTime;
            rb.AddRelativeForce(thrust);
            if (!audioSource.isPlaying) {
                audioSource.PlayOneShot(engine);
            }
            if (!mainBooster.isPlaying) {
                mainBooster.Play();
            }
        } else {
            mainBooster.Stop();
            audioSource.Stop();
        }

        //Horizontal Movement

        //move to the left
        if (Input.GetAxis("Horizontal") > 0)
        {
            ApplyRotation(-rotation);
            if (!rightBooster.isPlaying) {
                rightBooster.Play();
            }
        }
        //move to the right
        else if (Input.GetAxis("Horizontal") < 0) {
            ApplyRotation(rotation);
            if (!leftBooster.isPlaying) {
                leftBooster.Play();
            }
        } else {
            rightBooster.Stop();
            leftBooster.Stop();
        }
    }

    private void ApplyRotation(float rotSpeed)
    {
        rb.freezeRotation = true;
        Vector3 rotationThrust = Vector3.forward * rotSpeed * Time.deltaTime;
        transform.Rotate(rotationThrust);
        rb.freezeRotation = false;
    }
}
