using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GD
{
    public class InputHandler : MonoBehaviour
    {
        PlayerControls inputActions;

        public float vertical;
        public float horizontal;
        public float moveAmount;
        
        Vector2 movementInput;

        [HideInInspector]
        public bool j_input;
        [HideInInspector]
        public bool d_input;
        [HideInInspector]
        public bool jumpFlag;
        [HideInInspector]
        public bool dodgeFlag;

        void Awake()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
            }
        }
        void OnEnable()
        {
            inputActions.Enable();
        }

        void OnDisable()
        {
            inputActions.Disable();
        }

        void Start()
        {
            inputActions.PlayerMovement.Movement.performed += context => movementInput = context.ReadValue<Vector2>();
        }

        public void TickInput(float delta)
        {
            MoveInput();
            JumpInput();
            DodgeInput(delta);
        }

        void MoveInput()
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        }

        void JumpInput()
        {
            j_input = inputActions.PlayerActions.Jump.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            jumpFlag = j_input;
        }

        void DodgeInput(float delta)
        {
            d_input = inputActions.PlayerActions.Dodge.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            dodgeFlag = d_input;
        }
    }
}