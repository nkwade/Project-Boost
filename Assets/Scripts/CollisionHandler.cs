using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float delay = 1f;
    [SerializeField] private AudioClip crash;
    [SerializeField] private AudioClip success;
    [SerializeField] private ParticleSystem successPart;
    [SerializeField] private ParticleSystem failPart;
    
    private AudioSource audioSource;

    private bool isTransitioning = false;
    private bool isCollisionDisabled = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.L)) {
            NextLevel();
        } else if (Input.GetKeyDown(KeyCode.C)) {
            isCollisionDisabled = !isCollisionDisabled;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (isTransitioning || isCollisionDisabled) {
            return;
        }
        switch (collision.gameObject.tag) {
            case "Friendly":
                break;
            case "Finish":
                isTransitioning = true;
                StartEnd();
                break;
            default:
                isTransitioning = true;
                StartCrash();
                break;
        }
    }

    private void StartEnd()
    {
        this.GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successPart.Play();
        Invoke("NextLevel", delay);
    }

    private void StartCrash() {
        this.GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        failPart.Play();
        Invoke("ReloadLevel", delay);
    }

    private void NextLevel()
    {
        int curScene = SceneManager.GetActiveScene().buildIndex;
        if ((curScene + 1) == SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(0);
        } else {
            SceneManager.LoadScene(curScene + 1);
        }
    }

    private void ReloadLevel()
    {
        int curScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(curScene);
    }
}
