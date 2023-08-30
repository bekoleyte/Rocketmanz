using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;

    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem rocketParticles;
    [SerializeField] ParticleSystem leftParticles;
    [SerializeField] ParticleSystem rightParticles;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
       rb = GetComponent<Rigidbody>();
       audioSource = GetComponent<AudioSource>();
    }

  
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            startThrust();
        }

        else 
        {
                stopThrust();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            rotateLeft();
        }

        else if(Input.GetKey(KeyCode.D))
        {
            rotateRight();
        }

        else
        {
             stopRotate();
        }
    }

    void startThrust()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); // vector3.up equal to (0,1,0)
            
        if(!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine); //farklı dosya calmasi icin serialfield yapcaz
        }

        if(!rocketParticles.isPlaying)
        {
            rocketParticles.Play();
        }
    }

    void stopThrust()
    {
        audioSource.Stop();
        rocketParticles.Stop();
    }

    void rotateLeft()
    {
        ApplyRotation(rotationThrust);
           
        if(!rightParticles.isPlaying)
        {
           rightParticles.Play();
        }
    }
        
    void rotateRight()
    {
        ApplyRotation(-rotationThrust);
            
        if(!leftParticles.isPlaying)
        {
          leftParticles.Play();
        }
    }

    void stopRotate()
    {
        rightParticles.Stop();
        leftParticles.Stop();
    }

    public void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freeze rotation so we  can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreeze
    }

}
