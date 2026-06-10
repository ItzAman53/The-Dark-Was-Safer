using UnityEngine;

public class InteractableLight : MonoBehaviour
{

    
    private float maxInteractionTime=2f;
    [SerializeField] private float interactionTime=0f;
    private bool isLit=false;
    private float decayRate=3f;
    
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("LightCollider"))
        {
            isLit=true;
            interactionTime+=Time.deltaTime;
            
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightCollider"))
        {
            isLit=false;
        }
    }

    void Update()
    {
        if (!isLit)
        {
            interactionTime-=Time.deltaTime*decayRate;
            interactionTime=Mathf.Max(0,interactionTime);
        }

        if (interactionTime >= maxInteractionTime)
        {
            Destroy(gameObject);
        }
    }
}
