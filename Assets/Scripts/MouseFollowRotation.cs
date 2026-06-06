
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollowRotation : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask layer;
    
    [SerializeField] private Vector3 target;
    private float rotationSpeed = 720f;
    

    void Update()
    {
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit,float.MaxValue,layer))
        {

            target = hit.point;
            target.y = transform.position.y;

            Quaternion targetRotation = Quaternion.LookRotation(target-transform.position);

            transform.rotation = Quaternion.RotateTowards(transform.rotation,targetRotation,rotationSpeed * Time.deltaTime);
        }
    }
}