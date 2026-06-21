using UnityEngine;

public class DestroyOnCollection : MonoBehaviour
{
    [SerializeField]private GameObject treasure;
    [SerializeField]private PlayerMovement pm;
    
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Treasure"))
            {
                pm.TreasureCount++;
            }
            
            Destroy(gameObject);

        }
    }
}
