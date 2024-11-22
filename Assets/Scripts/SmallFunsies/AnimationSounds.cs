using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSounds : MonoBehaviour
{
    [SerializeField] private SingleSound explosion;
    [SerializeField] private SingleSound ddVoiceClip;

    public void Explode()
    {
        explosion.PlaySound();
    }

    public void Funsies()
    {
        ddVoiceClip.PlaySound();
    }
}
