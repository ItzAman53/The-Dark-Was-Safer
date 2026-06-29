using UnityEngine;
using System.Collections;

public class InteractableLightVines : MonoBehaviour
{

    
    [SerializeField] private float maxInteractionTime=2f;
    [SerializeField] private float interactionTime=0f;
    [SerializeField] private GameObject destroyEffect;
    private PlantWiggle wiggle;
    private bool startedWiggle = false;


    private bool isLit=false;
    private float decayRate=3f;
    
    void Start()
    {
        wiggle=GetComponent<PlantWiggle>();
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerMovement>().IsEscapeRunning)
        {
            isLit=true;
            interactionTime+=Time.deltaTime;
        }
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

    public void BurnByPuzzle()
    {
        if (startedWiggle) return;

        startedWiggle = true;
        wiggle.IsWiggling = true;

        StartCoroutine(BurnRoutine());
    }

    private IEnumerator BurnRoutine()
    {
        yield return new WaitForSeconds(maxInteractionTime * 0.5f);

        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
