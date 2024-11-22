using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    SingleSound singleSound;

    void Awake()
    {
        if (singleSound == null)
        {
            singleSound = GetComponent<SingleSound>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground") || other.CompareTag("Wall"))
            singleSound.PlaySound();
    }
}
