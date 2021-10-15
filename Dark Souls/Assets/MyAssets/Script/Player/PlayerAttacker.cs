using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GD
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimationHandler animationHandler;
        InputHandler inputHandler;
        PlayerInventory playerInventory;

        [HideInInspector]
        public float nextAttackTime = 0;

        bool lightAttack;
        bool heavyAttack;

        [SerializeField]
        float lightAttackRate = 2.5f;
        [SerializeField]
        float heavyAttackRate = 1f;

        private void Awake() {
            animationHandler = GetComponentInChildren<AnimationHandler>();
            inputHandler = GetComponent<InputHandler>();
            playerInventory = GetComponent<PlayerInventory>();
        }

        void HandleLightAttack(WeaponItem weapon)
        {
            animationHandler.HandlePlayTargetedAnimations(weapon.OH_Light_Attack_1, true);
        }

        void HandleHeavyAttack(WeaponItem weapon)
        {
            animationHandler.HandlePlayTargetedAnimations(weapon.OH_Heavy_Attack_1, true);
        }

        public void HandlePlayerAttack(float delta)
        {
            lightAttack = inputHandler.rb_Input;
            heavyAttack = inputHandler.rt_Input;

            if (Time.time >= nextAttackTime)
            {
                if (lightAttack)
                {
                    HandleLightAttack(playerInventory.rightWeapon);
                    inputHandler.rb_Input = false;
                    nextAttackTime = Time.time + 1f / lightAttackRate;
                }
                
                if (heavyAttack)
                {
                    HandleHeavyAttack(playerInventory.rightWeapon);
                    inputHandler.rt_Input = false;
                    nextAttackTime = Time.time + 1f / heavyAttackRate;
                }
            }
        }
    }
}