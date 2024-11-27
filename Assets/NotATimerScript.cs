using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NotATimerScript : MonoBehaviour
{
    public float notTimer;
    bool gameCompleted;
    float finalTime;
    public string finalTimeString;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 1)
        {
            notTimer += Time.deltaTime;
            print(TimeSpan.FromSeconds(notTimer).ToString("mm\\:ss\\.ff"));
        }
        else
        {
            finalTime = notTimer;
            finalTimeString = $"Final Time:\n{TimeSpan.FromSeconds(finalTime):mm\\:ss\\.ff}";
        }
    }
}
