using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class TeleportOnTouch : MonoBehaviour
{
    
    [SerializeField] private GameObject teleportPosition;
    [SerializeField] private FadeScreen fadeScreen;
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private GameObject Lantern;
    [SerializeField] private GameObject Lights;
    [SerializeField] private Light LanternSpotLight;
    [SerializeField] private AudioClip CaveEnter;
    [SerializeField] private AudioClip CaveAmbience;
    [SerializeField] private AudioSource Source;


    private bool isTeleporting;
    
    
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Teleport(other));
        }
    }

    

    private IEnumerator Teleport(Collider other)
    {
        if (isTeleporting) yield break;
        isTeleporting = true;
        CharacterController cc = other.GetComponent<CharacterController>();

        if (cc != null)
            cc.enabled = false;
        

        fadeScreen.StartCoroutine(fadeScreen.Fade(4f));
        Source.Stop();
        Source.clip=CaveEnter;
        Source.Play();
        yield return new WaitForSeconds(CaveEnter.length);
        Source.clip=CaveAmbience;
        Source.Play();


        if (gameObject.CompareTag("Level3"))
        {
            pm.isLevel4=true;
        }
        
        if (gameObject.CompareTag("Level4"))
        {
            pm.GetComponent<CharacterController>().stepOffset=1.3f;
        }

        yield return new WaitForSeconds(0.25f);

        pm.TurnOnHorizontalMovement = true;
        pm.anim.SetBool("isLantern", true);

        Lantern.SetActive(true);
        Lights.SetActive(false);
        LanternSpotLight.enabled = true;

        

        Vector3 target = teleportPosition.transform.position;

        // Jam-proof teleport 💀
        other.transform.position = target;
        yield return null;
        other.transform.position = target;

        if (cc != null)
            cc.enabled = true;
        isTeleporting = false;

        Debug.Log("Teleported to: " + target);
    }   
}
