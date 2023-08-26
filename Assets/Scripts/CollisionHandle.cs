using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandle : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem particleSuccess;
    [SerializeField] ParticleSystem crashTicle;
    AudioSource audioSource;
    bool isTransitioning = false; 
    bool collDis = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        DebugKeys();
    }

    void DebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            loadNextLevel();
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            collDis = !collDis; //toggle collision
        }
    }
    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || collDis) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "finish":
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
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        particleSuccess.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("loadNextLevel", levelLoadDelay);
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashTicle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }
    void ReloadLevel()
    {
        int currLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currLevel);
    }
    
    void loadNextLevel()
    {
        int currLevel = SceneManager.GetActiveScene().buildIndex;
        int nxtScene = currLevel +1;
        if(nxtScene == SceneManager.sceneCountInBuildSettings)
        {
            nxtScene = 0;
        }
        SceneManager.LoadScene(nxtScene);
    }
}
