using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GD
{
    public class PlayerLocomotion : MonoBehaviour
    {
        PlayerManager playerManager;
        public CharacterController controller;
        InputHandler inputHandler;
        AnimationHandler animationHandler;
        PlayerAttacker playerAttacker;
        
        public Transform cam;

        [Header("Locomotion Stats")]
        [SerializeField]
        float movementSpeed = 4.0f;
        [SerializeField]
        [Range(0.0f, 0.3f)]
        float turnSmoothRotationTime = 0.12f;
        [SerializeField]
        float jumpHeight = 1.2f;
        [SerializeField]
        float jumpTimeout = 0.50f;
        [SerializeField]
        float fallTimeout = 0.15f;

        [Header("Ground & Gravity Physics")]
        [SerializeField]
        float gravity = -9.81f;
        [SerializeField]
        float groundedOffset = -0.14f;
        [SerializeField]
        float groundSphereDistance = 0.2f;
        [SerializeField]
        LayerMask groundLayerMask;

        float turnSmoothVelocity;
        Vector3 velocity;
        float jumpTimeoutDelta;
        float fallTimeoutDelta;

        void Start()
        {
            playerManager = GetComponent<PlayerManager>();
            controller = GetComponent<CharacterController>();
            inputHandler = GetComponent<InputHandler>();
            animationHandler = GetComponentInChildren<AnimationHandler>();
            playerAttacker = GetComponent<PlayerAttacker>();
            cam = Camera.main.transform;

            jumpTimeoutDelta = jumpTimeout;
            fallTimeoutDelta = fallTimeout;
        }

        public void GroundedCheck()
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
            playerManager.isGrounded = Physics.CheckSphere(spherePosition, groundSphereDistance, groundLayerMask, QueryTriggerInteraction.Ignore);
        }

        public void HandleMovement(float delta)
        {
            Vector3 direction = new Vector3(inputHandler.horizontal, 0.0f, inputHandler.vertical).normalized;

            if (inputHandler.moveAmount >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothRotationTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                if (playerManager.isInteracting) return;
                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDirection.normalized * movementSpeed * delta);

            }
            animationHandler.HandleUpdateAnimatorValues(0, inputHandler.moveAmount);
        }

        public void HandleJumpAndGravity(float delta)
        {
            velocity.y += gravity * delta;
            controller.Move(velocity * delta);
            // animationHandler.anim.SetBool("Jump", inputHandler.jumpFlag);

            if (playerManager.isGrounded)
            {
                fallTimeoutDelta = fallTimeout;

                animationHandler.anim.SetBool("IsInAir", false);
                
                if (velocity.y < 0f) velocity.y = -2f;

                if (playerManager.isInteracting) return;
                animationHandler.anim.SetBool("Jump", inputHandler.jumpFlag);
                if (inputHandler.jumpFlag && jumpTimeoutDelta <= 0.0f)
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }

                if (jumpTimeoutDelta >= 0.0f) jumpTimeoutDelta -= delta;
            }
            else
            {
                animationHandler.anim.SetBool("Jump", inputHandler.jumpFlag);
                jumpTimeoutDelta = jumpTimeout;

                if (fallTimeoutDelta >= 0.0f)
                {
                    fallTimeoutDelta -= delta;
                }
                else
                {
                    animationHandler.anim.SetBool("IsInAir", true);
                }
                inputHandler.j_Input = false;
            }
             
        }

        public void HandleDodge(float delta)
        {
            if (playerManager.isGrounded == false) return;

            if (playerManager.isInteracting) return;

            if (inputHandler.dodgeFlag)
            {
                if(inputHandler.moveAmount > 0)
                {
                    animationHandler.HandlePlayTargetedAnimations("Roll", true);
                    playerAttacker.nextAttackTime = Time.time + 1f / 1.5f;
                }
                else
                {
                    animationHandler.HandlePlayTargetedAnimations("Backstep", true);
                    playerAttacker.nextAttackTime = Time.time + 1f / 2.9f;
                }
            }
        }
    }
}