using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelDelay = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisable = false;

    void Start()
    {
       audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        debugKeys();
    }

    void debugKeys()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            nextLevel();
        }

        else if(Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }

        else if(Input.GetKeyDown(KeyCode.X))
        {
            collisionDisable = !collisionDisable; // ilk basista true sonra false yapar ac kapat ozelligi
        }
    }

    void OnCollisionEnter(Collision other)  
    {
        if(isTransitioning || collisionDisable) { return; }  //carpisma olmadiysa veya transition false ise donguye sok

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Press R : Reload Level");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }
    
    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();     //tum sesleri durdur ve basari muzigini caldir
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("nextLevel" , levelDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel" , levelDelay); //1 saniyelik gecikme

    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex); //sahneyi tekrarla  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void nextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings) //bi sonraki sahne son sahneyse, bundan sonraki sahneyi basa dondur
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
