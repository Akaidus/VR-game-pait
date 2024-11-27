using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    int currentSceneIndex;
    int totalScenes;

    void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        totalScenes = SceneManager.sceneCountInBuildSettings;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerHand"))
        {
            var nextScene = (currentSceneIndex + 1) % totalScenes;
            SceneManager.LoadScene(nextScene);
        }
    }
}
