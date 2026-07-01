using UnityEngine;
using TMPro;

public class NoteCollection : MonoBehaviour
{
   [SerializeField] private GameObject noteUI;
    [SerializeField] private TMP_Text noteText;
     [Header("Journal")]
    [SerializeField, TextArea(5,10)]
    private string noteToRead;

    [SerializeField, TextArea(5,10)]
    private string journalEntry;
    [SerializeField] private JournalUI journal;

    [Header("Effects")]
    [SerializeField] private GameObject collectEffect;

    private bool reading = false;
    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;
        if (!other.CompareTag("Player")) return;

        collected = true;

        // Destroy all child objects (paper mesh, etc.)
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Instantiate(collectEffect, transform.position, Quaternion.identity);

        noteUI.SetActive(true);
        noteText.text = noteToRead;

        reading = true;
    }

    private void Update()
    {
        if (!reading) return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            noteUI.SetActive(false);

            journal.AddEntry(journalEntry);

            reading = false;

            Destroy(gameObject);
        }
    }
}
