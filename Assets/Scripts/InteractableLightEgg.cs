using System;
using UnityEngine;

public class InteractableEgg : MonoBehaviour
{

    
    [SerializeField] private float maxInteractionTime=2f;
    [SerializeField] private float interactionTime=0f;
    private bool isLit=false;
    private float decayRate=3f;
    [SerializeField] private GameObject enemyPrefab;
    
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
            Instantiate(enemyPrefab,transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }
}
