using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    public event EventHandler OnInteraction;

    PlayerAction playerInputAction;

    private void Awake()
    {
        playerInputAction = new PlayerAction();
        playerInputAction.Player.Enable();

        playerInputAction.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteraction?.Invoke(this,EventArgs.Empty);
    }

    public Vector2 GetNormalizedVector()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();

        return inputVector.normalized;
    }
}
