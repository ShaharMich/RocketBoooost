using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float thrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineP;
    [SerializeField] ParticleSystem rightEngine;
    [SerializeField] ParticleSystem leftEngine;
    AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Boost();
        }
        else
        {
            audioSource.Stop();
            mainEngineP.Stop();
        }
        
        if(Input.GetKey(KeyCode.RightArrow))
        {
            TurnLeft();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            TurnRight();
        }
    }

    void Boost()
    {
        rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineP.isPlaying)
        {
            mainEngineP.Play();
        }
    }

    void TurnRight()
    {
        rb.freezeRotation = true;
        transform.Rotate(-Vector3.forward * rotationThrust * Time.deltaTime);
        rb.freezeRotation = false;
    }

    void TurnLeft()
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
