using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]

public class PlayerController : InputController
{
    private PlayerInputActions inputActions;
    private bool isJumping;

    private void OnEnable()
    {
        inputActions = new PlayerInputActions();
        inputActions.Gameplay.Enable();
        inputActions.Gameplay.Jump.started += JumpStarted;
        inputActions.Gameplay.Jump.canceled += JumpCanceled;
    }

    private void OnDisable()
    {
        inputActions.Gameplay.Disable();
        inputActions.Gameplay.Jump.started -= JumpStarted;
        inputActions.Gameplay.Jump.canceled -= JumpCanceled;
        inputActions = null;
    }

    private void JumpCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        isJumping = false;
    }

    private void JumpStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        isJumping = true;
    }

    public override bool RetrieveJumpInput(GameObject gameObject)
    {
        return isJumping;
    }

    public override float RetrieveMoveInput(GameObject gameObject)
    {
        return inputActions.Gameplay.Move.ReadValue<Vector2>().x;
    }
}
