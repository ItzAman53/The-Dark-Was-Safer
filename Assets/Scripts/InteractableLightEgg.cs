using System;
using UnityEngine;

public class InteractableEgg : MonoBehaviour
{

    
    [SerializeField] private float maxInteractionTime=2f;
    [SerializeField] private float interactionTime=0f;
    private bool isLit=false;
    private float decayRate=3f;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private PlantWiggle eggWiggle;
    [SerializeField] private GameObject hatchEffect;

    private bool startedWiggle = false; 
    
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
            eggWiggle.IsWiggling = true;
        }

        if (interactionTime >= maxInteractionTime)
        {
            Instantiate(hatchEffect, transform.position, Quaternion.identity);
            Instantiate(enemyPrefab,transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }
}
