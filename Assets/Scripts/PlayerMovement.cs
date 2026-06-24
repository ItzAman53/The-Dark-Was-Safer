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

    [SerializeField] private AudioSource footstepSource;

    [SerializeField] private AudioClip footstep1;
    [SerializeField] private AudioClip footstep2;

    private bool playFirst = true;
    private float footstepTimer;
    [SerializeField] private float footstepInterval = 0.5f;
    
    public Animator anim;
    public int TreasureCount=0;
    public int Level4TreasureCount=0;
    
    public bool TurnOnHorizontalMovement=false;
    public bool isLevel4=false;
    public bool IsEscapeRunning = false;
    [SerializeField] private float escapeRunSpeed = 8f;
    



    
    [SerializeField]private  float maxSpeed=4f;



    void Update()
    {
        if(!cc.enabled)
    return;

        inputMoveVector=move.action.ReadValue<Vector2>();
       
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
        if (IsEscapeRunning)
        {
            cc.Move(
                transform.forward *
                escapeRunSpeed *
                Time.deltaTime
            );
        }
        else
        {
            Vector3 moveDirection=new Vector3(inputMoveVector.x,0,inputMoveVector.y);
            moveVelocity=Vector3.Lerp(moveVelocity,moveDirection,5f*Time.deltaTime);
            cc.Move(moveVelocity*maxSpeed*Time.deltaTime);
        }
     bool isMoving = inputMoveVector.magnitude > 0.1f;

    if (isMoving && !IsEscapeRunning)
    {
        footstepTimer += Time.deltaTime;

        if (footstepTimer >= footstepInterval)
        {
            footstepTimer = 0f;

            AudioClip clip =
                UnityEngine.Random.Range(0, 2) == 0
                ? footstep1
                : footstep2;

            footstepSource.PlayOneShot(clip);
        }
    }
    else
    {
        footstepTimer = footstepInterval;
    }


        
    }
}
