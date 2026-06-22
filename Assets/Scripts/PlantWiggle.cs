using UnityEngine;

public class PlantWiggle : MonoBehaviour
{
    [SerializeField] private float wiggleSpeed = 20f;
    [SerializeField] private float wiggleAmount = 5f;

    private Quaternion startRotation;

    public bool IsWiggling = false;

    private void Start()
    {
        startRotation = transform.localRotation;
    }

    private void Update()
    {
        if (IsWiggling)
        {
            float angle = Mathf.Sin(Time.time * wiggleSpeed) * wiggleAmount;

            transform.localRotation =
                startRotation *
                Quaternion.Euler(0f, angle, 0f);
        }
        else
        {
            transform.localRotation = startRotation;
        }
    }
}