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
        public bool isDeath;

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

            // Essentials
            inputHandler.TickInput(delta);
            playerLocomotion.GroundedCheck();

            if (isDeath)
            {
                // Death XD
                // Bikin respon jika pelayer die
            }
            else
            {
                playerLocomotion.HandleMovement(delta);
                playerLocomotion.HandleJumpAndGravity(delta);
                playerLocomotion.HandleDodge(delta);

                playerAttacker.HandlePlayerAttack(delta);
            }
        }

        private void FixedUpdate() {
            animationHandler.HandleRootMotion();
        }
    }
}