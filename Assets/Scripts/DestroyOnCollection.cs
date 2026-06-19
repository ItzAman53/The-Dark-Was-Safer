using UnityEngine;

public class DestroyOnCollection : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            Destroy(gameObject);

        }
    }
}
