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
    [SerializeField] private GameObject Flashlight;
    [SerializeField] private GameObject Lights;
    [SerializeField] private Light LanternSpotLight;
    [SerializeField] private AudioClip CaveEnter;
    [SerializeField] private AudioClip CaveAmbience;
    [SerializeField] private AudioSource Source;
    [SerializeField] private JournalUI journal;


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

        if (gameObject.CompareTag("Level0"))
        {
            Lantern.SetActive(false);
            Flashlight.SetActive(true);
            Lights.SetActive(false);
            LanternSpotLight.enabled = true;
            journal.AddEntry("I've entered the cave. The air feels strangely still...Let's find the lantern and get out.");
        }
        if (gameObject.CompareTag("Level1"))
        {
            
            journal.AddEntry("The amount of treasure left behind is unsettling...\nAlmost as if no one ever managed to leave with it.");
        }
        if (gameObject.CompareTag("LevelLantern"))
        {
            
            journal.AddEntry("The moment I took the lantern, the cave sealed itself.\nFlashlight died...\nGotta find another way out.");
        }
        if (gameObject.CompareTag("Level2"))
        {
            
            journal.AddEntry("The lantern's light burns through the strange vines.\nThey're unlike any plant I've ever seen...\nAlmost as if they're alive.");
        }
        if (gameObject.CompareTag("Level3"))
        {
            
            journal.AddEntry("The creatures only appeared after I started carrying the lantern.\nThat can't be a coincidence.\nThough the lantern also freezes them.");
        }


        

        

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
