using TMPro;
using UnityEngine;

public class JournalUI : MonoBehaviour
{
    [SerializeField] private GameObject journalPanel;
    [SerializeField] private TMP_Text journalText;

    private bool isOpen = false;

    void Start()
    {
        journalPanel.SetActive(false);
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;

            journalPanel.SetActive(isOpen);

            Cursor.visible = isOpen;
        }
    }

    public void AddEntry(string entry)
    {
        journalText.text += entry + "\n\n";
    }
}