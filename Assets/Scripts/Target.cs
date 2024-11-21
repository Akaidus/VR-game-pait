using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] Collider targetHitbox;
    [SerializeField] GameObject targetAlive;
    [SerializeField] GameObject targetBroken;

    public bool isHit;
    AudioSource audioSource;
    void Start()
    {
        if (targetHitbox == null)
        {
            targetHitbox = GetComponent<Collider>();
        }
        audioSource = GetComponent<AudioSource>();
        isHit = false;
        targetAlive.SetActive(true);
        targetBroken.SetActive(false);
    }

    void TargetHit()
    {
        if(isHit) return;
        audioSource.Play();
        targetAlive.SetActive(false);
        targetBroken.SetActive(true);
        isHit = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rock"))
        {
            TargetHit();
        }
    }
}
