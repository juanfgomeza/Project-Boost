using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay =1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    [SerializeField] bool collisionsEnabled = true;

    AudioSource audioSource;

    bool isTransiotioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheatKeys();
    }


    void OnCollisionEnter(Collision other) {
        if (isTransiotioning || !collisionsEnabled) return;
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Hit a friendly object");
                break;
            case "Finish":
                Debug.Log("Level completed!");
                StartNextLevelSequence();
                break;
            default:
                StartCrashSequence();                
                break;
        }
        
    }

    void CheatKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();

        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            ToggleDisableCollisions();
        }
    }
    void ToggleDisableCollisions()
    {
        collisionsEnabled = !collisionsEnabled;
    }

    void StartNextLevelSequence()
    {
        isTransiotioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        successParticles.Play();
        Invoke("NextLevel",levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransiotioning = true;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);        
        crashParticles.Play();
        Invoke("ReloadLevel",levelLoadDelay);

        
        
    }

    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
