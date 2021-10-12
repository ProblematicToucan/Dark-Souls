using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GD
{
    public class PlayerManager : MonoBehaviour
    {   
        PlayerLocomotion playerLocomotion;
        InputHandler inputHandler;
        AnimationHandler animationHandler;
        Animator anim;

        public bool isGrounded;
        public bool isInteracting;

        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            animationHandler = GetComponentInChildren<AnimationHandler>();
            anim = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            isInteracting = anim.GetBool("IsInteracting");

            float delta = Time.deltaTime;

            inputHandler.TickInput(delta);

            playerLocomotion.HandleMovement(delta);
            playerLocomotion.GroundedCheck();
            playerLocomotion.HandleJumpAndGravity(delta);
            playerLocomotion.HandleDodge(delta);
        }

        private void FixedUpdate() {
            animationHandler.HandleRootMotion();
        }
    }
}