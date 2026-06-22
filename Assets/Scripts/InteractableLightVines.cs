using UnityEngine;

public class InteractableLightVines : MonoBehaviour
{

    
    [SerializeField] private float maxInteractionTime=2f;
    [SerializeField] private float interactionTime=0f;
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private PlantWiggle wiggle;
    private bool startedWiggle = false;

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

        if (!startedWiggle && interactionTime >= maxInteractionTime * 0.5f)
        {
            startedWiggle = true;
            wiggle.IsWiggling = true;

            // Start wiggle
        }

        if (interactionTime >= maxInteractionTime)
        {
            Instantiate(destroyEffect,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
        
    }
}
