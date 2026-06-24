using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
[SerializeField] private GameObject introPanel; 
[SerializeField] private TextMeshProUGUI storyText;
[SerializeField] private float typingSpeed = 0.03f;
    public void StartGame()
    {
        StartCoroutine(StartSequence());
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Quit Game");
    }
    IEnumerator StartSequence()
    {
        menuPanel.SetActive(false);
        introPanel.SetActive(true);

        storyText.text = "";

        yield return StartCoroutine(TypeText(
            "You are an archaeologist."));

        yield return new WaitForSeconds(1f);

        storyText.text += "\n\n";

        yield return StartCoroutine(TypeText(
            "The cave was rumored to hold treasures untouched for centuries."));

        yield return new WaitForSeconds(1f);

        storyText.text += "\n\n";

        yield return StartCoroutine(TypeText(
            "Most who entered never returned."));

        yield return new WaitForSeconds(1f);

        storyText.text += "\n\n";

        yield return StartCoroutine(TypeText(
            "You came anyway."));

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("LevelScene");
    }

    IEnumerator TypeText(string message)
    {
        foreach (char c in message)
        {
            storyText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}