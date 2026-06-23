using TMPro;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    [SerializeField] private GameObject whiteImage;
    [SerializeField] private GameObject coinCouhht;
    [SerializeField] private TextMeshProUGUI endText;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if (!other.CompareTag("Player"))
            return;

        triggered = true;

        whiteImage.SetActive(true);
        coinCouhht.SetActive(false);

        endText.gameObject.SetActive(true);
        

        endText.text =
@"You escaped.

The lantern remained behind.

Yet the crystal still glows in your hand.

You are not sure why.

Thanks for playing.";
    }
}