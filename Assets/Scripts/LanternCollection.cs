using System.Collections;
using UnityEngine;
using Unity.Cinemachine;
using TMPro;

public class LanternCollection : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerMovement player;

    [Header("Flashlight")]
    [SerializeField] private GameObject flashlight;

    [Header("Lantern")]
    [SerializeField] private GameObject playerLantern;
    [SerializeField] private Light lanternLight;
    [SerializeField] private GameObject areaLights;

    [Header("World")]
    [SerializeField] private GameObject entranceRocks;
    [SerializeField] private GameObject exitGate;

    [Header("Effects")]
    [SerializeField] private CinemachineBasicMultiChannelPerlin noise;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip rumble;

    [Header("Timings")]
    [SerializeField] private float pauseBeforeRumble = 6f;
    [SerializeField] private float rumbleDuration = 4f;
    [SerializeField] private TextMeshProUGUI subtitleText;


    bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;
            StartCoroutine(CollectionSequence());
        }
    }

    IEnumerator CollectionSequence()
    {
        // Hide collectible
        GetComponent<Collider>().enabled = false;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        yield return new WaitForSeconds(pauseBeforeRumble);

        // Freeze player
        CharacterController cc = player.GetComponent<CharacterController>();

        cc.enabled = false;

        

        // Start rumble
        audioSource.PlayOneShot(rumble);
        StartRumble();
        yield return new WaitForSeconds(rumbleDuration);
        areaLights.SetActive(false);
        

        // Close entrance
        entranceRocks.SetActive(true);

        // Reveal exit
        exitGate.SetActive(true);

        
        // Stop shaking
        StopRumble();
        audioSource.Stop();
        StartCoroutine(TypeText("The entrance is blocked...Looks like I'll have to explore deeper."));
        yield return new WaitForSeconds(6f);
        subtitleText.text = "";


        lanternLight.enabled = false;
        yield return new WaitForSeconds(0.4f);
        lanternLight.enabled = true;
        yield return new WaitForSeconds(0.4f);
        lanternLight.enabled = false;
        yield return new WaitForSeconds(0.4f);
        lanternLight.enabled = true;
        yield return new WaitForSeconds(0.4f);
        lanternLight.enabled = false;

        StartCoroutine(TypeText("Ah Damn it, the flashlight's dead as well"));
        yield return new WaitForSeconds(4f);
        subtitleText.text = "";
        StartCoroutine(TypeText("...looks like this lantern is my only light now."));
        yield return new WaitForSeconds(4f);
        subtitleText.text = "";




        

        // Torch off
        flashlight.SetActive(false);
        yield return new WaitForSeconds(0.6f);

        // Lantern comes out
        playerLantern.SetActive(true);
        lanternLight.enabled = true;

        

        // Unfreeze player
        cc.enabled = true;

        Destroy(gameObject);
    }

    public void StartRumble()
    {
        noise.AmplitudeGain = 3f;
        noise.FrequencyGain = 2f;
    }

    public void StopRumble()
    {
        noise.AmplitudeGain = 0f;
    }
    private IEnumerator TypeText(string message)
    {
        subtitleText.text = "";

        foreach (char c in message)
        {
            subtitleText.text += c;
            yield return new WaitForSeconds(0.02f);
        }
    }
}