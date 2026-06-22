using UnityEngine;

public class DestroyOnCollection : MonoBehaviour
{
    [SerializeField]private GameObject treasure;
    [SerializeField]private PlayerMovement pm;
    [SerializeField]private GameObject collectEffect;
    
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Treasure"))
            {
                pm.TreasureCount++;
            }
            Instantiate(collectEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }
}
