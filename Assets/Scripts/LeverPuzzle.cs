using System.Collections;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform leverHandle;
    [SerializeField] private GameObject spotLight;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip leverToggle;
    [SerializeField] private AudioClip stoneSlide;

    [Header("Rotation")]
    [SerializeField] private float targetXRotation = 50f;
    [SerializeField] private float rotateSpeed = 180f;
    private static int activatedLevers = 0;

    [SerializeField] private GameObject vines;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            activated = true;
            StartCoroutine(ActivateLever());
        }
    }

   IEnumerator ActivateLever()
    {
        // Play lever sound
        audioSource.PlayOneShot(leverToggle);

        float currentX = leverHandle.localEulerAngles.x;

        // Convert 310° to -50° if needed
        if (currentX > 180f)
            currentX -= 360f;

        while (Mathf.Abs(currentX - 50f) > 0.1f)
        {
            currentX = Mathf.MoveTowards(currentX, 50f, rotateSpeed * Time.deltaTime);

            leverHandle.localEulerAngles = new Vector3(
                currentX,
                leverHandle.localEulerAngles.y,
                leverHandle.localEulerAngles.z);

            yield return null;
        }

        leverHandle.localEulerAngles = new Vector3(
            50f,
            leverHandle.localEulerAngles.y,
            leverHandle.localEulerAngles.z);

        // Wait for lever sound
        yield return new WaitForSeconds(leverToggle.length);

        // Play stone sound
        audioSource.PlayOneShot(stoneSlide);

        // Wait a little so the light appears while the stone is sliding
        yield return new WaitForSeconds(0.3f);

        spotLight.SetActive(true);

        activatedLevers++;

        if (activatedLevers == 3)
        {
            vines.SetActive(false); // or StartCoroutine(BurnVines());
        }
    }
}