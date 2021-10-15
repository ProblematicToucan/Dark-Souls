using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GD
{
    public class AnimationHandler : MonoBehaviour
    {
        public Animator anim;
        PlayerManager playerManager;
        PlayerLocomotion playerLocomotion;

        // animation IDs
        private int _animIDVertical;
        private int _animIDHorizontal;
        private int _animIDIsInteracting;

        Vector3 rootMotion;
        void Start()
        {
            playerManager = GetComponentInParent<PlayerManager>();
            playerLocomotion = GetComponentInParent<PlayerLocomotion>();
            anim = GetComponent<Animator>();
            AssignAnimationIDs();
        }

        private void AssignAnimationIDs()
        {
            _animIDVertical = Animator.StringToHash("Vertical");
            _animIDHorizontal = Animator.StringToHash("Horizontal");
            _animIDIsInteracting = Animator.StringToHash("IsInteracting");
        }

        public void HandleUpdateAnimatorValues(float verticalMovement, float horizontalMovement)
        {
            // logic if jika menambahkan fitur sprint;
            anim.SetFloat(_animIDVertical, verticalMovement, 0.1f, Time.deltaTime);
            anim.SetFloat(_animIDHorizontal, horizontalMovement, 0.1f, Time.deltaTime);
        }
        public void HandlePlayTargetedAnimations(string targetAnim, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool(_animIDIsInteracting, isInteracting);
            anim.CrossFade(targetAnim, 0.2f);
        }

        private void OnAnimatorMove() {
            rootMotion += anim.deltaPosition;
        }

        public void HandleRootMotion()
        {
            playerLocomotion.controller.Move(rootMotion);
            rootMotion = Vector3.zero;
        }
    }
}