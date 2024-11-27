using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LookForTimer : MonoBehaviour
{
    
    void Start()
    {
        Invoke(nameof(GetTime), 1f);
    }

    void GetTime()
    {
        var timer = GameObject.Find("NotATimer").GetComponent<NotATimerScript>();
        GetComponent<TMP_Text>().text = timer.finalTimeString;
        timer.notTimer = 0;
    }
}
