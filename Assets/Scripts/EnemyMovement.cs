using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 0.25f;
    [SerializeField] private Renderer enemyRenderer;
    [SerializeField] private Renderer glowingChildRenderer;

    [Header("Freeze")]
    [SerializeField] private float maxInteractionTime = 2f;
    [SerializeField] private float decayRate = 1f;
    [SerializeField] private float freezeTime = 4f;
    
    [SerializeField] private Material freezeGlow;
    [SerializeField] private Material normalGlow;

    private Transform player;

    private float interactionTime;
    private bool isLit;
    private bool isFrozen;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void Update()
    {
        HandleFreezeMeter();

        if (!isFrozen)
        {
            FollowPlayer();
        }
    }

    private void HandleFreezeMeter()
    {
        if (isFrozen)
            return;

        if (isLit)
        {
            interactionTime += Time.deltaTime;

            if (interactionTime >= maxInteractionTime)
            {
                StartCoroutine(FreezeEnemy());
            }
        }
        else
        {
            interactionTime -= decayRate * Time.deltaTime;
            interactionTime = Mathf.Max(0f, interactionTime);
        }
    }

    private void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );
    }

    private IEnumerator FreezeEnemy()
    {
        isFrozen = true;
        enemyRenderer.material.color = Color.cyan;
        glowingChildRenderer.material=freezeGlow;
        // TODO:
        // Change material color
        // Play VFX
        // Play freeze sound

        yield return new WaitForSeconds(freezeTime);
        enemyRenderer.material.color = Color.white;
        glowingChildRenderer.material=normalGlow;

        interactionTime = 0f;
        isFrozen = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("LightCollider"))
        {
            isLit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LightCollider"))
        {
            isLit = false;
        }
    }
}