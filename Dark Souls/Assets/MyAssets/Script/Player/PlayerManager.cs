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
        PlayerInventory playerInventory;
        Animator anim;

        public bool isGrounded;
        public bool isInteracting;
        public bool isDeath;

        public bool canCombo;

        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            animationHandler = GetComponentInChildren<AnimationHandler>();
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            anim = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            isInteracting = anim.GetBool("IsInteracting");
            canCombo = anim.GetBool("CanCombo");

            float delta = Time.deltaTime;

            // Essentials
            playerLocomotion.Gravity(delta);
            playerLocomotion.GroundedCheck();

            if (isDeath)
            {
                animationHandler.anim.SetBool("IsInteracting", false);
                // Bikin respon jika pelayer die
            }
            else
            {
                inputHandler.TickInput(delta);
                playerLocomotion.HandleMovement(delta);
                playerLocomotion.HandleJump(delta);
                playerLocomotion.HandleDodge(delta);

                playerAttacker.HandlePlayerAttack(delta);

                playerInventory.SwapWeapon();
            }
        }

        private void FixedUpdate() {
            animationHandler.HandleRootMotion();
        }
    }
}