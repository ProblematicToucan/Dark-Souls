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
        PlayerAttacker playerAttacker;
        Animator anim;

        public bool isGrounded;
        public bool isInteracting;

        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            animationHandler = GetComponentInChildren<AnimationHandler>();
            playerAttacker = GetComponent<PlayerAttacker>();
            anim = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            isInteracting = anim.GetBool("IsInteracting");

            float delta = Time.deltaTime;

            // InputHandler
            inputHandler.TickInput(delta);

            // PlayerLocomotion
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.GroundedCheck();
            playerLocomotion.HandleJumpAndGravity(delta);
            playerLocomotion.HandleDodge(delta);

            // PlayerAttacker
            playerAttacker.HandlePlayerAttack(delta);
        }

        private void FixedUpdate() {
            animationHandler.HandleRootMotion();
        }
    }
}