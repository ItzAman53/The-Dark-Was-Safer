using UnityEngine;

public class DestroyOnCollection : MonoBehaviour
{
    [SerializeField]private GameObject treasure;
    
    [SerializeField]private GameObject collectEffect;
    
    
    
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Treasure"))
            {
                other.GetComponent<PlayerMovement>().TreasureCount++;
                if (other.GetComponent<PlayerMovement>().isLevel4)
                {
                    other.GetComponent<PlayerMovement>().Level4TreasureCount++;
                }
            }
            
            Instantiate(collectEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }
}
