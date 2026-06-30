using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class DestroyOnTask : MonoBehaviour
{
    [SerializeField] private PlayerMovement pm;
    
    void Update()
    {
        if (pm.Level4TreasureCount>=7)
        {
            Destroy(gameObject);
        }

        else if (!gameObject.CompareTag("Level4") && pm.TreasureCount >= 8)
        {
            Destroy(gameObject);
        }
    }
}
