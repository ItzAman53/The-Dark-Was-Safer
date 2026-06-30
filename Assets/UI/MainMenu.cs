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
            "Legends speak of an ancient magical lantern hidden deep within these forgotten caves."));

        yield return new WaitForSeconds(1f);

        storyText.text += "\n\n";

        yield return StartCoroutine(TypeText(
            "Your mission is simple: recover it and return it to the museum."));

        yield return new WaitForSeconds(1f);

        storyText.text += "\n\n";

        yield return StartCoroutine(TypeText(
            "What could possibly go wrong?"));

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