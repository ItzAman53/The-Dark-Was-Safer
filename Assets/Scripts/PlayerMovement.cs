using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference look;
    [SerializeField] private CharacterController cc;
    [SerializeField] private Vector2 inputMoveVector;
    [SerializeField] private Vector3 moveVelocity;
    
    public Animator anim;
    
    public bool TurnOnHorizontalMovement=false;
    



    
    private float maxSpeed=4f;



    void Update()
    {
        

        inputMoveVector=move.action.ReadValue<Vector2>();
        Debug.Log(inputMoveVector);
        
        if (!TurnOnHorizontalMovement)
        {

            inputMoveVector.x=0;
            if(inputMoveVector.y!=0)
            {
                anim.SetBool("isMoving",true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
            

        }
        Vector3 moveDirection=new Vector3(inputMoveVector.x,0,inputMoveVector.y);
        moveVelocity=Vector3.Lerp(moveVelocity,moveDirection,5f*Time.deltaTime);
        cc.Move(moveVelocity*maxSpeed*Time.deltaTime);
    }
}
