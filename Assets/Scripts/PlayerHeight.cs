using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeight : MonoBehaviour
{
    [SerializeField] float maxHeight = 0.7f;
    
    void Update()
    {
        LimitHeight();
    }

    void LimitHeight()
    {
        var camHeight = transform.position;
        if (camHeight.y > maxHeight || camHeight.y < maxHeight)
        {
            camHeight.y = maxHeight;
            transform.position = camHeight;
        }
    }
}
