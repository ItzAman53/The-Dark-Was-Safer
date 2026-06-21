using TMPro;
using UnityEngine;

public class TreasureUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI treasureText;
    [SerializeField] private PlayerMovement pm;
    void Update()
    {
        treasureText.text=pm.TreasureCount.ToString();
    }




}