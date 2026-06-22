using UnityEngine;

public class DestroyOnTask : MonoBehaviour
{
    [SerializeField] private PlayerMovement pm;
    
    void Update()
    {
        if (pm.TreasureCount >= 12)
        {
            Destroy(gameObject);
        }
    }
}
