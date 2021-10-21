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

        // Button Binding
        [HideInInspector]
        public bool j_Input;
        [HideInInspector]
        public bool d_Input;
        // [HideInInspector]
        public bool q_input;
        [HideInInspector]
        public bool rb_Input;
        [HideInInspector]
        public bool rt_Input;

        // Flag
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
            DodgeInput();
            AttackInput();
            SwapWeaponInput();
        }

        void MoveInput()
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        }

        void JumpInput()
        {
            j_Input = inputActions.PlayerActions.Jump.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            jumpFlag = j_Input;
        }

        void DodgeInput()
        {
            d_Input = inputActions.PlayerActions.Dodge.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            dodgeFlag = d_Input;
        }

        void AttackInput()
        {
            rb_Input = inputActions.PlayerActions.RB.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            rt_Input = inputActions.PlayerActions.RT.phase == UnityEngine.InputSystem.InputActionPhase.Started;
        }
    
        void SwapWeaponInput()
        {
            // q_input = inputActions.PlayerActions.SwapWeapon.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
            inputActions.PlayerActions.SwapWeapon.performed += _ => q_input = true;
        }
    }
}