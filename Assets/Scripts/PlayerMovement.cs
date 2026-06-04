using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference move;
    [SerializeField] private CharacterController cc;
    [SerializeField] private Vector2 inputMoveVector;


    private float playerSpeed=4f;



    void Update()
    {
        inputMoveVector=move.action.ReadValue<Vector2>();
        Vector3 moveVector=new Vector3(inputMoveVector.x,0,inputMoveVector.y);
        
        cc.Move(moveVector*playerSpeed*Time.deltaTime);
    }
}
