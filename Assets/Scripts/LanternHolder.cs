using System.Collections;
using TMPro;
using UnityEngine;
using Unity.Cinemachine;

public class LanternHolder : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject playerLantern;
    [SerializeField] private GameObject lanternOnPedestal;
    [SerializeField] private GameObject Crystal;

    [SerializeField] private GameObject caveLights;

    [SerializeField] private TextMeshProUGUI subtitleText;

    [SerializeField] private Transform escapeSpawnPoint;
    [SerializeField] private GameObject LightBlast;
    [SerializeField] private Animator anim;
    [SerializeField] private Light SpotLight;
    [SerializeField] private CinemachineBasicMultiChannelPerlin noise;
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private Transform archaeologistModel;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip Rumble;
    [SerializeField] private AudioClip TitleMusic;


    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (activated)
            return;

        activated = true;

        StartCoroutine(FinalSequence(other.transform));
    }

    private IEnumerator FinalSequence(Transform player)
    {
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null)
            cc.enabled = false;
        // Place lantern
        playerLantern.SetActive(false);
        lanternOnPedestal.SetActive(true);
        SpotLight.enabled=false;
        anim.SetTrigger("NoLantern");
        yield return new WaitForSeconds(1f);
        Instantiate(LightBlast,transform.position,Quaternion.identity);
        yield return new WaitForSeconds(1f);

        
        caveLights.SetActive(true);

        // Subtitle
        yield return StartCoroutine(TypeText("The lantern has found its home. The cave was waiting for you"));

        yield return new WaitForSeconds(3f);

        // Light up cave
        

        yield return new WaitForSeconds(1f);

        // Teleport to escape section
        

        

        
        yield return new WaitForSeconds(0.2f);
        

        yield return StartCoroutine(TypeText("You were never the treasure hunter. You were the COURIER.") );
        yield return new WaitForSeconds(1.5f);
        audioSource.Stop();
        audioSource.clip=Rumble;
        audioSource.Play();
        StartRumble();
        subtitleText.text = "";

        yield return new WaitForSeconds(1f);
        
        

        player.position = escapeSpawnPoint.position;

        

        Crystal.SetActive(true);
        SpotLight.enabled=true;
        yield return StartCoroutine(TypeText("The cave is collapsing. The shard will protect you. LEAVE.") );
        yield return new WaitForSeconds(2f);
        subtitleText.text = "";

        


        pm.IsEscapeRunning=true;
        if(pm.IsEscapeRunning)
        {
            archaeologistModel.SetParent(player.transform);
            archaeologistModel.rotation = escapeSpawnPoint.rotation;
        }
        if (cc != null)
            cc.enabled = true;

        anim.SetTrigger("Run");

        yield return new WaitForSeconds(4f);

        audioSource.Stop();
        audioSource.clip=TitleMusic;
        audioSource.Play();

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
    public void StartRumble()
    {
        noise.AmplitudeGain = 3f;
        noise.FrequencyGain = 2f;
    }

    public void StopRumble()
    {
        noise.AmplitudeGain = 0f;
    }
}