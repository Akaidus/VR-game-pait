using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    int currentSceneIndex;

    void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("PlayerHand"))
            SceneManager.LoadScene(currentSceneIndex+1);
    }
}
