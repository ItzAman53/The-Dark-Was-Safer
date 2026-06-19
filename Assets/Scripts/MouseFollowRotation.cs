
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollowRotation : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private PlayerMovement pm;
    [SerializeField] private LayerMask layer;
    
    [SerializeField] private Vector3 target;
    private float rotationSpeed = 720f;
    private float deadZoneRadius=0.5f;
    private float aimDistance=10f;
    
    void Start()
    {
        
        Cursor.visible=false;
    }

    void Update()
    {
        
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit,float.MaxValue,layer))
        {
            Vector3 aimDirection = (hit.point-transform.position).normalized;
            target = transform.position+aimDirection*aimDistance;
            target.y = transform.position.y;

            if ((target - transform.position).magnitude < deadZoneRadius)
            {
                return;
            }

            Quaternion targetRotation = Quaternion.LookRotation(target-transform.position);
            if (!pm.TurnOnHorizontalMovement)
            {
                rotationSpeed=0;
            }

            transform.rotation = Quaternion.RotateTowards(transform.rotation,targetRotation,rotationSpeed * Time.deltaTime);
            
        }
    }
}