using UnityEngine;

public class LanternVines : MonoBehaviour
{
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private GameObject explosionPoint;

    public void BurnAway()
    {
        Instantiate(destroyEffect, explosionPoint.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}