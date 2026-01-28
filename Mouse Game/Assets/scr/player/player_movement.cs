using System.Collections;

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class player_movement : MonoBehaviour
{

    PlayerInput playerInput;
    InputAction moveAction;

    [SerializeField] private float MoveMultiplier;



   

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
    }


    void Update()
    {
        MovePlayer();

    }


    void MovePlayer()
    {
        Vector2 dirction = moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(dirction.x * MoveMultiplier, 0, dirction.y * MoveMultiplier) * Time.deltaTime;
    }
}
