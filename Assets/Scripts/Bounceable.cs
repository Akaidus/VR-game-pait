using UnityEngine;
using Random = UnityEngine.Random;

public class Bounceable : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] float minPitch, maxPitch;

    Rigidbody rb;
    float movementThreshold = 0.01f;
    [SerializeField] float bounceMultiplier;
    
    [SerializeField] public bool isCaptureDevice;
    GameObject capturedObject;
    bool readyToCapture;
    bool containsCapture;
    bool readyToSummon;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Velocity();
    }

    void Velocity()
    {
        //stops the ball from rolling infinitely. maybe
        if (rb.velocity.magnitude <= movementThreshold)
        {
            rb.velocity = Vector3.zero;
        }
    }
    
    void PerformCapture(GameObject obj)
    {
        if (obj.gameObject.GetComponent<Captureable>() && !containsCapture)
        {
            audioSource.volume = .5f;
            audioSource.PlayOneShot((AudioClip)Resources.Load("Audio/Pokeball/Pokeball return SFX"));
            readyToCapture = false;
            capturedObject = obj.gameObject;
            obj.gameObject.GetComponent<Captureable>().StartShrink();
            containsCapture = true;
        }
        else
        {
            print("capture failed / unable to catch");
        }
    }
    
    public void ActivateCaptureDevice()
    {
        if(!isCaptureDevice) return;
        if(capturedObject == null)
        {
            if (!readyToCapture)
            {
                readyToCapture = true;
                audioSource.PlayOneShot((AudioClip)Resources.Load("Audio/Pokeball/Pokeball enlarge SFX"));
            }
            else if(readyToCapture)
            {
                readyToCapture = false;
                audioSource.PlayOneShot((AudioClip)Resources.Load("Audio/Pokeball/Pokeball shrink SFX"));
            }
            print("no capture contained");
        }
        
        else if (capturedObject != null)
        {
            if (!readyToSummon) 
            {
                readyToSummon = true;
                audioSource.volume = 1;
                audioSource.PlayOneShot((AudioClip)Resources.Load("Audio/Pokeball/Pokeball enlarge SFX"));
            }
            else if(readyToSummon)
            {
                readyToSummon = false;
                audioSource.volume = 1;
                audioSource.PlayOneShot((AudioClip)Resources.Load("Audio/Pokeball/Pokeball shrink SFX"));
            }
        }
    }

    void SummonCapture()
    {
        audioSource.volume = .5f;
        audioSource.PlayOneShot((AudioClip)Resources.Load("Sound/Pokeball/Pokeball summon SFX"));
        readyToCapture = false;
        transform.DetachChildren();
        capturedObject.transform.rotation = Quaternion.Euler(0,0,0);
        capturedObject.GetComponent<Captureable>().Summon();
        capturedObject.GetComponent<Collider>().enabled = false;
        containsCapture = false;
        capturedObject = null;
        readyToSummon = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (audioSource)
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.Play();
        }
        if (isCaptureDevice && containsCapture && readyToSummon)
        {
            SummonCapture();
        }
        else if (isCaptureDevice && capturedObject == null && readyToCapture && !containsCapture)
        {
            PerformCapture(collision.gameObject);
        }
        Vector3 bounceDirection = Vector3.Reflect(-collision.relativeVelocity, collision.GetContact(0).normal);
        rb.velocity = bounceDirection * bounceMultiplier;
    }
}
