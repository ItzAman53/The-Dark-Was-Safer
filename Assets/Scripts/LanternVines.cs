using UnityEngine;

public class LanternVines : MonoBehaviour
{
    [SerializeField] private GameObject destroyEffect;

    public void BurnAway()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}