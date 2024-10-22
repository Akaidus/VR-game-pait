using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject bulletPrefab;
    float despawnTime = 5f;

    [SerializeField] float bulletSpeed;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        audioSource.Play();
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = spawnPoint.position;
        newBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * bulletSpeed;
        Destroy(newBullet, despawnTime);
    }
}
