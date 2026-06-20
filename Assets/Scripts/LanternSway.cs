using UnityEngine;

public class LanternSway : MonoBehaviour
{
    [SerializeField] private float swayAmount = 5f;
    [SerializeField] private float swaySpeed = 6f;
    [SerializeField] private float moveThreshold = 0.1f;


    private Vector3 lastPosition;
    private Quaternion initialRotation;

    private void Start()
    {
        lastPosition = transform.root.position;
        initialRotation = transform.localRotation;
    }

    private void Update()
    {
        float speed = (transform.root.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = transform.root.position;

        if (speed > moveThreshold)
        {
            float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;

            transform.localRotation =
                initialRotation *
                Quaternion.Euler(sway, 0f, 0f);
        }
        else
        {
            transform.localRotation = Quaternion.Lerp(
                transform.localRotation,
                initialRotation,
                5f * Time.deltaTime);
        }
    }
}