using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BasicEnemy : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;

    float timePassed;
    [SerializeField] float timeThreshold;
    [SerializeField] float speedIncrease;
    float currentSpeed;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        currentSpeed = agent.speed;
    }

    void Update()
    {
        agent.SetDestination(player.transform.position);
        timePassed += Time.deltaTime;
        if (timePassed > timeThreshold)
        {
            currentSpeed += speedIncrease;
            if (currentSpeed > 20)
                currentSpeed = 20;
            agent.speed = currentSpeed;
            timePassed = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && player != null)
        {
            SceneManager.LoadScene(0);
        }
    }
}
