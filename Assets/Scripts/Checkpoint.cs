using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isCollected;
    [SerializeField] MeshRenderer flag;
    [SerializeField] MeshRenderer button;
    [SerializeField] Material newFlag;
    [SerializeField] Material newButton;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("checkpoint collected");
            flag.materials[2] = newFlag;
            button.materials[1] = newButton;
            isCollected = true;
        }
    }
}
